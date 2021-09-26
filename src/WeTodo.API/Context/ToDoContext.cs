using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeTodo.API.DataContext
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<ToDo> ToDo { get; set; }
        public DbSet<Memo> Memo { get; set; }
    }
}
