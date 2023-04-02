using FluentMigrator;

namespace Core.Infra.Data.Migrations;

[Migration(4)]
public sealed class _4_Create_Task_Table : Migration
{
    public override void Down()
    {
        Delete.Table("task");
    }

    public override void Up()
    {
        Create.Table("task")
            .WithColumn("id")
                .AsGuid()
                .NotNullable()
                .PrimaryKey()
                .Unique()
            .WithColumn("user_id")
                .AsGuid()
                .NotNullable()
                .ForeignKey("user", "id")
            .WithColumn("name").AsString().NotNullable().Unique()
            .WithColumn("completed").AsBoolean().WithDefaultValue(false)
            .WithColumn("created_at").AsDateTimeOffset();
    }
}
