using Dapper.FluentMap.Dommel.Mapping;

namespace BGD.User.Entities.Dapper
{
    public class ItemMap : DommelEntityMap<Item>
    {
        public ItemMap()
        {
            ToTable("item");
            Map(x => x.Id).ToColumn("Id").IsIdentity();
            Map(x => x.Category).ToColumn("Category");
            Map(x => x.Value).ToColumn("Value");
            Map(x => x.Description).ToColumn("Description");
            Map(x => x.Name).ToColumn("Name");
            Map(x => x.Cod).ToColumn("Cod");

        }
    }
}