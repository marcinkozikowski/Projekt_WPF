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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Projekt_WPF_Solution.DataBaseClasses;
using Projekt_WPF_Solution.Commands;
using Projekt_WPF_Solution.Converters;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;
using System.ComponentModel;

namespace Projekt_WPF_Solution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ListCollectionView carsView, clientsView, rentsView;

        public MainWindow()
        {
            SqlDataGetters.GetAll();
            carsView = new ListCollectionView(SqlDataGetters.Cars);
            clientsView = new ListCollectionView(SqlDataGetters.Clients);
            rentsView = new ListCollectionView(SqlDataGetters.Rents);

            InitializeComponent();
            CarListBox.ItemsSource = carsView;
            ClientListBox.ItemsSource = clientsView;
            RentListBox.ItemsSource = rentsView;           

            Loaded += delegate
            {
                MyCommands.BindCommands(this);
            };
        }

        private void ContactMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow contactWindow = new ContactWindow();
            contactWindow.ShowDialog();
        }

        #region Buttons
        private void AddNewCar_Click(object sender, RoutedEventArgs e)
        {
            Car newCar = new Car();
            CarWindow newCarWindow = new CarWindow(newCar);
            if (newCarWindow.ShowDialog() == true)
            {
                if (!newCar.SqlInsert())
                {
                    MessageBox.Show("BŁĄD DODAWANIA");
                }
                else
                {
                    SqlDataGetters.GetAll();
                }
            }
        }
        private void AddNewClientButton_Click(object sender, RoutedEventArgs e)
        {
            Client newClient = new Client();
            ClientWindow newClientWindow = new ClientWindow(newClient);
            if (newClientWindow.ShowDialog() == true)
            {
                if (!newClient.SqlInsert())
                {
                    MessageBox.Show("BŁĄD DODAWANIA");
                }
                else
                {
                    SqlDataGetters.GetAll();
                }
            }
        }
        private void AddNewRentalButton_Click(object sender, RoutedEventArgs e)
        {
            Rent newRent = new Rent();
            RentalWindow newRenatlWindow = new RentalWindow(newRent);
            if(newRenatlWindow.ShowDialog() == true)
            {
                if(!newRent.SqlInsert())
                {
                    MessageBox.Show("Błąd dodawania");
                }
                else
                {
                    SqlDataGetters.GetAll();
                }
            }
        }

        



        private void SearchClientButton_Click(object sender, RoutedEventArgs e)
        {
            SearchClientWindow searchClientWindow = new SearchClientWindow();
            searchClientWindow.ShowDialog();
        }

        private void SearchCarButton_Click(object sender, RoutedEventArgs e)
        {
            SearchCarWindow searchCarWindow = new SearchCarWindow();
            searchCarWindow.ShowDialog();
        }

        #endregion

        #region Group
        private void GroupNone(object sender, RoutedEventArgs e)
        {
            carsView.GroupDescriptions.Clear();
            carsView.SortDescriptions.Clear();
        }
        #region GroupCar
        private void CarGroupBrand(object sender, RoutedEventArgs e)
        {
            carsView.GroupDescriptions.Clear();
            carsView.GroupDescriptions.Add(new PropertyGroupDescription("Maker"));
            carsView.SortDescriptions.Clear();
            carsView.SortDescriptions.Add(new SortDescription("Maker", ListSortDirection.Ascending));
        }

        private void CarGroupYear(object sender, RoutedEventArgs e)
        {
            carsView.GroupDescriptions.Clear();
            carsView.GroupDescriptions.Add(new PropertyGroupDescription("ManufacturedYear"));
            carsView.SortDescriptions.Clear();
            carsView.SortDescriptions.Add(new SortDescription("ManufacturedYear", ListSortDirection.Descending));
        }

        private void CarGroupType(object sender, RoutedEventArgs e)
        {
            carsView.GroupDescriptions.Clear();
            carsView.GroupDescriptions.Add(new PropertyGroupDescription("Type"));
            
        }
        #endregion
        #region GroupClient
        private void ClientGroupAge(object sender, RoutedEventArgs e)
        {
            Client c = new Client();
            clientsView.SortDescriptions.Clear();
            clientsView.SortDescriptions.Add(new SortDescription("Born.Year", ListSortDirection.Ascending));
            clientsView.GroupDescriptions.Clear();
            AgeRangeGrouper grouper = new AgeRangeGrouper();
            grouper.GroupInterval = 5;
            clientsView.GroupDescriptions.Add(new PropertyGroupDescription("Born.Year", grouper));
        }
        private void ClientGroupCity(object sender, RoutedEventArgs e)
        {
            clientsView.GroupDescriptions.Clear();
            clientsView.GroupDescriptions.Add(new PropertyGroupDescription("City"));
            clientsView.SortDescriptions.Clear();
            clientsView.SortDescriptions.Add(new SortDescription("City", ListSortDirection.Ascending));
        }
        #endregion
        #endregion



        #region Filter
        #region FilterCar
        private void CarFilterNone(object sender, RoutedEventArgs e)
        {
            carsView.Filter = null;
        }
        private void CarFilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int index = CarFilterComboBox.SelectedIndex;
            CarFilterComboBox.SelectedIndex = 0;
            CarFilterComboBox.SelectedIndex = index;
        }
        private void CarFilterFuelConsumption(object sender, RoutedEventArgs e)
        {
            carsView.Filter = null;
            string[] fuelConsumption = CarFilterBox.Text.Split('-');
            int lowerLimit, upperLimit;

            if (fuelConsumption.Count() >= 1 && int.TryParse(fuelConsumption[0], out lowerLimit))
            {
                if (!(fuelConsumption.Count() > 1 && int.TryParse(fuelConsumption[1], out upperLimit)))
                {
                    upperLimit = lowerLimit;
                }
                carsView.Filter = delegate (object item)
                {
                    Car car = item as Car;
                    if (car != null)
                    {
                        return (car.FuelConsumption >= lowerLimit && car.FuelConsumption <= upperLimit);
                    }
                    return false;
                };
            }
        }
        #endregion

        #region FilterClient
        private void ClientFilterNone(object sender, RoutedEventArgs e)
        {
            clientsView.Filter = null;
        }


        private void ClientFilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int index = ClientFilterComboBox.SelectedIndex;
            ClientFilterComboBox.SelectedIndex = 0;
            ClientFilterComboBox.SelectedIndex = index;
        }
        private void ClientFilterPesel(object sender, RoutedEventArgs e)
        {
            clientsView.Filter = delegate (object item)
            {
                Client client = item as Client;
                if (client != null)
                {
                    return client.Pesel.Equals(ClientFilterBox.Text);
                }
                return false;
            };
        }
        #endregion
        #endregion

    }
}
