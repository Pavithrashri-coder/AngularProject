using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Options.Helper
{
    internal static class ListAsTableValuedParameterExtensions
    {
        public static SqlMapper.ICustomQueryParameter? AsTableValuedParameter<T>(this IEnumerable<T> enumerable, string typeName) where T : class
        {
            List<string> list = new List<string>();
            if (enumerable == null)
            {
                return null;
            }

            T val = enumerable.FirstOrDefault();
            if (val != null)
            {
                PropertyInfo[] properties = val.GetType().GetProperties();
                foreach (PropertyInfo propertyInfo in properties)
                {
                    list.Add(propertyInfo.Name);
                }
            }

            DataTable dataTable = new DataTable();
            if (typeof(T).IsValueType || (typeof(T).FullName ?? string.Empty).Equals("System.String"))
            {
                dataTable.Columns.Add((list == null) ? "NONAME" : list.First(), typeof(T));
                foreach (T item in enumerable)
                {
                    dataTable.Rows.Add(item);
                }
            }
            else
            {
                PropertyInfo[] properties2 = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
                PropertyInfo[] readableProperties = properties2.Where((PropertyInfo w) => w.CanRead).ToArray();
                if (readableProperties.Length > 1 && list == null)
                {
                    throw new ArgumentException("Ordered list of column names  must be provided when TVP contains more than one column");
                }

                IEnumerable<string> enumerable2 = list;
                string[] array = (enumerable2 ?? readableProperties.Select((PropertyInfo s) => s.Name)).ToArray();
                string[] array2 = array;
                foreach (string name in array2)
                {
                    Type propertyType = readableProperties.Single((PropertyInfo s) => s.Name.Equals(name)).PropertyType;
                    dataTable.Columns.Add(name, Nullable.GetUnderlyingType(propertyType) ?? propertyType);
                }

                foreach (T obj in enumerable)
                {
                    dataTable.Rows.Add(array.Select((string s) => readableProperties.Single((PropertyInfo s2) => s2.Name.Equals(s)).GetValue(obj)).ToArray());
                }
            }

            return dataTable.AsTableValuedParameter(typeName);
        }
    }

}
