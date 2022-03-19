using System;

namespace BGD.User.Entities
{
    public class OrderItem
    {
        public Guid? Id { get; set; }
        public Guid OrdersId { get; set; }
        public Guid ItemsId { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }

        public OrderItem()
        {
            Id = Guid.NewGuid();
        }
    }
}