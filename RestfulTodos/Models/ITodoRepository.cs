using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestfulTodos.Models;

namespace RestfulTodos.Models
{
    public interface ITodoRepository
    {
        IQueryable<Todo> GetTodos();
        Todo GetTodoById(int id);
        bool UpdateTodo(Todo todo);
        bool SaveTodo(Todo todo);
        bool RemoveTodo(Todo todo);
        bool TodoExists(int? id);
    }
}