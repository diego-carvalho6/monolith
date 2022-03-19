using System;

namespace BGD.User.Entities
{
    public class BuyValue
    {
        public Guid? Id { get; set; }
        public decimal Meterprice { get; set; }
        public string Category { get; set; }

        public BuyValue()
        {
            Id = Guid.NewGuid();
        }
    }
}