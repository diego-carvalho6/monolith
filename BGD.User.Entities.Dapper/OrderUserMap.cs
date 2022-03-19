using Dapper.FluentMap.Dommel.Mapping;

namespace BGD.User.Entities.Dapper
{
    public class OrderUserMap : DommelEntityMap<OrderUser>
    {
       public OrderUserMap()
        {
            ToTable("order_user");
            Map(x => x.Id).ToColumn("Id").IsIdentity();
            Map(x => x.OrdersId).ToColumn("OrdersId");
            Map(x => x.UsersId).ToColumn("UsersId");
        }
    }
}