using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodoWebApi.Models
{
    public class Todo
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public bool isCompleted { get; set; }

        public override bool Equals(object obj)
        {
            var todo = obj as Todo;
            return (todo != null) && (Id == todo.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}