using Dapper;
using Options.Attributes;
using System.Data;
using System.Reflection;

namespace Options.Helper
{
    internal class DynamicParametersBuilder
    {
        public static DynamicParameters Parse<TRequest>(TRequest entity)
        {
            DynamicParameters parameter = new DynamicParameters();

            if (entity == null)
            {
                throw new ArgumentNullException("entity is null in DynamicParametersBuilder.Parse Method");
            }
            var properties = entity.GetType().GetProperties().Where(x => !(x.DeclaringType is TRequest));

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(entity, null);
                string propertyName = property.Name;
                string propertyDataType = property.PropertyType.Name;
                var dbType = property.PropertyType.TryGetDbType();
                string fullName = property.PropertyType.FullName ?? string.Empty;
                if (propertyDataType.Contains("DateTime") || fullName.Contains("DateTime"))
                {
                    dbType = typeof(DateTime).TryGetDbType();
                }

                var attribute = property.GetCustomAttributes<DBParameterAttribute>(true).SingleOrDefault();

                System.Data.ParameterDirection direction = attribute == null ? System.Data.ParameterDirection.Input : attribute.ParameterDirection;

                int? size = attribute == null || attribute.Size <= 0 ? (int?)null : attribute.Size;

                if (!propertyDataType.Contains("List"))
                {
                    parameter.Add(
                   name: propertyName,
                   value: propertyValue,
                   dbType: dbType,
                   direction: direction,
                   size: size
                   );
                }
                else
                {
                    if (propertyValue != null)
                    {
                        Type listElementType = property.PropertyType.GetGenericArguments().Single();

                        var _openMethod = typeof(ListAsTableValuedParameterExtensions).GetMethod("AsTableValuedParameter");
                        if (_openMethod != null)
                        {
                            MethodInfo openMethod = _openMethod;
                            MethodInfo typeMethod = openMethod.MakeGenericMethod(listElementType);
                            if (attribute?.ParameterType != null)
                            {
                                propertyValue = typeMethod.Invoke(null, new object[] { propertyValue, attribute.ParameterType });
                            }
                            parameter.Add(name: propertyName, value: propertyValue);
                        }
                    }
                }
            }
            return parameter;
        }
    }
}
