namespace ReportingServicesProvider.DbMigrations.MetaData.Tables
{
    public static class Server
    {
        public const string TableName = "ReportingServer";
        public const string TableDescription = "Stores definitions of registered reporting servers.";
        public const string IdColumn = DefaultValues.DefaultIdColumn;
        public const string NameColumn = DefaultValues.DefaultNameColumn;
        public const string NameDescription = "Identifiable rescind alias of the server. Is unique across registered servers.";
        public const string ReportingPlatformColumn = "ReportingPlatformId";
        public const string UrlColumn = "Url";
        public const string UrlDescription = "The root url for the report server."; //TODO: evaluate description
    }
}