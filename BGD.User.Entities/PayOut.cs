using System;

namespace BGD.User.Entities
{
    public class PayOut
    {
        public Guid? Id { get; set; }
        public Guid? Orderid { get; set; }
        public string Category { get; set; }
        public  decimal Value { get; set; }

        public PayOut()
        {
            Id = Guid.NewGuid();
        }
    }
}