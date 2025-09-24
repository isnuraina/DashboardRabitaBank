namespace DashboardRabitaBank.Settings
{
    public class FacebookMongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; } // fb_db
        public string RabitaBankCollectionFacebookName { get; set; } = "rabita.mobile";
    }
}
