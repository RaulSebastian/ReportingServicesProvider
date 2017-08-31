namespace ReportingServicesProvider.DbMigrations.MetaData.Tables
{
    public static class Report
    {
        public const string TableName = "Report";
        public const string TableDescription = "Stores definitions of registered reports.";
        public const string IdColumn = DefaultValues.DefaultIdColumn;
        public const string NameColumn = DefaultValues.DefaultNameColumn;
        public const string NameDescription = "Name of the report. A report's name is only unique within a path on a server.";
        public const string ServerColumn = "ReportingServerId";
        public const string PathColumn = "Path";
        public const string PathDescription = "The path to the report on the server.";
    }
}