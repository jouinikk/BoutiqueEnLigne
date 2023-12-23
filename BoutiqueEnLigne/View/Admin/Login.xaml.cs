using System;
using Xamarin.Forms;
using BoutiqueEnLigne.View.Admin;
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
                usernameEntry.Text = "";
                passwordEntry.Text = "";
                Navigation.PushModalAsync(new Main());
            }
            else
            {
                usernameEntry.Text = "";
                passwordEntry.Text = "";
                DisplayAlert("Login Failed", "Invalid username or password", "OK");
            }
        }
    }
}
