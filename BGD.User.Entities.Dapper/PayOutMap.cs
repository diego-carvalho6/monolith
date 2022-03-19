using Dapper.FluentMap.Dommel.Mapping;

namespace BGD.User.Entities.Dapper
{
    public class PayOutMap : DommelEntityMap<PayOut>
    {
        public PayOutMap()
        {
            ToTable("pay_out");
            Map(x => x.Id).ToColumn("Id").IsIdentity();
            Map(x => x.Orderid).ToColumn("Orderid");
            Map(x => x.Category).ToColumn("Category");
            Map(x => x.Value).ToColumn("Value");
        }
    }
}