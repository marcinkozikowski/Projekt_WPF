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
    /// Interaction logic for RentalWindow.xaml
    /// </summary>
    public partial class RentalWindow : Window
    {
        Rent rent;

        public RentalWindow(Rent rent)
        {
            InitializeComponent();
            this.rent = rent;
            ClientGrid.DataContext = rent.RentingPerson;
            MainRentalGrid.DataContext = rent;
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
