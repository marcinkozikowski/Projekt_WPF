using Projekt_WPF_Solution.Commands;
using Projekt_WPF_Solution.DataBaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekt_WPF_Solution
{
    /// <summary>
    /// Interaction logic for SearchRentalWindow.xaml
    /// </summary>
    public partial class SearchRentalWindow : Window
    {
        private ListCollectionView rentsView;

        public SearchRentalWindow()
        {
            rentsView = new ListCollectionView(SqlDataGetters.Rents);
            InitializeComponent();
            RentListBox.ItemsSource = rentsView;
            Loaded += delegate
            {
                MyCommands.BindCommands(this);
            };
        }

        private void SearchRentsButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchRentTextBox.Text.Equals(string.Empty) && (OdDatePicker.SelectedDate ==null && DoDatePicker == null))
            {
                rentsView.Filter = null;
            }
            else
            {
                rentsView.Filter = delegate (object item)
                {
                    Rent rent = item as Rent;
                    if (rent != null)
                    {
                        if (NameSurnameButton.IsChecked == true)
                        {
                            return rent.RentingPerson.NameSurname.ToLower().Equals(SearchRentTextBox.Text.ToLower());
                        }
                        else if (IdRadioButton.IsChecked == true)
                        {
                            return rent.ID.Equals(Int32.Parse(SearchRentTextBox.Text));

                        }
                        else if (RentalTimeRadioButton.IsChecked == true)
                        {
                            return (rent.RentStart.Date>=OdDatePicker.SelectedDate && rent.RentEnd<= DoDatePicker.SelectedDate);
                        }

                    }
                    return false;
                };
            }
        }

        private void NoneRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            rentsView.Filter = null;
        }

        private void CloseSearchRentButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
