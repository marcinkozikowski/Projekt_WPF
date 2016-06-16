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
        private ListCollectionView carsView;

        public RentalWindow(Rent rent)
        {
            this.rent = rent;
            carsView = new ListCollectionView(SqlDataGetters.Cars);
            InitializeComponent();
            //main grid
            MainRentalGrid.DataContext = rent;
            //client grid

            ClientAutoCompleteTextBox.ChangedSelection += new EventHandler(AutoCompleteComboBox_SelectionChanged);
            if (this.rent.RentingPerson != null)
            {
                ClientAutoCompleteTextBox.ComboBox.SelectedItem = this.rent.RentingPerson;
            }
            else
            {
                this.rent.RentingPerson = ClientAutoCompleteTextBox.ComboBox.SelectedItem as Client;
            }
            //load car
            CarTypeComboBox.ItemsSource = SqlDataGetters.CarTypes;
            CarBodyTypeComboBox.ItemsSource = SqlDataGetters.BodyTypes;
            TypKlientaComboBox.ItemsSource = SqlDataGetters.ClientTypes;
            CarsListBox.ItemsSource = carsView;
        }

        private void AutoCompleteComboBox_SelectionChanged(object sender, EventArgs e)
        {
            this.rent.RentingPerson = ClientAutoCompleteTextBox.ComboBox.SelectedItem as Client;
        }

        private void CancelRenatlButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CarTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterCars();
        }

        private void CarBodyTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterCars();
        }

        private void FuelConsumptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterCars();
        }

        private void FilterCars()
        {
            List<Car> availableCars = SqlDataGetters.GetAvailableCars(OdDataPicker.SelectedDate, DoDataPicker.SelectedDate);
            if (!availableCars.Contains(rent.RentedCar))
            {
                availableCars.Add(rent.RentedCar);
            }
            carsView.Filter = delegate (object item)
            {
                Car car = item as Car;
                if (car != null)
                {
                    bool retValue = availableCars.Contains(car);
                    if (CarTypeComboBox != null && CarTypeComboBox.SelectedIndex > 0)
                    {
                        retValue = retValue && car.Type.Equals(CarTypeComboBox.SelectedItem);
                    }
                    if (CarBodyTypeComboBox != null && CarBodyTypeComboBox.SelectedIndex > 0)
                    {
                        retValue = retValue && car.BodyType.Equals(CarBodyTypeComboBox.SelectedItem);
                    }

                    if (CarFuelConsumptionTextBox != null && string.IsNullOrWhiteSpace(CarFuelConsumptionTextBox.Text))
                    {
                        string[] fuelConsumption = CarFuelConsumptionTextBox.Text.Split('-');
                        int lowerLimit, upperLimit;
                        if (fuelConsumption.Count() >= 1 && int.TryParse(fuelConsumption[0], out lowerLimit))
                        {
                            if (!(fuelConsumption.Count() > 1 && int.TryParse(fuelConsumption[1], out upperLimit)))
                            {
                                upperLimit = lowerLimit;
                            }
                            retValue = retValue && car.FuelConsumption >= lowerLimit && car.FuelConsumption <= upperLimit;
                        }
                    }
                    return retValue;
                }
                return false;
            };
        }

        private void DataPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterCars();
        }

        

        private void AddRentalButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            SqlDataGetters.GetAll();
            SqlDataGetters.GetBottomPanelInfo();
            this.Close();
        }

        private void ZwrotComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
