using AutoMapper.Configuration;

using WeTodo.API.DataContext;

using WeToDo.Share.Dtos;

namespace WeTodo.API.Extenions
{
    /// <summary>
    /// 实体与传输映射层
    /// </summary>
    public class AutoMapperProFile : MapperConfigurationExpression
    {
        public AutoMapperProFile()
        {
            CreateMap<ToDo,TodoDto>().ReverseMap();
        }
    }
}
