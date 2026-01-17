using BookstoreApp.Infrastructure;
using BookstoreApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Windows;

namespace BookstoreApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool DatabaseReady { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            try
            {
                using var db = new BookstoreContext();
                await db.Database.MigrateAsync();
                await DbSeeder.SeedAsync(db);

                DatabaseReady = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Databasfel");
                Shutdown();
            }
        }
    }

}


