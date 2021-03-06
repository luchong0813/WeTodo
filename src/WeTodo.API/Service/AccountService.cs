using AutoMapper;

using System.Threading.Tasks;

using WeTodo.API.DataContext;
using WeTodo.Share.Common.Utils;

using WeToDo.Api;
using WeToDo.Share.Dtos;
using WeToDo.Share.Extensions;

namespace WeTodo.API.Service
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResult> LoginAsync(string userName, string password)
        {
            try
            {
                var pwd = ((password + "234789").GetMD5()).GetMD5();
                var repository = unitOfWork.GetRepository<User>();
                if (await IsAccountExist(repository, userName) == false)
                    return new ApiResult((int)ResultEnum.ACCOUNT_NOEXIST, "该账户不存在");

                var model = await unitOfWork
               .GetRepository<User>()
               .GetFirstOrDefaultAsync(predicate: x =>
               x.UserName.Equals(userName) &&
               x.PassWord.Equals(pwd));
                 
                if (model == null)
                    return new ApiResult((int)ResultEnum.ERROR_USER, "账号或密码错误");
                return new ApiResult(model);
            }
            catch (System.Exception ex)
            {
                return new ApiResult((int)ResultEnum.FAIL, ex.Message);
            }
        }

        public async Task<ApiResult> RegisterAsync(UserDto user)
        {
            try
            {
                var model = mapper.Map<User>(user);
                var repository = unitOfWork.GetRepository<User>();
                if (await IsAccountExist(repository, model.UserName))
                    return new ApiResult((int)ResultEnum.ACCOUNT_EXIST, "该账户已存在");

                //双重MD5+自定义密钥
                //model.PassWord = MD5Util.GenerateMD5(MD5Util.GenerateMD5(model.PassWord + "123456"));
                model.PassWord = ((model.PassWord + "234789").GetMD5()).GetMD5();

                model.CreatDate = System.DateTime.Now;

                await repository.InsertAsync(model);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResult(model);
                return new ApiResult((int)ResultEnum.FAIL, "账户注册失败");
            }
            catch (System.Exception ex)
            {
                return new ApiResult((int)ResultEnum.FAIL, ex.Message);
            }
        }

        /// <summary>
        /// 检查账户是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        private async Task<bool> IsAccountExist(IRepository<User> repository, string userName)
        {
            var userModel = await repository.GetFirstOrDefaultAsync(predicate: x => x.UserName.Equals(userName));
            if (userModel == null)
                return false;
            return true;
        }
    }
}
