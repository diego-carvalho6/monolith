using System;
using System.Collections.Generic;

namespace BGD.User.Entities
{
    public class Client
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Cellphone { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public List<Order> Orders { get; set; }
        
        public Client()
        {
            Id = Id.HasValue ? Id : Guid.NewGuid();
        }
        
    }
}