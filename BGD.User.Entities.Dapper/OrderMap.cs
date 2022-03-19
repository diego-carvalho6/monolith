using Dapper.FluentMap.Dommel.Mapping;

namespace BGD.User.Entities.Dapper
{
    public class OrderMap : DommelEntityMap<Order>
    {
        public OrderMap()
        {
            ToTable("order");
            Map(x => x.Id).ToColumn("Id").IsIdentity();
            Map(x => x.Finished).ToColumn("Finished");
            Map(x => x.Payed).ToColumn("Payed");
            Map(x => x.Finalprice).ToColumn("Finalprice");
            Map(x => x.Discount).ToColumn("Discount");
            Map(x => x.Createdat).ToColumn("Createdat");
            Map(x => x.Until).ToColumn("Until");
            Map(x => x.Progress).ToColumn("Progress");
            Map(x => x.Table).ToColumn("Table");
            Map(x => x.Description).ToColumn("Description");
            Map(x => x.AdtionalFee).ToColumn("AdtionalFee");
        }
    }
}