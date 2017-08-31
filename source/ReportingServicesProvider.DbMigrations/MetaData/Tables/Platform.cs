namespace ReportingServicesProvider.DbMigrations.MetaData.Tables
{
    public static class Platform
    {
        public const string TableName = "ReportingPlatform";
        public const string TableDescription = "Contains list of supported reporting systems.";
        public const string IdColumn = DefaultValues.DefaultIdColumn;
        public const string NameColumn = DefaultValues.DefaultNameColumn;
        public const string NameDescription = "Identifiable rescind alias of the platform. Is unique across registered reporting platforms.";
    }
}