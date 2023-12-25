using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using BoutiqueEnLigne.DB;
using BurgerSpot.Views;
using BurgerSpot.ViewModel;
using BoutiqueEnLigne.Views;

namespace BoutiqueEnLigne
{
    public partial class App : Application
    {

        static DataBaseConnection database;
        public static DataBaseConnection DataBase
        {
            get
            {
                if (database == null)
                {
                    database = new DataBaseConnection(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "base.db3")
                    );
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LandingPage());
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