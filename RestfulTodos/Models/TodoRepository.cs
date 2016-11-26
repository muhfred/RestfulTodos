using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace TodoWebApi.Models
{
    public class TodoRepository : ITodoRepository
    {
        TodoContext _ctx;
        public TodoRepository(TodoContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Todo> GetTodos()
        {
            return _ctx.Todos;
        }

        public Todo GetTodoById(int id)
        {
            return _ctx.Todos.Find(id);
        }

        public bool SaveTodo(Todo todo)
        {
            try
            {
                _ctx.Todos.Add(todo);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateTodo(Todo todo)
        {
            try
            {
                _ctx.Entry(todo).State = EntityState.Modified;
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
                
            }
        }

        public bool RemoveTodo(Todo todo)
        {
            try
            {
                _ctx.Todos.Remove(todo);
                _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public bool TodoExists(int? id)
        {
            return _ctx.Todos.Count(e => e.Id == id) > 0;
        }
    }
}