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
using Microsoft.Win32;
using System.IO;

namespace Projekt_WPF_Solution
{
    /// <summary>
    /// Interaction logic for CarWindow.xaml
    /// </summary>
    public partial class CarWindow : Window
    {
        Car car;

        public CarWindow(Car car)
        {
            InitializeComponent();
            MarkaComboBox.ItemsSource = SqlDataGetters.Brands;
            TypComboBox.ItemsSource = SqlDataGetters.BodyTypes;
            KlasaComboBox.ItemsSource = SqlDataGetters.CarTypes;

            this.car = car;
            MainAddCarGrid.DataContext = this.car;
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
            this.Close();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Image File(*.jpg; *.bmp; *.gif)| *.jpg; *.bmp; *.gif";
            op.Title = "Wybierz zdjęcie:";
            if (op.ShowDialog() == true)
            {
                /*if(op.FileNames.Count() == 2)
                {

                }*/
                //car.Image2 = new BitmapImage(new Uri(op.FileName));
            }
        }
    }
}
