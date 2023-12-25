using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using BoutiqueEnLigne.Models;
using BoutiqueEnLigne.View;
using BurgerSpot.Views;
using BoutiqueEnLigne;
using BoutiqueEnLigne.DB;
using BoutiqueEnLigne.Views;

namespace BurgerSpot.ViewModel
{
    public class LandingViewModel : BaseViewModel
    {
        private ObservableCollection<Categorie> categories;
        private Categorie selectedCategory;
        private DataBaseConnection dataBase = App.DataBase;
        public ICommand NavigateToLoginPageCommand { get; set; }
        public ICommand NavigateToPanierPageCommand { get; set; }
        public ICommand DeleteCategoryCommand { get; set; }

        public ObservableCollection<Categorie> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }

        public Categorie SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                OnPropertyChanged();
                if (selectedCategory != null)
                {
                    NavigateToProductListPage(selectedCategory.Id);
                }
            }
        }
       /* private void DeleteAllCategories()
        {
            try
            {
                var allCategories = dataBase.ObtenirCategories().Result;
                foreach (var category in allCategories)
                {
                    dataBase.SupprimerCategorie(category.Id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la suppression des catégories : {ex.Message}");
            }
        }*/


        public LandingViewModel()
        {
            LoadCategories();
            NavigateToLoginPageCommand = new Command(NavigateToLoginPage);
            NavigateToPanierPageCommand = new Command(NavigateToPanierPage);
           //  DeleteAllCategories();

        }
       /* private void DeleteCategory(Categorie category)
        {
            if (category != null)
            {
                Categories.Remove(category);
                // Ajoutez ici la logique pour supprimer la catégorie de la base de données
                // dataBase.SupprimerCategorie(category.Id);
            }
        }
       */

        private async void LoadCategories()
        {
            Categories = new ObservableCollection<Categorie>(await dataBase.ObtenirCategories());
        }

        private void NavigateToProductListPage(int categoryId)
        {
            var productViewModel = new ProductListViewModel(categoryId);
            var productListPage = new Views.ProductListPage { BindingContext = productViewModel };
            Application.Current.MainPage.Navigation.PushAsync(productListPage, true);
        }

        private void NavigateToLoginPage()
        {
            // Utilisez la navigation de Xamarin.Forms pour aller à la page LoginPage
            App.Current.MainPage.Navigation.PushAsync(new PanierPage());
        }
        private void NavigateToPanierPage()
        {
            // Utilisez la navigation de Xamarin.Forms pour aller à la page LoginPage
            App.Current.MainPage.Navigation.PushAsync(new PanierPage());
        }
    }
}
