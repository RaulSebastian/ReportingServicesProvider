using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using ReportingServicesProvider.DbMigrations.MetaData;

namespace ReportingServicesProvider.DbMigrations
{
    public static class Extensions
    {
        private static ICreateTableWithColumnOrSchemaSyntax WithForeignKeyDescription(
            this ICreateTableColumnOptionOrWithColumnSyntax column, string primaryTableName, string primaryColumnName,
            string primaryTableSchema = null)
            => column.WithDescription(
                $"References the table '{primaryTableSchema ?? string.Empty}{primaryTableName}' column '{primaryColumnName}'");

        public static ICreateTableColumnOptionOrWithColumnSyntax WithSystemColumns(
            this ICreateTableWithColumnSyntax table)
            => table.WithColumn(DefaultValues.SystemColumns.Created)
                        .AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                    .WithColumn(DefaultValues.SystemColumns.Modified)
                        .AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                    .WithColumn(DefaultValues.SystemColumns.Active)
                        .AsBoolean().NotNullable().WithDefaultValue(true);

        public static ICreateTableWithColumnOrSchemaSyntax WithIdColumnDescription(
            this ICreateTableColumnOptionOrWithColumnSyntax column)
            => column.WithDescription(DefaultValues.DefaultIdColumnDescription);

        public static ICreateTableWithColumnOrSchemaSyntax ForeignKeyWithDescription(
            this ICreateTableColumnOptionOrWithColumnSyntax column, string foreignKeyName, string primaryTableName,
            string primaryColumnName)
            => column.ForeignKey(foreignKeyName, primaryTableName, primaryColumnName)
                .WithColumnDescription($"References the table '{primaryTableName}' column '{primaryColumnName}'");

        public static ICreateTableWithColumnOrSchemaSyntax ForeignKeyWithDescription(
            this ICreateTableColumnOptionOrWithColumnSyntax column, string foreignKeyName, string primaryTableSchema,
            string primaryTableName, string primaryColumnName)
            => column.ForeignKey(foreignKeyName, primaryTableSchema, primaryTableName, primaryColumnName)
                .WithColumnDescription($"References the table '{primaryTableName}' column '{primaryColumnName}'");

        public static ICreateTableWithColumnOrSchemaSyntax ForeignKeyWithDescription(
            this ICreateTableColumnOptionOrWithColumnSyntax column, string primaryTableName, string primaryColumnName)
            => column.ForeignKey(primaryTableName, primaryColumnName)
                .WithColumnDescription($"References the table '{primaryTableName}' column '{primaryColumnName}'");
    }
}