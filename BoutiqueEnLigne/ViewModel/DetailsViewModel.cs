using BoutiqueEnLigne;
using BoutiqueEnLigne.DB;
using BoutiqueEnLigne.Models;
using BoutiqueEnLigne.View;
using BoutiqueEnLigne.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BurgerSpot.ViewModel
{
    public class DetailsViewModel : BaseViewModel

    {

        public ICommand NavigateToLoginPageCommand => new Command(NavigateToLoginPage);
        public ICommand NavigateToCartPageCommand => new Command(NavigateToCartPage);

        private async void NavigateToLoginPage()
        {
            App.Current.MainPage.Navigation.PushAsync(new Login());
        }

        private async void NavigateToCartPage()
        {
            try
            {
                App.Current.MainPage.Navigation.PushAsync(new PanierPage());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation Error: {ex.Message}");
            }
        }
        private int position;
        private DataBaseConnection database = App.DataBase;
        private int _quantity = 1;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }
        public ICommand MinusQuantityCommand => new Command(MinusQuantity);
        public ICommand PlusQuantityCommand => new Command(PlusQuantity);

        private void PlusQuantity()
        {
            Quantity++;
        }

        private void MinusQuantity()
        {
            if (Quantity > 1)
            {
                Quantity--;
            }
        }

        public int Position
        {
            get
            {
                if (position != Produits.IndexOf(SelectedProduit))
                    return Produits.IndexOf(SelectedProduit);

                return position;
            }
            set
            {
                position = value;
                SelectedProduit = Produits[position];

                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedProduit));
            }
        }

        public ICommand ChangePositionCommand => new Command(ChangePosition);

        public Produit SelectedProduit { get; internal set; }
        public ObservableCollection<Produit> Produits { get; internal set; }

        private void ChangePosition(object obj)
        {
            string direction = (string)obj;

            if (direction == "L")
            {
                if (position == 0)
                {
                    Position = Produits.Count - 1;
                    return;
                }

                Position -= 1;
            }
            else if (direction == "R")
            {
                if (position == Produits.Count - 1)
                {
                    Position = 0;
                    return;
                }

                Position += 1;
            }
        }

        public ICommand AddToCartCommand => new Command(AddToCart);
        // public async Task<List<LigneCommande>> ObtenirLignesCommandeAsync(int idCommande)


        private async void AddToCart()
        {
            // Vérifier si le produit est déjà dans le panier
            bool produitDejaDansLePanier = false;

            foreach (var ligneCommande in await database.ObtenirLignesCommande1Async())
            {
                if (ligneCommande.IdProduit == SelectedProduit.Id)
                {
                    produitDejaDansLePanier = true;
                    break;
                }
            }

            if (produitDejaDansLePanier)
            {
                // Afficher un message d'erreur si le produit est déjà dans le panier
                DisplayAlert("Erreur", "Le produit est déjà dans le panier.", "OK");
            }
            else
            {
                // Ajouter le produit au panier
                LigneCommande ligneCommande = new LigneCommande
                {
                    IdProduit = SelectedProduit.Id,
                    Quantite = Quantity
                };

                // Ajouter la ligne de commande à la base de données
                database.AjouterLigneCommande(ligneCommande);

                // Afficher un message de succès
                DisplayAlert("Succès", "Le produit a été ajouté au panier avec succès!", "OK");
               // await Application.Current.MainPage.Navigation.PushAsync(panierPage, true);


            }
        }
        // Méthode pour afficher une alerte (à implémenter dans la page)
        private void DisplayAlert(string title, string message, string cancel)
        {
            App.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }

   
}
