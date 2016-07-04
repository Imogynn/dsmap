using System.Management.Instrumentation;
using System.Reflection;

namespace DsMap
{
    public class DsMapper
    {
  

        public static Cookbook<T> FromRecipe<T>()
        {
            return new Cookbook<T>();
        }
    }
}
