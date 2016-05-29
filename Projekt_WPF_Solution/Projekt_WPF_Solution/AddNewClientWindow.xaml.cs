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
    /// Interaction logic for AddNewClientWindow.xaml
    /// </summary>
    public partial class AddNewClientWindow : Window
    {
        Client client;
        public AddNewClientWindow(Client client)
        {
            InitializeComponent();
            this.client = client;
            MainAddClientGrid.DataContext = client;
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validator.IsValid(this))
            {
                DialogResult = true;

            }
            else
            {
                MessageBox.Show("Niepoprawne dane!");
            }
        }

        private void CancelClientButton_Click(object sender, RoutedEventArgs e)
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
                string[] split = op.FileName.Split('.').ToArray();
                string filename = client.Pesel + "." + split[split.Count() - 1]; ;
                string dir = GetDirectory() + "\\" + filename;
                File.Copy(op.FileName, dir, true);
                client.Image = "Clients\\" + filename;
            }

        }

        private string GetDirectory()
        {
            string dir = null, imgdir;
            do
            {
                if (dir == null)
                    dir = Directory.GetCurrentDirectory();
                else
                    dir = Directory.GetParent(dir).ToString();
                imgdir = System.IO.Path.Combine(dir, "Images");
                imgdir = System.IO.Path.Combine(imgdir, "Clients");
            } while (!Directory.Exists(imgdir));
            return imgdir;
        }
    }
}
