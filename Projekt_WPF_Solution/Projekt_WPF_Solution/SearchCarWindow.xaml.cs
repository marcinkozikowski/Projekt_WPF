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
    /// Interaction logic for SearchCarWindow.xaml
    /// </summary>
    public partial class SearchCarWindow : Window
    {
        private ListCollectionView carsView;

        public Car SearchedCar { get { return CarsListBox.SelectedItem as Car; ; } }
        public SearchCarWindow()
        {
            carsView = new ListCollectionView(SqlDataGetters.Cars);
            InitializeComponent();
            CarsListBox.ItemsSource = carsView;
            Loaded += delegate
            {
                MyCommands.BindCommands(this);
            };
        }
        private void SearchCarButton_Click(object sender, RoutedEventArgs e)
        {   
            if(FreeRadioButton.IsChecked == true)
            {
                List<Car> availableCars = SqlDataGetters.GetAvailableCars(OdDatePicker.SelectedDate, DoDatePIcker.SelectedDate);
                carsView.Filter = delegate (object item)
                {
                    Car car = item as Car;
                    if (car != null)
                    {
                        return availableCars.Contains(car);
                    }
                    return false;
                };
            }
            else if (SearchCarTextBox.Text.Equals(string.Empty))
            {
                carsView.Filter = null;
            }
            else
            {
                carsView.Filter = delegate (object item)
                {
                    Car car = item as Car;
                    if (car != null)
                    {
                        if (NrRejRadioButton.IsChecked == true)
                        {
                            return car.RegPlate.ToLower().Equals(SearchCarTextBox.Text.ToLower());
                        }
                        else if (MarkaRadioButton.IsChecked == true)
                        {
                            return car.Maker.ToLower().Equals(SearchCarTextBox.Text.ToLower());
                        }
                    }
                    return false;
                };
            }
        }

        private void NoneRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            carsView.Filter = null;
        }

        private void CloseSearchCarButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ChooseCarButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
