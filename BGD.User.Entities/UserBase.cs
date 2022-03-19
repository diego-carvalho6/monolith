using System;
using System.Collections.Generic;
using BGD.User.Entities.Extensions;


namespace BGD.User.Entities
{
    public class UserBase
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Enums.UserStatus Status { get; set; }
        public List<Order>? Orders { get; set; }

        public UserBase()
        {
            Id = Guid.NewGuid();
        }
        
    }
    
}
