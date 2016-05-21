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
using Projekt_WPF_Solution.Validators;

namespace Projekt_WPF_Solution
{
    /// <summary>
    /// Interaction logic for AddNewCarWindow.xaml
    /// </summary>
    public partial class AddNewCarWindow : Window
    {
        Car newCar;

        public AddNewCarWindow(Car newCar)
        {
            InitializeComponent();
            this.newCar = newCar;
            MainAddCarGrid.DataContext = newCar;
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            if(Validator.IsValid(this))
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Niepoprawne dane!");
            }
        }

        private void CancelCarButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
