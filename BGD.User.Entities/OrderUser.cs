using System;

namespace BGD.User.Entities
{
    public class OrderUser
    {
        public Guid? Id { get; set; }
        public Guid? OrdersId { get; set; }
        public Guid? UsersId { get; set; }
        

        public OrderUser()
        {
            Id = Guid.NewGuid();
        }
    }
}