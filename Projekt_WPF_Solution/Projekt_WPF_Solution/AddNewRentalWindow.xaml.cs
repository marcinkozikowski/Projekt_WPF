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
using Projekt_WPF_Solution.DataBaseClasses;
namespace Projekt_WPF_Solution
{
    /// <summary>
    /// Interaction logic for AddNewRentalWindow.xaml
    /// </summary>
    public partial class AddNewRentalWindow : Window
    {
        Rent rent;
        bool isEnabled;


        public AddNewRentalWindow(Rent rent, bool isEnabled)
        {
            InitializeComponent();
            this.rent = rent;
            ClientGrid.DataContext = rent.RentingPerson;
            this.isEnabled = isEnabled;
            MainRentalGrid.DataContext = rent;
            MainRentalGrid.IsEnabled = isEnabled;
        }

        private void SearchClientButton_Click(object sender, RoutedEventArgs e)
        {
            SearchClientWindow searchClientWindow = new SearchClientWindow();
            searchClientWindow.ShowDialog();
        }

        private void CancelRenatlButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
