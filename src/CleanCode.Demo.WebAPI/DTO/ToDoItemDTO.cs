using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CleanCode.Demo.WebAPI.DTO
{
    public class ToDoItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
    }
}