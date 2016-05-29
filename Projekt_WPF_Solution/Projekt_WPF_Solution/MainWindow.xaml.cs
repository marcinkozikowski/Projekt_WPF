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
        private ListCollectionView rentsView {  get { return (ListCollectionView)CollectionViewSource.GetDefaultView(SqlDataGetters.Rents); } }



        public MainWindow()
        {
            InitializeComponent();
            SqlDataGetters.GetAll();
            CarListBox.ItemsSource = carsView;
            ClientListBox.ItemsSource = clientsView;
            RentListBox.ItemsSource = rentsView;
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
            AddNewCarWindow newCarWindow = new AddNewCarWindow(newCar);
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
            AddNewClientWindow newClientWindow = new AddNewClientWindow(newClient);
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
            //AddNewRentalWindow newRenatlWindow = new AddNewRentalWindow();
            //newRenatlWindow.ShowDialog();
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

                IDBaccess db = new IDBaccess();
                if (db.OpenConnection() == true)
                {
                    try
                    {
                        MySqlCommand cmd = db.CreateCommand();
                        if (selectedItem is Car)
                        {
                            cmd.CommandText = "DELETE FROM cars WHERE RegPlate = @RegPlate";
                            cmd.Parameters.AddWithValue("@RegPlate", (selectedItem as Car).RegPlate);
                            cmd.ExecuteNonQuery();
                           // this.GetCars();
                        }
                        else if (selectedItem is Client)
                        {
                            cmd.CommandText = "DELETE FROM clients WHERE Pesel = @Pesel";
                            cmd.Parameters.AddWithValue("@Pesel", (selectedItem as Client).Pesel);
                            cmd.ExecuteNonQuery();
                           // this.GetClients();
                        }
                        else if (selectedItem is Rent)
                        {
                            cmd.CommandText = "DELETE FROM rents WHERE ID = @Id";
                            cmd.Parameters.AddWithValue("@Id", (selectedItem as Rent).Id);
                            cmd.ExecuteNonQuery();
                           // this.GetRents();
                        }
                    }
                    catch (MySqlException)
                    {
                        MessageBox.Show("BŁĄD");
                    }
                }
                else
                {
                    MessageBox.Show("BŁĄD");
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
            if(selectedItem is Car)
            {
                Car car = new Car(selectedItem as Car);
                AddNewCarWindow newCarWindow = new AddNewCarWindow(car);
                if(newCarWindow.ShowDialog() == true)
                {
                    car.SqlUpdate();
                    (selectedItem as Car).PropertyUpdate(car);
                }

            }
            else if(selectedItem is Client)
            {
                Client client = new Client(selectedItem as Client);
                AddNewClientWindow newClientWindow = new AddNewClientWindow(client);
                if (newClientWindow.ShowDialog() == true)
                {
                    client.SqlUpdate();
                    (selectedItem as Client).PropertyUpdate(client);
                }
            }
            else if(selectedItem is Rent)
            {

            }
        }
        #endregion

    }
}
