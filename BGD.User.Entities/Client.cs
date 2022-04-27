using System;
using System.Collections.Generic;

namespace BGD.User.Entities
{
    public class Client
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string CellPhone { get; set; }
        public int AdsCount {get; set;}
        
        public DateTime PaymentDate { get; set; }
        
        public Client()
        {
            Id = Guid.NewGuid();
        }
    }
}