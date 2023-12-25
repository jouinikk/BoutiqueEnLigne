using BoutiqueEnLigne.DB;
using BoutiqueEnLigne.Models;
using BurgerSpot.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace BoutiqueEnLigne.ViewModel
{
    public class PanierViewModel : BaseViewModel
    {
        private ObservableCollection<LigneCommande> _lignesCommande;
        private DataBaseConnection _dataBase = App.DataBase;
        private string _nomClient; // Nouvelle propriété pour le nom du client

        public ObservableCollection<LigneCommande> LignesCommande
        {
            get { return _lignesCommande; }
            set
            {
                _lignesCommande = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public string NomClient
        {
            get { return _nomClient; }
            set
            {
                _nomClient = value;
                OnPropertyChanged();
            }
        }

        public decimal TotalPrice
        {
            get
            {
                if (LignesCommande == null)
                    return 0;

                decimal total = 0;

                foreach (var item in LignesCommande)
                {
                    if (item.Produit != null)
                    {
                        total += item.Produit.Prix * item.Quantite;
                    }
                }

                return total;
            }
        }


        public ICommand PlaceOrderCommand => new Command(AddCommande);

        private async void AddCommande()
        {
            // Vérifier si une commande avec le même nom de client existe déjà
            var existingCommand = await _dataBase.GetCommandeByNomClientAsync(NomClient);

            if (existingCommand != null)
            {
                // La commande existe déjà, vous pouvez traiter cela en conséquence
                await Application.Current.MainPage.DisplayAlert("Error", "A command with the same client name already exists.", "OK");
            }
            else
            {
                // La commande n'existe pas, créez une nouvelle commande
                var newCommand = new Commande
                {
                    NomClient = NomClient,
                    LignesCommande = new List<LigneCommande>(LignesCommande)
                };

                // Ajoutez la nouvelle commande à la base de données
                _dataBase.AjouterCommandeAsync(newCommand);

                // Affichez l'alerte de succès
                await Application.Current.MainPage.DisplayAlert("Success", "Order placed successfully.", "OK");
            }
        }


        public PanierViewModel()
        {
            LoadListeCommande();
        }

        private async void LoadListeCommande()
        {
            LignesCommande = new ObservableCollection<LigneCommande>(await _dataBase.ObtenirLignesCommande1Async());

            if (LignesCommande != null && LignesCommande.Any())
            {
                foreach (var ligneCommande in LignesCommande)
                {
                    // Chargez manuellement la propriété Produit
                    ligneCommande.Produit = await _dataBase.GetProduitByIdAsync(ligneCommande.IdProduit);
                }
            }

            OnPropertyChanged(nameof(TotalPrice));
        }
    }
}

