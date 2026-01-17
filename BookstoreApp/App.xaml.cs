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
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                using var db = new BookstoreContext();

                await db.Database.MigrateAsync();
                await DbSeeder.SeedAsync(db);
            }
            catch (Exception ex)
            {

                MessageBox.Show(
                    "Kunde inte initiera databasen.\n\n" + ex.Message,
                    "Databasfel",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                Shutdown();
            }
        }
    }

}
