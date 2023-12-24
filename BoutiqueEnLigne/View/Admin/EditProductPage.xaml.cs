using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueEnLigne.DB;
using BoutiqueEnLigne.Models;
using Xamarin.Forms;
namespace BoutiqueEnLigne.View.Admin
{
    public partial class EditProductPage : ContentPage
    {
        private readonly DataBaseConnection dataBase = App.DataBase;
        public Produit product;
        public List<Categorie> categories { get; set; }
        public EditProductPage(Produit selectedProduct)
        {
            InitializeComponent();
            product = selectedProduct;
            LoadData();
        }
        private async void LoadData()
        {
            categories = await dataBase.ObtenirCategories();
            editCategoryPicker.ItemsSource = categories.Select(c => c.Nom).ToList();
            editNameEntry.Text = product.Nom;
            editDescriptionEntry.Text = product.Description;
            editPrixEntry.Text = product.Prix.ToString();
            editUrlImageEntry.Text = product.UrlImage;
            editCategoryPicker.SelectedItem = categories.FirstOrDefault(c => c.Id == product.IdCategorie)?.Nom;
        }
        private async void EditProduct(object sender, EventArgs e)
        {
            product.Nom = editNameEntry.Text;
            product.Description = editDescriptionEntry.Text;
            product.Prix = decimal.Parse(editPrixEntry.Text);
            product.UrlImage = editUrlImageEntry.Text;
            foreach (Categorie cat in await dataBase.ObtenirCategories())
            {
                if (cat.Nom == editCategoryPicker.SelectedItem.ToString())
                {
                    product.IdCategorie = cat.Id;
                }
            }
            try
            {
                dataBase.ModifierProduit(product);
                _ = await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Failure", ex.Message, "OK");
            }
        }
    }
}