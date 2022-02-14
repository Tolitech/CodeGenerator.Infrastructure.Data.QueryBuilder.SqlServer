using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.SqlServer
{
    public static class SqlBuilderConfiguration
    {
        public static void UseSqlServer()
        {
            AddQueryBuilder("default");
        }

        public static void AddQueryBuilder(string key)
        {
            var builderFactory = new SqlServerBuilderFactory();
            SqlBuilder.AddQueryBuilder(key, builderFactory);
        }
    }
}
