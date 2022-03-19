using Dapper.FluentMap.Dommel.Mapping;

namespace BGD.User.Entities.Dapper
{
    public class BuyValueMap : DommelEntityMap<BuyValue>
    {
        public BuyValueMap()
        {
            ToTable("buy_value");
            Map(x => x.Id).ToColumn("Id").IsIdentity();
            Map(x => x.Meterprice).ToColumn("Meterprice");
            Map(x => x.Category).ToColumn("Category");
        }
    }
}