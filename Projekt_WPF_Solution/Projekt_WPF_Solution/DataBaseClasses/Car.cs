using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_WPF_Solution.DataBaseClasses
{
    public class Car : IDataErrorInfo, INotifyPropertyChanged
    {
        #region Variables
        private int id { get; set; }                    //ID
        private string maker { get; set; }               // Producent
        private string model { get; set; }               // Model
        private int manufacturedYear { get; set; }       // Data produkcji auta
        private int engine { get; set; }            // Pojemność silnika
        private string type { get; set; }               // Klasa/Rodzaj auta terenowe, miejskie, premium
        private string bodyType { get; set; }           //Rodzaj nadwozia
        private double fuelConsumption { get; set; }     // Spalanie na 100/km
        private string regPlate { get; set; }           //Numery rejestacyjne
        private string image { get; set; }              // Zdjecie samochodu
        private bool rented { get; set; }              // Czy samochod jest aktualnie wypozyczony
        private bool booked { get; set; }               // Czy dany samochod został zarezerwowany
        #endregion
        #region Properties
        public int Id {  get { return id; } set { id = value; } }
        public string Maker { get { return maker; } set { maker = value; } }
        public string Model { get { return model; } set { model = value; } }
        public int ManufacturedYear { get { return manufacturedYear; } set { manufacturedYear = value; } }
        public int Engine {  get { return engine; } set { engine = value; } }
        public string Type { get { return type; } set { type = value; } }
        public string BodyType { get { return bodyType; } set { bodyType = value; } }
        public string RegPlate { get { return regPlate; } set { regPlate = value; } }
        public double FuelConsumption { get { return fuelConsumption; } set { fuelConsumption = value; } }
        public string Image { get { return image; } set { image = value; OnPropertyChanged("Image"); } }
        public bool Rented { get { return rented; } set { rented = value; } }
        public bool Booked { get { return booked; } set { booked = value; } }
        public string MakerAndModel {  get { return maker + " " + model; } }
        #endregion


        public Car(int id)
        {
            this.id = id;
            maker = string.Empty;
            model = string.Empty;
            type = string.Empty;
            image = "brakZdjecia.gif";
            rented = false;
            booked = false;
        }

        #region IDataErrorInfo
        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                if(columnName.Equals("FuelConsumption"))
                {
                    if(FuelConsumption <= 0)
                    {
                        return "Nie może być zerem";
                    }
                }
                if (columnName.Equals("Engine"))
                {
                    if (Engine <= 0)
                    {
                        return "Nie może być zerem";
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
