﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoutiqueEnLigne.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BoutiqueEnLigne.DB;
using System.Diagnostics;

namespace BoutiqueEnLigne.View.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Main : TabbedPage
    {
        private DataBaseConnection dataBase = App.DataBase;
        public List<Categorie> Categories
        {
            get;
            set;
        }

        public Main()
        {
            InitializeComponent();
            LoadCategories();
            LoadProducts();
        }

        private async void LoadCategories()
        {
            Categories = await dataBase.ObtenirCategories();
            categoryListView.ItemsSource = Categories;
            categoryPicker.ItemsSource = Categories.Select(c => c.Nom).ToList();
        }

        public async void LoadProducts()
        {
            var products = await dataBase.ObtenirProduits(); 
            productListView.ItemsSource = products;
        }

        public void AddProduct(object sender, EventArgs e)
        {
           
            Produit p = new Produit
            {
                Nom = nameEntry.Text,
                Description = descriptionEntry.Text,
                Prix = decimal.Parse(prixEntry.Text),
                UrlImage = urlImageEntry.Text
            };
            foreach (Categorie cat in Categories)
            {
                if (cat.Nom == categoryPicker.SelectedItem.ToString())
                {
                    p.IdCategorie = cat.Id;
                }
            }

            try { 
                dataBase.AjouterProduit(p);
                DisplayAlert("Success", "Product added successfully!", "OK");
                LoadProducts();
            }
            catch (Exception ex)
            {
                DisplayAlert("Failure", ex.Message, "OK");
            }
        }

        public void AddCategorie(object sender, EventArgs e)
        {
            var v = true;
            Categorie c = new Categorie
            {
                Nom = categorieEntry.Text,
                img = categorieEntry1.Text // Ajoutez l'URL de l'image
            };

            foreach (Categorie cat in Categories)
            {
                if (cat.Nom == c.Nom)
                {
                    DisplayAlert("Denied", "A Categorie with the same name exists", "OK");
                    v = false;
                    break;
                }
            }

            if (v)
            {
                try
                {
                    dataBase.AddCategorie(c);
                    DisplayAlert("Success", "The New Categorie has been added", "OK");
                    categorieEntry.Text = "";
                    categorieEntry1.Text = ""; // Efface également l'URL de l'image après l'ajout
                    LoadCategories();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error:", ex.Message);
                }
            }
        }


        public void RemoveProduct(object sender,EventArgs e)
        {
            var item = sender as SwipeItem;
            var product = item.CommandParameter as Produit;
            dataBase.SupprimerProduit(product.Id);
            LoadProducts();
        }
    }
}