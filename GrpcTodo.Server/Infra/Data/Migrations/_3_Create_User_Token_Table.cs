using FluentMigrator;

namespace Core.Infra.Data.Migrations;

[Migration(3)]
public sealed class _3_Create_User_Token_Table : Migration
{
    public override void Down()
    {
        Delete.Column("token")
            .FromTable("user");
    }

    public override void Up()
    {
        Alter.Table("user")
            .AddColumn("token")
            .AsGuid()
            .WithDefaultValue(RawSql.Insert("gen_random_uuid()"))
            .NotNullable();
    }
}
