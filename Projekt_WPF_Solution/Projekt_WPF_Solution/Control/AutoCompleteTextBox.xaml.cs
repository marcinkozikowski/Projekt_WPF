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

namespace Projekt_WPF_Solution.Control
{
    /// <summary>
    /// Interaction logic for AutoCompleteTextBox.xaml
    /// </summary>
    public partial class AutoCompleteTextBox : UserControl
    {
        #region variables
        private ObservableCollection<Client> clients;
        private System.Timers.Timer keypressTimer;
        private delegate void TextChangeCallback();
        private bool insertText;
        private int delayTime;
        private int searchTreshold;
        #endregion


        #region Properties
        public ObservableCollection<Client> Clients { get { return clients; } set { clients = value; } }
        public int DelayTime
        {
            get { return delayTime; }
            set { delayTime = value; }
        }
        public int Threshold
        {
            get { return searchTreshold; }
            set { searchTreshold = value; }
        }

        public ComboBox ClientComboBox
        {
            get { return ComboBox; }
            set { ComboBox = value; }
        }

        public event EventHandler ChangedSelection;

        #endregion
        public AutoCompleteTextBox()
        {
            InitializeComponent();
            clients = new ObservableCollection<Client>(SqlDataGetters.Clients);
            clients.Insert(0, new Client());
            ComboBox.ItemsSource = clients;

            searchTreshold = 2;
            keypressTimer = new System.Timers.Timer();
            keypressTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
        }
        private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            keypressTimer.Stop();
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new TextChangeCallback(this.TextChanged));
        }
        private void AutoCompleteComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox.SelectedItem != null)
            {
                insertText = true;
                TextBox.Text = ComboBox.SelectedItem.ToString();
                this.ChangedSelection?.Invoke(this, e);
            }
            else
            {
                insertText = true;
                if(string.IsNullOrEmpty(TextBox.Text))
                {
                    ComboBox.SelectedIndex = clients.Count - 1;
                }
            }
        }
        private void TextChanged()
        {
            try
            {
                ComboBox.ItemsSource = null;
                ComboBox.Items.Clear();
                if (TextBox.Text.Length >= searchTreshold)
                {
                    foreach (Client client in Clients)
                    {
                        if (client.ToString().ToLower().Contains(TextBox.Text.ToLower()))
                        {
                            ComboBox.Items.Add(client);
                        }
                    }
                    ComboBox.IsDropDownOpen = true;

                }
                else
                {
                    ComboBox.Items.Add(new Client());
                    ComboBox.IsDropDownOpen = false;
                    ComboBox.SelectedIndex = ComboBox.Items.Count - 1;

                }
            }
            catch
            {
                ComboBox.IsDropDownOpen = false;
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (insertText) { insertText = false; }
            else
            {
                if (delayTime > 0)
                {
                    keypressTimer.Interval = delayTime;
                    keypressTimer.Start();
                }
                else
                {
                    TextChanged();
                }
            }
        }
    }
}
