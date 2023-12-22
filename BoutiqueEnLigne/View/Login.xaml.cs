using System;
using Xamarin.Forms;

namespace BoutiqueEnLigne.View
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            if (username == "admin" && password == "admin")
            {
                DisplayAlert("Login Successful", "Welcome, Admin!", "OK");

                // Navigate to a new page (you can replace MainPage with your desired page)
                App.Current.MainPage = new MainPage();
            }
            else
            {
                DisplayAlert("Login Failed", "Invalid username or password", "OK");
            }
        }
    }
}
