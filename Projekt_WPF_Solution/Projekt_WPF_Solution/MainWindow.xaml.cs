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
using System.Windows.Threading;

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
            BottomAlertPanel.DataContext = SqlDataGetters.BottomPanel;

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
            Rent rent = new Rent();
            RentalWindow newRentalWindow = new RentalWindow(rent);
            if (newRentalWindow.ShowDialog() == true)
            {
                if (SqlDataGetters.Clients.Contains(rent.RentingPerson))
                {
                    rent.RentingPerson.SqlUpdate();
                }
                else
                {
                    rent.RentingPerson.SqlInsert();
                    rent.RentingPerson.ID = SqlDataGetters.GetUserIdByPesel(rent.RentingPerson.Pesel);
                }
                rent.SqlInsert();
            }
            SqlDataGetters.GetAll();
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
        private void CarFilters()
        {
            List<Car> availableCars = SqlDataGetters.GetAvailableCars(DateTime.Today.Date, DateTime.Today.Date);
            carsView.Filter = delegate (object item)
            {
                Car car = item as Car;
                bool filterValue = true;
                if(CarFilterBox == null)
                {
                    return true;
                }
                if (AvailableOnlyCheckBox != null && AvailableOnlyCheckBox.IsChecked == true)
                {
                    filterValue = filterValue & availableCars.Contains(car);
                }
                if(string.IsNullOrWhiteSpace(CarFilterBox.Text))
                {
                    filterValue = filterValue & true;
                }
                else
                {
                    if(CarFilterComboBox.SelectedIndex == 0)
                    {
                        filterValue = filterValue & car.ToString().ToLower().Contains(CarFilterBox.Text.ToLower());
                    }
                    else if(CarFilterComboBox.SelectedIndex == 1)
                    {
                        string[] fuelConsumption = CarFilterBox.Text.Split('-');
                        int lowerLimit, upperLimit;
                        if (fuelConsumption.Count() >= 1 && int.TryParse(fuelConsumption[0], out lowerLimit))
                        {
                            if (!(fuelConsumption.Count() > 1 && int.TryParse(fuelConsumption[1], out upperLimit)))
                            {
                                upperLimit = lowerLimit;
                            }
                            filterValue = filterValue && car.FuelConsumption >= lowerLimit && car.FuelConsumption <= upperLimit;
                        }
                    }
                }
                return filterValue;
            };
        }
        private void CarFilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CarFilters();
        }
        private void AvailableOnlyCheckBox_CheckChanged(object sender, RoutedEventArgs e)
        {
            CarFilters();
        }
        private void CarFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CarFilters();
        }



        #endregion

        #region FilterClient
        private void ClientFilter()
        {
            clientsView.Filter = delegate (object item)
            {
                Client client = item as Client;
                bool filterValue = true;
                if(!string.IsNullOrWhiteSpace(ClientNameTextBox.Text))
                {
                    filterValue = client.ToString().ToLower().Contains(ClientNameTextBox.Text.ToLower()); 
                }
                return filterValue;                
            };
        }
        private void ClientNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClientFilter();
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainTabControl != null && MainTabControl.SelectedItem != null)
            {
                if ((MainTabControl.SelectedItem as TabItem).Name.Equals("CarTab"))
                {
                    CarFilterBox.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                    {
                        CarFilterBox.Focus();
                    }));
                }
                else if ((MainTabControl.SelectedItem as TabItem).Name.Equals("ClientTab"))
                {
                    ClientNameTextBox.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                    {
                        ClientNameTextBox.Focus();
                    }));
                }
                else if ((MainTabControl.SelectedItem as TabItem).Name.Equals("RentTab"))
                {
                    RentFilterTextBox.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                    {
                        RentFilterTextBox.Focus();
                    }));
                }
            }
        }


        #endregion

        #region FilterRent
        private void RentFilter()
        {
            rentsView.Filter = delegate (object item)
            {
                Rent rent = item as Rent;
                bool filterValue = true;
                if (!string.IsNullOrWhiteSpace(RentFilterTextBox.Text))
                {
                    filterValue = rent.ToString().ToLower().Contains(RentFilterTextBox.Text.ToLower());
                }
                return filterValue;
            };
        }
        private void RentFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RentFilter();
        }
        #endregion
        #endregion

    }
}
