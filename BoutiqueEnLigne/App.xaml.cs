using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueEnLigne
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new FlyoutPage1();
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
