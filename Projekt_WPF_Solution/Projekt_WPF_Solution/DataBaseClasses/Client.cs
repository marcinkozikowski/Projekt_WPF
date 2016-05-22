using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_WPF_Solution.DataBaseClasses
{
    public class Client : IDataErrorInfo, INotifyPropertyChanged
    {
        #region Variables
        private int id, phoneNumber;
        private string name, surname, pesel, address, city, type, image;
        private bool isMale;
        private DateTime born;
        #endregion
        #region Properties
        public int Id { get { return id; } set { id = value; } }                                //ID 
        public string Name { get { return name; } set { name = value; } }                       //Imię 
        public string Surname { get { return surname; } set { surname = value; } }              //Nazwisko
        public DateTime Born { get { return born; } set { born = value; } }                     //Data urodzenia
        public string Pesel { get { return pesel; } set { pesel = value; } }                    //PESEL
        public bool IsMale { get { return isMale; } set { isMale = value; } }                   //Płeć (true = mężczyzna, false = kobieta)
        public int PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }     //Numer telefonu
        public string Address { get { return address; } set { address = value; } }              //Adres
        public string City { get { return city; } set { city = value; } }                       //Miasto
        public string Type { get { return type; } set { type = value; } }                       //Typ klienta
        public string Image { get { return image; } set { image = value; OnPropertyChanged("Image"); } }                    //Ścieżka do pliku jpg
        #endregion

        public Client(int id)
        {
            name = surname = pesel = address = city = type = string.Empty;
            this.id = id;
            phoneNumber = 0;
            image = "brakZdjecia.gif";
        }
        #region IDataErrorInfo
        public string Error
        {
            get
            {
                //throw new NotImplementedException();
                return null;
            }
        }
        public string this[string columnName]
        {
            get
            {
                if (columnName.Equals("Pesel"))
                {
                    if (Pesel.Count() != 11)
                    {
                        return "Zły pesel";
                    }
                }
                return null;
            }
        }
        #endregion
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
