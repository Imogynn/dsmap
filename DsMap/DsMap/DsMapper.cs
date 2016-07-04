using System;
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

    public class Cookbook<T>
    {
        public NewClass Create<NewClass>(object ingredients) where NewClass : new()
        { 

            Type recipeType = typeof(T);
            Type newClassType = typeof(NewClass);
            Type oldClassType = ingredients.GetType();

            // need to look for potential other constructors...
            var result = new NewClass();

            // will want to look at properties so that we can use interfaces for recipes
            /*foreach (var property in recipeType.GetProperties() )
            {

            }*/

            foreach (var field in recipeType.GetFields()) // need to consider private fields etc.
            {
                var value = oldClassType.GetField(field.Name).GetValue(ingredients);
                var fieldToSet = newClassType.GetField(field.Name);
                fieldToSet.SetValue(result, value);

            }

            return result;
        }
    }
}
