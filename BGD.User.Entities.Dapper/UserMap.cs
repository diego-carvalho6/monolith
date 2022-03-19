using System.ComponentModel.DataAnnotations.Schema;
using Dapper.FluentMap.Dommel.Mapping;
using Dapper.FluentMap.Dommel;
using Dapper.FluentMap;
using Dapper.FluentMap.Mapping;

namespace BGD.User.Entities.Dapper
{
    public class UserMap : DommelEntityMap<User>
    {
        public UserMap()
        {
            ToTable("user");
            Map(x => x.Id).ToColumn("Id");
            Map(x => x.Name).ToColumn("Name");
            Map(x => x.Password).ToColumn("Password");
            Map(x => x.Status).ToColumn("Status");
            Map(x => x.Username).ToColumn("Username");
        }
    }
}
