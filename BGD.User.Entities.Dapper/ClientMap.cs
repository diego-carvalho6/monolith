using Dapper.FluentMap.Dommel.Mapping;

namespace BGD.User.Entities.Dapper
{
    public class ClientMap : DommelEntityMap<Client>
    {
        public ClientMap()
        {
            ToTable("client");
            Map(x => x.Id).ToColumn("Id").IsIdentity();
            Map(x => x.Name).ToColumn("Name");
            Map(x => x.Description).ToColumn("Description");
            Map(x => x.Address).ToColumn("Address");
            Map(x => x.Cellphone).ToColumn("Cellphone");
        }
    }
}