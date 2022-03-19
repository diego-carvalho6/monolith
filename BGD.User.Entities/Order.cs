using System;
using System.Collections.Generic;

namespace BGD.User.Entities
{
    public class Order
    {
        public Guid? Id { get; set; }
        public bool Finished { get; set; }
        public bool Payed { get; set; }
        public decimal Finalprice { get; set; }
        public decimal Discount { get; set; }
        public decimal AdtionalFee { get; set; }
        public DateTime Createdat { get; set; }
        public DateTime Until { get; set; }
        public Enums.OderStatus Progress { get; set; }
        public int Table { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
        public List<PayOut> Payouted { get; set; }
        public List<User> Employers { get; set; }

        public Order()
        {
            Id = Guid.NewGuid();
            Createdat = DateTime.UtcNow;
        }
        
    }
}