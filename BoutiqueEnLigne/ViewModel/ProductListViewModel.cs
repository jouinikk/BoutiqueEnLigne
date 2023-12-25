// ProductListViewModel.cs
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using BoutiqueEnLigne.DB;
using BoutiqueEnLigne.Models;
using BoutiqueEnLigne;
using BurgerSpot.Views;
using BoutiqueEnLigne.View;

namespace BurgerSpot.ViewModel
{
    public class ProductListViewModel : BaseViewModel
    {
        private ObservableCollection<Produit> products;
        private Produit selectedProduct;
        private DataBaseConnection dataBase = App.DataBase;

        public ObservableCollection<Produit> Products
        {
            get { return products; }
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }

        public Produit SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged();
                // Handle the selection if needed
            }
        }
        public ProductListViewModel()
        {
           // InitializeComponent();
        }

        public ProductListViewModel(int categoryId)
        {
            LoadProductsForCategory(categoryId);

        }

        private async void LoadProductsForCategory(int categoryId)
        {
            var productsForCategory = await dataBase.ObtenirProduit(categoryId);
            Device.BeginInvokeOnMainThread(() =>
            {
                Products = new ObservableCollection<Produit>(productsForCategory);
            });
        }
        public ICommand SelectionCommand => new Command(DisplayProduit);

        private async void DisplayProduit()
        {
            if (selectedProduct != null)
            {
                var viewModel = new DetailsViewModel
                {
                    SelectedProduit = selectedProduct,
                    Produits = products,
                    Position = products.IndexOf(selectedProduct)
                };
                var detailsPage = new DetailsPage { BindingContext = viewModel };

                var navigation = Application.Current.MainPage as NavigationPage;
                navigation.PushAsync(detailsPage, true);
            }
        }
        private void NavigateToLoginPage()
        {
            // Utilisez la navigation de Xamarin.Forms pour aller à la page LoginPage
            App.Current.MainPage.Navigation.PushAsync(new Login());
        }

    }
}
