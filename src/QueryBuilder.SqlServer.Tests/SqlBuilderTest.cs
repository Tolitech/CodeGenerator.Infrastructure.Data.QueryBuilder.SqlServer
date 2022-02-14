using System;
using Xunit;
using Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.SqlServer.Tests.Domain.Entities;

namespace Tolitech.CodeGenerator.Infrastructure.Data.QueryBuilder.SqlServer.Tests
{
    public class SqlBuilderTest
    {
        public SqlBuilderTest()
        {
            SqlBuilderConfiguration.UseSqlServer();
        }

        [Fact(DisplayName = "SqlBuilder - Insert - Valid")]
        public void SqlBuilder_Insert_Valid()
        {
            var person = new PersonEntity()
            {
                PersonId = 1,
                Name = "Person 1",
                Age = 18
            };

            string sql = new SqlBuilder()
                .Insert("dbo", "Person")
                .AddColumns(person)
                .Build();

            string expected = "insert into [dbo].[Person] ([PersonId], [Name], [Age]) values (@PersonId, @Name, @Age);";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - InsertWithIdentity - Valid")]
        public void SqlBuilder_InsertWithIdentity_Valid()
        {
            var person = new PersonEntity()
            {
                PersonId = null,
                Name = "Person 1",
                Age = 18
            };

            string sql = new SqlBuilder()
                .Insert("Person")
                .AddColumns(person)
                .Identity("PersonId")
                .Build();

            string expected = "insert into [dbo].[Person] ([Name], [Age]) values (@Name, @Age); select cast(SCOPE_IDENTITY() as int);";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - Update - Valid")]
        public void SqlBuilder_Update_Valid()
        {
            var person = new PersonEntity()
            {
                PersonId = 1,
                Name = "Person 1",
                Age = 18
            };

            string sql = new SqlBuilder()
                .Update("dbo", "Person")
                .AddColumns(person)
                .RemoveColumn(nameof(person.PersonId))
                .Where()
                .AddColumn(nameof(person.PersonId))
                .Build();

            string expected = "update [dbo].[Person] set [Name] = @Name, [Age] = @Age where [PersonId] = @PersonId;";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - Delete - Valid")]
        public void SqlBuilder_Delete_Valid()
        {
            string sql = new SqlBuilder()
                .Delete("dbo", "Person")
                .Where()
                .AddColumn("PersonId")
                .AddCondition("and [Age] > @Age")
                .Build();

            string expected = "delete from [dbo].[Person] where [PersonId] = @PersonId and [Age] > @Age;";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - DeleteWithoutSchema - Valid")]
        public void SqlBuilder_DeleteWithoutSchema_Valid()
        {
            string sql = new SqlBuilder()
                .Delete("Person")
                .Where()
                .AddColumn("PersonId")
                .AddCondition("and [Age] > @Age")
                .Build();

            string expected = "delete from [dbo].[Person] where [PersonId] = @PersonId and [Age] > @Age;";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - Select - Valid")]
        public void SqlBuilder_Select_Valid()
        {
            string sql = new SqlBuilder()
                .Select("dbo", "Person")
                .AddColumns("PersonId", "Name")
                .Where()
                .AddColumn("PersonId")
                .AddCondition("and [Age] > @Age")
                .Build();

            string expected = "select [PersonId], [Name] from [dbo].[Person] where [PersonId] = @PersonId and [Age] > @Age;";

            Assert.Equal(expected, sql);
        }

        [Fact(DisplayName = "SqlBuilder - SelectAsterisk - Valid")]
        public void SqlBuilder_SelectAsterisk_Valid()
        {
            string sql = new SqlBuilder()
                .Select("Person")
                .Where()
                .AddColumn("PersonId")
                .AddCondition("and [Age] > @Age")
                .Build();

            string expected = "select * from [dbo].[Person] where [PersonId] = @PersonId and [Age] > @Age;";

            Assert.Equal(expected, sql);
        }
    }
}
