using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_WPF_Solution.DataBaseClasses
{
    public class BottomPanelData : INotifyPropertyChanged
    {
        private int notReturned, booked, rented;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public int NotReturned
        {
            get { return notReturned; }
            set { notReturned = value; OnPropertyChanged("NotReturned"); }
        }
        public int Booked
        {
            get { return booked; }
            set { booked = value; OnPropertyChanged("Booked"); }
        }
        public int Rented
        {
            get { return rented; }
            set { rented = value; OnPropertyChanged("Rented"); }
        }
    }
}
