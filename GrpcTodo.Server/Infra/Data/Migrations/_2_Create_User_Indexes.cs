using FluentMigrator;

namespace GrpcTodo.Server.Infra.Data.Migrations;

[Migration(2)]
public sealed class _2_Create_User_Indexes : Migration
{
    public override void Down()
    {
        Delete.Index("username_unique")
            .OnTable("user");
    }

    public override void Up()
    {
        Create.Index("username_unique")
            .OnTable("user")
            .OnColumn("username")
            .Ascending()
            .WithOptions()
            .Unique();
    }
}