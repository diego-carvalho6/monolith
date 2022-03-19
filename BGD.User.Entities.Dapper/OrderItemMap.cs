using Dapper.FluentMap.Dommel.Mapping;

namespace BGD.User.Entities.Dapper
{
    public class OrderItemMap : DommelEntityMap<OrderItem>
    {
        public OrderItemMap()
        {
            ToTable("order_item");
            Map(x => x.Id).ToColumn("Id").IsIdentity();
            Map(x => x.ItemsId).ToColumn("ItemsId");
            Map(x => x.OrdersId).ToColumn("OrdersId");
            Map(x => x.Description).ToColumn("Description");
            Map(x => x.Status).ToColumn("Status");
        }
    }
}