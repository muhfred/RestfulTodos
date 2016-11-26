using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TodoWebApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext() : base("DefaultConnection")
        {

        }
        public DbSet<Todo> Todos { get; set; }
    }
}