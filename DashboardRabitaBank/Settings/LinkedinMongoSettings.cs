namespace DashboardRabitaBank.Settings
{
    public class LinkedinMongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; } // linkedin_db
        public string RabitaBankCollectionLinkedinName { get; set; } = "rabitabank-ojsc";
    }
}
