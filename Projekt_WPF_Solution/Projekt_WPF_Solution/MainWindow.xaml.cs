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
        private ObservableCollection<Car> cars;
        private ListCollectionView carsView { get { return (ListCollectionView)CollectionViewSource.GetDefaultView(cars); } }

        private ObservableCollection<Client> clients;
        private ListCollectionView clientsView { get { return (ListCollectionView)CollectionViewSource.GetDefaultView(clients); } }



        public MainWindow()
        {
            InitializeComponent();
            this.GetCars();
            this.GetClients();
        }

        #region SqlData
        private void GetCars()
        {
            cars = new ObservableCollection<Car>();
            IDBaccess db = new IDBaccess();
            if (db.OpenConnection() == true)
            {
                MySqlCommand cmd = db.CreateCommand();
                cmd.CommandText = "SELECT RegPlate, Maker, Model, ManufacturedYear, Engine, Type, BodyType, FuelConsumption, Image FROM cars";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Car newCar = new Car(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6), reader.GetDouble(7), reader.GetString(8));
                    cars.Add(newCar);
                }
                db.CloseConnection();
            }
            CarListBox.ItemsSource = carsView;
        }
        private void GetClients()
        {
            clients = new ObservableCollection<Client>();
            IDBaccess db = new IDBaccess();
            if (db.OpenConnection() == true)
            {
                MySqlCommand cmd = db.CreateCommand();
                cmd.CommandText = "SELECT Pesel, Name, Surname, Born, IsMale, PhoneNumber, Address, City, Type, Image from Clients";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Client newClient = new Client(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetBoolean(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                    clients.Add(newClient);
                }
                db.CloseConnection();
            }
            ClientListBox.ItemsSource = clientsView;
        }
        #endregion


        private void ContactMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow contactWindow = new ContactWindow();
            contactWindow.ShowDialog();
        }

        #region AddButtons
        private void AddNewCar_Click(object sender, RoutedEventArgs e)
        {
            Car newCar = new Car();
            AddNewCarWindow newCarWindow = new AddNewCarWindow(newCar);
            if (newCarWindow.ShowDialog() == true)
            {
                if (!newCar.Insert())
                {
                    MessageBox.Show("BŁĄD DODAWANIA");
                }
                else
                {
                    this.GetCars();
                }
            }
        }

        private void AddNewClientButton_Click(object sender, RoutedEventArgs e)
        {
            Client newClient = new Client();
            AddNewClientWindow newClientWindow = new AddNewClientWindow(newClient);
            if (newClientWindow.ShowDialog() == true)
            {
                if (!newClient.Insert())
                {
                    MessageBox.Show("BŁĄD DODAWANIA");
                }
                else
                {
                    this.GetClients();
                }
            }
        }

        private void AddNewRentalButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewRentalWindow newRenatlWindow = new AddNewRentalWindow();
            newRenatlWindow.ShowDialog();
        }

        #endregion



        private void SearchClientButton_Click(object sender, RoutedEventArgs e)
        {
            SearchClientWindow searchClientWindow = new SearchClientWindow();
            searchClientWindow.ShowDialog();
        }

        #region DeleteCommand
        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ListBox lw = e.Source as ListBox;
            if(lw != null && lw.SelectedItems.Count > 0)
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
                if (selectedItem is Car)
                {
                    if(!(selectedItem as Car).Delete())
                    {
                        MessageBox.Show("Błąd usuwania");
                    }
                    else
                    {
                        this.GetCars();
                    }
                }
                else if (selectedItem is Client)
                {
                    if (!(selectedItem as Client).Delete())
                    {
                        MessageBox.Show("Błąd usuwania");
                    }
                    else
                    {
                        this.GetClients();
                    }
                }
            }
        }
        #endregion

    }
}
