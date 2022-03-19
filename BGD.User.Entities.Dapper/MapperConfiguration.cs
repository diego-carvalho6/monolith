using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;

namespace BGD.User.Entities.Dapper
{
    public class MapperConfiguration
    {
        public static void ConfigureMappers()
        {
          
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new UserMap());
                config.AddMap(new ItemMap());
                config.AddMap(new OrderMap());
                config.AddMap(new PayOutMap());
                config.AddMap(new OrderUserMap());
                config.AddMap(new OrderItemMap());
                config.ForDommel();

            });
        }
    }
}
