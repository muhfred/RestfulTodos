using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TodoWebApi.Models;

namespace TodoWebApi.API
{
    public class TodosController : ApiController
    {
        private readonly ITodoRepository _repo;
        public TodosController(ITodoRepository repo)
        {
            _repo = repo;
        }
        HttpResponseMessage Response;
        // GET: api/Todoes
        public IQueryable<Todo> GetTodos()
        {
            return _repo.GetTodos();
        }

        // GET: api/Todoes/5
        public HttpResponseMessage GetTodo(int id)
        {
            Todo todo =  _repo.GetTodoById(id);
            if (todo == null)
            {
                return Response = Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Response = Request.CreateResponse(HttpStatusCode.OK, todo); 
        }

        // POST: api/Todoes
        public HttpResponseMessage AddTodo([FromBody] Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return Response = Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                _repo.SaveTodo(todo);
            }
            catch (Exception)
            {

                throw;
            }
            

            return Response = Request.CreateResponse(HttpStatusCode.OK, todo);
        }

        // PUT: api/Todoes/5
        [HttpPut]
        public HttpResponseMessage UpdateTodo(int id, Todo todo)
        {
            //HttpResponseMessage Response;
            if (!ModelState.IsValid)
            {
                Response = Request.CreateResponse(HttpStatusCode.BadRequest);
                return Response;
            }

            if (id != todo.Id)
            {
                return Response = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            try
            {
                _repo.UpdateTodo(todo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repo.TodoExists(id))
                {
                    return Response = Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    throw;
                }
            }
            

            return Response = Request.CreateResponse(HttpStatusCode.OK,todo);
        }

      

        // DELETE: api/Todoes/5
        public HttpResponseMessage DeleteTodo(int id)
        {
            Todo todo = _repo.GetTodoById(id);
            if (todo == null)
            {
                return Response = Request.CreateResponse(HttpStatusCode.NotFound, todo);
            }

            _repo.RemoveTodo(todo);

            return Response = Request.CreateResponse(HttpStatusCode.OK, todo);
        }
    }
}