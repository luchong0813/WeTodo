

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WeTodo.API.DataContext;

using WeToDo.Api;

namespace WeTodo.API.Repository
{
    public class MemoRepository : Repository<Memo>, IRepository<Memo>
    {
        public MemoRepository(ToDoContext dbContext) : base(dbContext)
        {
        }
    }
}
