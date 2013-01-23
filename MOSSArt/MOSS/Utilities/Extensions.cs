using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection;

namespace MOSSArt.Utilities
{
    public static class Extensions
    {
        public static string AsString<T>(this T input) where T : class
        {
            return input == null ? string.Empty : input.ToString();
        }

        public static bool IsNotNullOrWhitespace(this string input)
        {
            return input != null && input.Trim().Length != 0;
        }

        public static DataTable AsDataTable<T>(this IEnumerable<T> enumerable)
        {
            DataTable dt = new DataTable("Generated");

            T first = enumerable.FirstOrDefault();
            if (first != null)
            {
                PropertyInfo[] properties = first.GetType().GetProperties();

                foreach (PropertyInfo propertyInfo in properties)
                {
                    dt.Columns.Add(propertyInfo.Name, propertyInfo.PropertyType);
                }

                foreach (T t in enumerable)
                {
                    DataRow row = dt.NewRow();
                    foreach (PropertyInfo propertyInfo in properties)
                    {
                        row[propertyInfo.Name] = t.GetType().InvokeMember(propertyInfo.Name, BindingFlags.GetProperty, null, t, null);
                    }
                    dt.Rows.Add(row);
                }
            }

            return dt;
        }
    }
}
