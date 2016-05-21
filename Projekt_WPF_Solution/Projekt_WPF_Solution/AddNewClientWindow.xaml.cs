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
    /// Interaction logic for AddNewClientWindow.xaml
    /// </summary>
    public partial class AddNewClientWindow : Window
    {
        Client newClient;
        public AddNewClientWindow(Client newClient)
        {
            InitializeComponent();
            this.newClient = newClient;
            MainAddClientGrid.DataContext = newClient;
            
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelClientButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
