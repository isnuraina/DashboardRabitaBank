namespace DashboardRabitaBank.Settings
{
    public class AppMongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; } // app_db
        public string RabitaBankCollectionBusiness { get; set; } = "apple_rabita_business";
        public string RabitaBankCollectionMobile { get; set; } = "apple_rabita_mobile";
    }
}
