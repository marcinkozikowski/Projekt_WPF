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

        #region Methods
        public MainWindow()
        {
            InitializeComponent();
            cars = new ObservableCollection<Car>();
            CarListBox.ItemsSource = carsView;

            clients = new ObservableCollection<Client>();
            ClientListBox.ItemsSource = clientsView;
        }

        private void ContactMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow contactWindow = new ContactWindow();
            contactWindow.ShowDialog();
        }

        private void AddNewCar_Click(object sender, RoutedEventArgs e)
        {
            Car newCar = new Car();
            AddNewCarWindow newCarWindow = new AddNewCarWindow(newCar);
            if(newCarWindow.ShowDialog() == true)
            {
                cars.Add(newCar);
            }
        }

        private void AddNewClientButton_Click(object sender, RoutedEventArgs e)
        {
            Client newClient = new Client();
            AddNewClientWindow newClientWindow = new AddNewClientWindow(newClient);
            if(newClientWindow.ShowDialog() == true)
            {
                clients.Add(newClient);
            }
        }

        #endregion
    }
}
