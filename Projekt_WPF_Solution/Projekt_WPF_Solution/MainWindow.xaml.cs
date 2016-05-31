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
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

namespace Projekt_WPF_Solution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ListCollectionView carsView { get { return (ListCollectionView)CollectionViewSource.GetDefaultView(SqlDataGetters.Cars); } }
        private ListCollectionView clientsView { get { return (ListCollectionView)CollectionViewSource.GetDefaultView(SqlDataGetters.Clients); } }
        private ListCollectionView rentsView { get { return (ListCollectionView)CollectionViewSource.GetDefaultView(SqlDataGetters.Rents); } }

        public MainWindow()
        {
            InitializeComponent();
            SqlDataGetters.GetAll();
            CarListBox.ItemsSource = SqlDataGetters.Cars;
            ClientListBox.ItemsSource = SqlDataGetters.Clients;
            RentListBox.ItemsSource = SqlDataGetters.Rents;
        }

        private void ContactMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow contactWindow = new ContactWindow();
            contactWindow.ShowDialog();
        }

        #region AddButtons
        private void AddNewCar_Click(object sender, RoutedEventArgs e)
        {
            Car newCar = new Car();
            AddNewCarWindow newCarWindow = new AddNewCarWindow(newCar, true);
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
            AddNewClientWindow newClientWindow = new AddNewClientWindow(newClient, true);
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
            AddNewRentalWindow newRenatlWindow = new AddNewRentalWindow(newRent, true);
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

        #endregion



        private void SearchClientButton_Click(object sender, RoutedEventArgs e)
        {
            SearchClientWindow searchClientWindow = new SearchClientWindow();
            searchClientWindow.ShowDialog();
        }

        #region Commands
        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ListBox lw = e.Source as ListBox;
            if (lw != null && lw.SelectedItems.Count > 0)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }
        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy napewno chcesz usunąć zaznaczony wpis?", "Czy jestes pewien?", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            if (result == MessageBoxResult.OK)
            {
                ListBox lw = e.Source as ListBox;
                var selectedItem = lw.SelectedItem;
                bool deleted = false;

                if (selectedItem is Car)
                {
                    deleted = (selectedItem as Car).SqlDelete();
                }
                else if (selectedItem is Client)
                {
                    deleted = (selectedItem as Client).SqlDelete();
                }
                else if (selectedItem is Rent)
                {
                    deleted = (selectedItem as Rent).SqlDelete();
                }

                if(!deleted)
                {
                    MessageBox.Show("Błąd usuwania!");
                }
                else
                {
                    SqlDataGetters.GetAll();
                }
            }
        }
        private void Edit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ListBox lw = e.Source as ListBox;
            if (lw != null && lw.SelectedItems.Count > 0)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }
        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ListBox lw = e.Source as ListBox;
            var selectedItem = lw.SelectedItem;
            if (selectedItem is Car)
            {
                Car car = new Car(selectedItem as Car);
                AddNewCarWindow newCarWindow = new AddNewCarWindow(car, true);
                if (newCarWindow.ShowDialog() == true)
                {
                    car.SqlUpdate();
                    (selectedItem as Car).PropertyUpdate(car);
                }

            }
            else if (selectedItem is Client)
            {
                Client client = new Client(selectedItem as Client);
                AddNewClientWindow newClientWindow = new AddNewClientWindow(client, true);
                if (newClientWindow.ShowDialog() == true)
                {
                    client.SqlUpdate();
                    (selectedItem as Client).PropertyUpdate(client);
                }
            }
            else if (selectedItem is Rent)
            {
                Rent rent = new Rent(selectedItem as Rent);
                AddNewRentalWindow newRentalWindow = new AddNewRentalWindow(rent, true);
                if(newRentalWindow.ShowDialog() == true)
                {
                    rent.SqlUpdate();
                    (selectedItem as Rent).PropertyUpdate(rent);
                }
            }
        }
        #endregion

    }
}
