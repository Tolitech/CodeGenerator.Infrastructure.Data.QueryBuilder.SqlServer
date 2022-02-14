﻿using System;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.SqlServer
{
    public class SqlBuilderSelect : QueryBuilder.SqlBuilderSelect
    {
        public SqlBuilderSelect(IBuilderFactory builderFactory, string schemaName, string tableName) : base(builderFactory, schemaName, tableName)
        {

        }

        public override string Build()
        {
            string sql = $"select {GetSqlColumnsToSelect()} from [{schemaName}].[{tableName}]{where.BuildWhere()};";
            return sql;
        }

        private string GetSqlColumnsToSelect()
        {
            if (base.columns.Any())
            {
                IList<string> columnsRenamed = new List<string>();
                foreach (var column in base.columns)
                {
                    string columnRenamed = base.GetColumnName(column);
                    string asName = column == columnRenamed ? "" : $" as [{column}]";
                    columnsRenamed.Add($"[{columnRenamed}]{asName}");
                }

                return string.Join(", ", columnsRenamed);
            }

            return "*";
        }
    }
}
