using System;
using System.Collections.Generic;

namespace BGD.User.Entities
{
    public class Item
    {
        public Guid? Id { get; set; }
        public string Category { get; set; }
        public decimal Value { get; set; }
        public int Cod { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        

        public List<Order> orders { get; set; }

        public Item()
        {
            Description = "";
            Id = Guid.NewGuid();
        }

    }
}