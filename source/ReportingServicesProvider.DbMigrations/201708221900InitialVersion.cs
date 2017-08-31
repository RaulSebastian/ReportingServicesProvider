using FluentMigrator;
using FluentMigrator.Runner.Extensions;
using Tables = ReportingServicesProvider.DbMigrations.MetaData.Tables;

namespace ReportingServicesProvider.DbMigrations
{
    [Migration(201708221900,"Initial Version")]
    public class InitialVersion : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table(Tables.Platform.TableName)
                .WithDescription(Tables.Platform.TableDescription)
                .WithColumn(Tables.Platform.IdColumn).AsInt32().Identity().PrimaryKey()
                    .WithIdColumnDescription()
                .WithColumn(Tables.Platform.NameColumn).AsString().NotNullable()
                    .WithColumnDescription(Tables.Platform.NameDescription);

            Insert.IntoTable(Tables.Platform.TableName)
                .Row(new {Id = 1, Name = "SqlServerReportingServices"})
                .WithIdentityInsert();

            Create.Table(Tables.Server.TableName)
                .WithDescription(Tables.Server.TableDescription)
                .WithColumn(Tables.Server.IdColumn).AsInt32().Identity().PrimaryKey()
                    .WithIdColumnDescription()
                .WithColumn(Tables.Server.NameColumn).AsString().Unique().NotNullable()
                    .WithColumnDescription(Tables.Platform.NameDescription)
                .WithColumn(Tables.Server.ReportingPlatformColumn).AsInt32().NotNullable()
                    .ForeignKeyWithDescription(Tables.Platform.TableName, Tables.Platform.IdColumn)
                .WithColumn(Tables.Server.UrlColumn).AsString().NotNullable()
                    .WithColumnDescription(Tables.Server.UrlDescription)
                .WithSystemColumns();

            Create.Table(Tables.Report.TableName)
                .WithDescription(Tables.Report.TableDescription)
                .WithColumn(Tables.Report.IdColumn).AsInt32().Identity().PrimaryKey()
                    .WithIdColumnDescription()
                .WithColumn(Tables.Report.NameColumn).AsString().NotNullable()
                    .WithDescription(Tables.Report.NameDescription)
                .WithColumn(Tables.Report.ServerColumn).AsInt32().NotNullable()
                    .ForeignKeyWithDescription(Tables.Server.TableName, Tables.Server.IdColumn)
                .WithColumn(Tables.Report.PathColumn).AsString().NotNullable()
                    .WithColumnDescription(Tables.Report.PathDescription)
                //TODO: check if last exec / call is needed as attribute
                .WithSystemColumns();
        }
    }
}