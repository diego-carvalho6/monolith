using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BGD.User.Entities.Extensions
{
    // public static class FillPropertiesExtension
    // {
    //     public static void Fillproperties(IEnumerable<dynamic> entityToGive)
    //     {
    //
    //         foreach (var kvp in entityToGive.FirstOrDefault())
    //         {
    //
    //         }
    //     }
    //   
    //     public static void Fillproperties(object entityToPick, object entityToGive)
    //     {
    //         foreach (var PropertyInfo in entityToGive.GetType().GetProperties())
    //         {
    //             var value = entityToPick.GetType().GetProperty(PropertyInfo.Name);
    //             value?.SetValue(entityToPick ,PropertyInfo.GetValue(entityToGive));
    //         }
    //     }

    public class FillpropertiesExtension
    {
        
        public static void Fillproperties(object entityToPick, IEnumerable<dynamic> entityToGive)
        {
            foreach (var kvp in entityToGive.FirstOrDefault())
            {
                var value = entityToPick.GetType().GetProperty(kvp.Key);
                value?.SetValue(entityToPick ,kvp.Value);
            }
        }
      
        public static void Fillproperties(object entityToPick, object entityToGive)
        {
            foreach (var PropertyInfo in entityToGive.GetType().GetProperties())
            {
                var value = entityToPick.GetType().GetProperty(PropertyInfo.Name);
                value?.SetValue(entityToPick ,PropertyInfo.GetValue(entityToGive));
            }
        }
    }
}