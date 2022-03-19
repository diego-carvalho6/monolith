using Dapper.FluentMap.Dommel.Mapping;

namespace BGD.User.Entities.Dapper
{
    public class ToDoListMap : DommelEntityMap<ToDoList>
    {
        public ToDoListMap()
        { 
            ToTable("to_do_list");
            Map(x => x.Id).ToColumn("Id").IsIdentity();
            Map(x => x.Orderid).ToColumn("Orderid");
            Map(x => x.Comment).ToColumn("Comment");
            Map(x => x.Createdat).ToColumn("Createdat");
            Map(x => x.Username).ToColumn("Username");
        }
    }
}