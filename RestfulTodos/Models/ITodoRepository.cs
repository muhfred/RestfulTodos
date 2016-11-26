using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoWebApi.Models
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