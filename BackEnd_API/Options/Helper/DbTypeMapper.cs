using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Options.Helper
{
    public static class DbTypeMapper
    {
        private struct DbTypeMapEntry
        {
            public Type Type;

            public DbType DbType;

            public DbTypeMapEntry(Type type, DbType dbType)
            {
                Type = type;
                DbType = dbType;
            }
        }

        private static List<DbTypeMapEntry> _DbTypeMapping;

        static DbTypeMapper()
        {
            _DbTypeMapping = new List<DbTypeMapEntry>();
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(byte[]), DbType.Binary));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(byte?[]), DbType.Binary));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(bool), DbType.Boolean));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(string), DbType.String));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(char[]), DbType.String));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(DateTime), DbType.DateTime));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(DateTime), DbType.DateTime));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(DateTimeOffset), DbType.DateTimeOffset));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(DateTimeOffset?), DbType.DateTimeOffset));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(decimal), DbType.Decimal));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(decimal?), DbType.Decimal));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(double), DbType.Double));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(double?), DbType.Double));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(short), DbType.Int16));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(int), DbType.Int32));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(long), DbType.Int64));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(short?), DbType.Int16));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(int?), DbType.Int32));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(long?), DbType.Int64));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(float), DbType.Single));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(float?), DbType.Single));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(TimeSpan), DbType.Time));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(TimeSpan?), DbType.Time));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(object), DbType.Object));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(byte), DbType.Byte));
            _DbTypeMapping.Add(new DbTypeMapEntry(typeof(byte?), DbType.Byte));
        }

        public static DbType TryGetDbType(this Type type)
        {
            Type type2 = type;
            return (from o in _DbTypeMapping
                    where o.Type == type2
                    select o.DbType).FirstOrDefault();
        }
    }
}
