using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Salon2.Data;
using System.IO;

namespace Salon2
{
    public partial class App : Application
    {
        static SalonDB database;
        public static SalonDB Database
        {
            get
            {
                if (database == null)
                {
                    database = new
                   SalonDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.
                   LocalApplicationData), "Salon.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ListEntryPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
