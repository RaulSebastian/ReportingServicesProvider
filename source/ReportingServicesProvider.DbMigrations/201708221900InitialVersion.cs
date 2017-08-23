using FluentMigrator;
using FluentMigrator.Runner.Extensions;


namespace ReportingServicesProvider.DbMigrations
{
    [Migration(201708221900,"Initial Version")]
    public class InitialVersion : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table(MetaData.Tables.ReportingPlatform.TableName)
                    .WithDescription(MetaData.Tables.ReportingPlatform.Description)
                .WithColumn(MetaData.Tables.ReportingPlatform.IdColumn).AsInt32().Identity().PrimaryKey()

                .WithColumn(MetaData.Tables.ReportingPlatform.NameColumn).AsString().NotNullable();

            Insert.IntoTable(MetaData.Tables.ReportingPlatform.TableName)
                .Row( new { Id = 1, Name = "SqlServerReportingServices" })
                .WithIdentityInsert();

            Create.Table("ReportingServer")
                .WithDescription("Stores definitions of registered reporting servers.")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                    .WithColumnDescription("")
                .WithColumn("Name").AsString().Unique().NotNullable()
                .WithColumn("ReportingPlatform").AsInt32().NotNullable()
                    .ForeignKey(MetaData.Tables.ReportingPlatform.TableName, MetaData.Tables.ReportingPlatform.IdColumn)
                    .WithColumnDescription("")
                .WithColumn("Url").AsString().NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable().WithDefaultValue("SYSDATETIME()") //TODO: move to custom fluent extensions
                .WithColumn("Modified").AsDateTime().NotNullable().WithDefaultValue("SYSDATETIME()");
        }
    }
}