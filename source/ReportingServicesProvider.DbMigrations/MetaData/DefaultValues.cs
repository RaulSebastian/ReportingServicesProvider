namespace ReportingServicesProvider.DbMigrations.MetaData
{
    public static class DefaultValues
    {
        public const string DefaultIdColumn = "Id";
        public const string DefaultIdColumnDescription = "The unique identifier of the entity.";
        public const string DefaultNameColumn = "Name";

        public static class SystemColumns
        {
            public const string Created = "Created";
            public const string Modified = "Modified";
            public const string Active = "Active";
        }
    }
}