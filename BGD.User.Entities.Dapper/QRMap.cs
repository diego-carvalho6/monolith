using Dapper.FluentMap.Dommel.Mapping;

namespace BGD.User.Entities.Dapper
{
    public class QRMap: DommelEntityMap<QR>
    {
        public QRMap()
        {
            ToTable("qr");
            Map(x => x.Id).ToColumn("Id").IsIdentity();
            Map(x => x.Tenant).ToColumn("Tenant");
            Map(x => x.Table).ToColumn("Table");
            Map(x => x.Url).ToColumn("Url");
        }
    }

}