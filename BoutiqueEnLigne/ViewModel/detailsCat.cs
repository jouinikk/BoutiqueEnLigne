using BoutiqueEnLigne.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BurgerSpot.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class detailsCat : ContentPage
    {
   
        private int position;
        public int Position
        {
            get
            {
                if (position != Categories.IndexOf(SelectedCat))
                    return Categories.IndexOf(SelectedCat);

                return position;
            }
            set
            {
                position = value;
                SelectedCat = Categories[position];

                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedCat));
            }
        }

        public ICommand ChangePositionCommand => new Command(ChangePosition);

        public Categorie SelectedCat { get; internal set; }
        public ObservableCollection<Categorie> Categories { get; internal set; }

        private void ChangePosition(object obj)
        {
            string direction = (string)obj;

            if (direction == "L")
            {
                if (position == 0)
                {
                    Position = Categories.Count - 1;
                    return;
                }

                Position -= 1;
            }
            else if (direction == "R")
            {
                if (position == Categories.Count - 1)
                {
                    Position = 0;
                    return;
                }

                Position += 1;
            }
        }
    }
}