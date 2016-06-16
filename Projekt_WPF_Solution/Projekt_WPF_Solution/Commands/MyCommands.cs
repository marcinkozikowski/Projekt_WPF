using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Projekt_WPF_Solution.DataBaseClasses;
using System.Windows;
using System.Windows.Media;
using System.Globalization;

namespace Projekt_WPF_Solution.Commands
{
    public static class MyCommands
    {
        private static RoutedUICommand delete;
        private static RoutedUICommand edit;
        private static RoutedUICommand print;

        static MyCommands()
        {
            delete = new RoutedUICommand("Delete", "del", typeof(MyCommands));
            edit = new RoutedUICommand("Edit", "edit", typeof(MyCommands));
            print = new RoutedUICommand("Print", "print", typeof(MyCommands));
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

        public static RoutedUICommand Print
        {
            get { return print; }
            set { print = value; }
        }

        public static void BindCommands(Window window)
        {
            window.CommandBindings.Add(new CommandBinding(edit, Edit_Executed, Edit_CanExecute));
            window.CommandBindings.Add(new CommandBinding(delete, Delete_Executed, Delete_CanExecute));
            window.CommandBindings.Add(new CommandBinding(print, Print_Executed, Print_CanExecute));
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
                    (selectedItem as Rent).PropertyUpdate(rent);
                    if (SqlDataGetters.Clients.Contains(rent.RentingPerson))
                    {
                        rent.RentingPerson.SqlUpdate();
                    }
                    else
                    {
                        rent.RentingPerson.SqlInsert();
                        rent.RentingPerson.ID = SqlDataGetters.GetUserIdByPesel(rent.RentingPerson.Pesel);
                    }
                    rent.SqlUpdate();
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

        #region Print
        public static void Print_CanExecute(object sender, CanExecuteRoutedEventArgs e)
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

        public static void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {

                ListBox lw = e.Source as ListBox;
                var selectedItem = lw.SelectedItem;

                if (selectedItem is Rent)
                {

                    PrintDialog printDialog = new PrintDialog();

                // Stwórz nagłówek
                DrawingVisual header = new DrawingVisual();
                ContainerVisual newVisual = new ContainerVisual();
                using (DrawingContext dc = header.RenderOpen())
                {
                    Typeface typeface = new Typeface("Times New Roman");
                    FormattedText text = new FormattedText("Wypożyczalnia samochodów M&M_auto", CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight, typeface, 14, Brushes.Black);
                    dc.DrawText(text, new Point(96 * 0.25, 96 * 0.25));
                }
                // Dodaj nagłówek do Visual
                newVisual.Children.Add(header);

                if (printDialog.ShowDialog() == true)
                    {
                        // rysujemy na kontekście utworzonego DrawinVisual
                        DrawingVisual visual = new DrawingVisual();
                        using (DrawingContext dc = visual.RenderOpen())
                        {
                            // tworzymy tekst
                            FormattedText text = new FormattedText("Pierwszy wydruk",
                            CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                            new Typeface("Calibri"), 20, Brushes.Black);
                            // potrzebne do zawijania wierszy
                            text.MaxTextWidth = printDialog.PrintableAreaWidth / 2;
                            // pobranie rozmiaru tekstu
                            Size textSize = new Size(text.Width, text.Height);
                            // położenie
                            double margin = 96 * 0.25;
                            Point point = new Point(
                            (printDialog.PrintableAreaWidth - textSize.Width) / 2 - margin,
                            (printDialog.PrintableAreaHeight - textSize.Height) / 2 - margin);
                            // rysujemy
                            dc.DrawText(text, point);
                        // i ramka
                        dc.DrawRectangle(null, new Pen(Brushes.Black, 1),
                        new Rect(margin, margin, printDialog.PrintableAreaWidth - margin * 2,
                        printDialog.PrintableAreaHeight - margin * 2));
                        }
                        // drukujemy
                        printDialog.PrintVisual(visual, "Moja strona tekstu");
                    }
             }
        }

        #endregion
    }
}
