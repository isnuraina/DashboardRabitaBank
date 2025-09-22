namespace DashboardRabitaBank.Settings
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
        public string AppDatabaseName { get; set; }

        // Collections
        public string RabitaBankCollection { get; set; }
        public string RabitaBankCorporateCollection { get; set; }
    }
}
