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
using Projekt_WPF_Solution.Commands;

namespace Projekt_WPF_Solution
{
    /// <summary>
    /// Interaction logic for SearchClientWindow.xaml
    /// </summary>
    public partial class SearchClientWindow : Window
    {
        private ListCollectionView clientsView;
        public SearchClientWindow()
        {
            clientsView = new ListCollectionView(SqlDataGetters.Clients);
            InitializeComponent();
            ClientListBox.ItemsSource = clientsView;
            Loaded += delegate
            {
                MyCommands.BindCommands(this);
            };
        }

        private void DeleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Czy napewno chcesz usunąć zaznaczony wpis?", "Czy jestes pewien?", MessageBoxButton.OKCancel, MessageBoxImage.Error);
        }

        private void CloseSearchClientWButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region Filter
        private void SearchClientButton_Click(object sender, RoutedEventArgs e)
        {
            if(SearchClientTextBox.Text.Equals(string.Empty))
            {
                clientsView.Filter = null;
            }
            else
            {
                clientsView.Filter = delegate (object item)
                {
                    Client client = item as Client;
                    if (client != null)
                    {
                        if (NameSurnameRadioButton.IsChecked == true)
                        {
                            return client.NameSurname.ToLower().Equals(SearchClientTextBox.Text.ToLower());
                        }
                        else if (PeselRadioButton.IsChecked == true)
                        {
                            return client.Pesel.Equals(SearchClientTextBox.Text);

                        }
                        else if (CityRadioButton.IsChecked == true)
                        {
                            return client.City.ToLower().Equals(SearchClientTextBox.Text.ToLower());
                        }

                    }
                    return false;
                };
            }
        }
        private void NoneRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            clientsView.Filter = null;
        }

        #endregion

        private void ShowClientButton_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow client = new ClientWindow(ClientListBox.SelectedItem as Client);
            client.ShowDialog();
        }

        
    }
}
