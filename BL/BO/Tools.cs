using System.Collections;
using System.Reflection;
using System.Text;

namespace BO
{
    public static class Tools
    {
        private static readonly DalApi.IDal _dal = DalApi.Factory.Get;

        /// <summary>
        /// Converts an object to a string representation, including the values of its properties.
        /// </summary>
        /// <typeparam name="T">The type of the object to convert.</typeparam>
        /// <param name="obj">The object to convert.</param>
        /// <returns>A string representation of the object, including its properties.</returns>

        public static string ToStringProperty<T>(this T obj)
        {
            StringBuilder result = new StringBuilder();
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(obj);
                if (value != null)
                {
                    if (property.PropertyType.IsGenericType &&
                        property.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    {
                        IEnumerable collection = (IEnumerable)value;
                        StringBuilder collectionBuilder = new StringBuilder();

                        foreach (var item in collection)
                        {
                            collectionBuilder.Append($"{item}, ");
                        }

                        if (collectionBuilder.Length > 0)
                            collectionBuilder.Remove(collectionBuilder.Length - 2, 2); // Remove the last comma and space

                        result.AppendLine($"{property.Name}: [{collectionBuilder}]");
                    }
                    else
                    {
                        result.AppendLine($"{property.Name}: {value}");
                    }
                }
            }

            return result.ToString();
        }


    }
}
