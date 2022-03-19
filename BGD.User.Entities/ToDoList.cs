using System;

namespace BGD.User.Entities
{
    public class ToDoList
    {
        public Guid? Id { get; set; }
        public Guid? Orderid { get; set; }
         public string Comment { get; set; }
         public DateTime Createdat { get; set; }
         public string Username{ get; set; }

         public ToDoList()
         {
             Createdat = DateTime.Now;
             Id = Guid.NewGuid();
         }
    }
}