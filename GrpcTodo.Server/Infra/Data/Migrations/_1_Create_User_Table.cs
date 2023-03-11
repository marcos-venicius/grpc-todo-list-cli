using FluentMigrator;

namespace GrpcTodo.Server.Infra.Data.Migrations;

[Migration(1)]
public sealed class _1_Create_User_Table : Migration
{
    private const string TableName = "user";

    public override void Down()
    {
        Delete.Table(TableName);
    }

    public override void Up()
    {
        Create.Table(TableName)
            .WithColumn("id")
                .AsGuid()
                .WithDefaultValue(RawSql.Insert("gen_random_uuid()"))
                .NotNullable()
                .PrimaryKey()
            .WithColumn("username").AsString(50).NotNullable()
            .WithColumn("password").AsString(64).NotNullable();
    }
}