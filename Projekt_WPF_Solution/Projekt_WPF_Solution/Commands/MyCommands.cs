using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Projekt_WPF_Solution.DataBaseClasses;
using System.Windows;

namespace Projekt_WPF_Solution.Commands
{
    public static class MyCommands
    {
        private static RoutedUICommand delete;
        private static RoutedUICommand edit;

        static MyCommands()
        {
            delete = new RoutedUICommand("Delete", "del", typeof(MyCommands));
            edit = new RoutedUICommand("Edit", "edit", typeof(MyCommands));
        }

        public static RoutedUICommand Delete
        {
            get { return delete; }
            set { delete = value; }
        }

        public static RoutedUICommand Edit
        {
            get { return edit; }
            set { edit = value; }
        }

        public static void BindCommands(Window window)
        {
            window.CommandBindings.Add(new CommandBinding(edit, Edit_Executed, Edit_CanExecute));
            window.CommandBindings.Add(new CommandBinding(delete, Delete_Executed, Delete_CanExecute));
        }


        #region Edit
        public static void Edit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
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
        public static void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ListBox lw = e.Source as ListBox;
            var selectedItem = lw.SelectedItem;
            if (selectedItem is Car)
            {
                Car car = new Car(selectedItem as Car);
                CarWindow newCarWindow = new CarWindow(car);
                if (newCarWindow.ShowDialog() == true)
                {
                    car.SqlUpdate();
                    (selectedItem as Car).PropertyUpdate(car);
                }

            }
            else if (selectedItem is Client)
            {
                Client client = new Client(selectedItem as Client);
                ClientWindow newClientWindow = new ClientWindow(client);
                if (newClientWindow.ShowDialog() == true)
                {
                    client.SqlUpdate();
                    (selectedItem as Client).PropertyUpdate(client);
                }
            }
            else if (selectedItem is Rent)
            {
                Rent rent = new Rent(selectedItem as Rent);
                RentalWindow newRentalWindow = new RentalWindow(rent);
                if (newRentalWindow.ShowDialog() == true)
                {
                    rent.SqlUpdate();
                    (selectedItem as Rent).PropertyUpdate(rent);
                }
            }
            SqlDataGetters.GetAll();
        }
        #endregion

        #region Delete
        public static void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
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
        public static void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
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

                if (!deleted)
                {
                    MessageBox.Show("Błąd usuwania!");
                }
                else
                {
                    SqlDataGetters.GetAll();
                }
            }
        }

        #endregion
    }
}
