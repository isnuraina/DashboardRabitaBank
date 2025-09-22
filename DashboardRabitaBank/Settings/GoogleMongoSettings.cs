namespace DashboardRabitaBank.Settings
{
    public class GoogleMongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; } // app_db
        public string RabitaBankCollection { get; set; } = "google_com.rabitabank";
        public string RabitaBankCorporateCollection { get; set; } = "google_com.rabitabank.corporate";
    }
}
