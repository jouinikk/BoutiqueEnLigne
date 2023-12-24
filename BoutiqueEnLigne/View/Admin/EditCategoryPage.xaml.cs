using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoutiqueEnLigne.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BoutiqueEnLigne.DB;

namespace BoutiqueEnLigne.View.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCategoryPage : ContentPage
    {
        private DataBaseConnection dataBase =App.DataBase;
        public Categorie categorie;
        public EditCategoryPage(Categorie SelectedCategorie)
        {
            InitializeComponent();
            categorie = SelectedCategorie;
            editNameEntry.Text = categorie.Nom;
        }

        public async void EditCategory(object Sender,EventArgs e)
        {
            categorie.Nom = editNameEntry.Text;
            try
            {
                 dataBase.ModifierCategorie(categorie);
                await Navigation.PopModalAsync();
                
            }catch(Exception ex)
            {
                DisplayAlert("error",ex.Message,"a");
            }
        }
        


    }
}