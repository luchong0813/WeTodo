using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WeTodo.API.DataContext;
using WeTodo.API.UnitOfWork;

namespace WeTodo.API.Repository
{
    public class TodoRepository : Repository<ToDo>, IRepository<ToDo>
    {
        public TodoRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
