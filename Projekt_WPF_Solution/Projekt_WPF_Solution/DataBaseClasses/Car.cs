using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projekt_WPF_Solution.DataBaseClasses
{
    public class Car : IDataErrorInfo, INotifyPropertyChanged
    {
        #region Variables
        private string regPlate { get; set; }           //Numery rejestacyjne (KLUCZ GLOWNY)
        private string maker { get; set; }               // Producent
        private string model { get; set; }               // Model
        private int manufacturedYear { get; set; }       // Data produkcji auta
        private int engine { get; set; }            // Pojemność silnika
        private string type { get; set; }               // Klasa/Rodzaj auta terenowe, miejskie, premium
        private string bodyType { get; set; }           //Rodzaj nadwozia
        private double fuelConsumption { get; set; }     // Spalanie na 100/km
        private string image { get; set; }              // Zdjecie samochodu
        private bool rented { get; set; }              // Czy samochod jest aktualnie wypozyczony
        private bool booked { get; set; }               // Czy dany samochod został zarezerwowany
        #endregion
        #region Properties
        public string RegPlate { get { return regPlate; } set { regPlate = value; } }
        public string Maker { get { return maker; } set { maker = value; } }
        public string Model { get { return model; } set { model = value; } }
        public int ManufacturedYear { get { return manufacturedYear; } set { manufacturedYear = value; } }
        public int Engine { get { return engine; } set { engine = value; } }
        public string Type { get { return type; } set { type = value; } }
        public string BodyType { get { return bodyType; } set { bodyType = value; } }
        public double FuelConsumption { get { return fuelConsumption; } set { fuelConsumption = value; } }
        public string Image { get { return image; } set { image = value; OnPropertyChanged("Image"); } }
        public bool Rented { get { return rented; } set { rented = value; } }
        public bool Booked { get { return booked; } set { booked = value; } }
        public string MakerAndModel { get { return maker + " " + model; } }
        #endregion


        public Car()
        {
            this.RegPlate = string.Empty;
            this.Maker = string.Empty;
            this.Model = string.Empty;
            this.Type = string.Empty;
            this.BodyType = string.Empty;
            this.Image = "brakZdjecia.gif";
        }

        public Car(string regPlate, string maker, string model, int manufacturedYear, int engine, string type, string bodyType, double fuelConsumption, string image)
        {
            this.regPlate = regPlate;
            this.maker = maker;
            this.model = model;
            this.manufacturedYear = manufacturedYear;
            this.engine = engine;
            this.type = type;
            this.bodyType = bodyType;
            this.fuelConsumption = fuelConsumption;
            this.image = image;
        }

        #region SQL
        public bool Insert()
        {
            IDBaccess db = new IDBaccess();
            if (db.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = db.CreateCommand();
                    cmd.CommandText = "INSERT INTO cars (RegPlate, Maker, Model, ManufacturedYear, Engine, Type, BodyType, FuelConsumption, Image) VALUES (@RegPlate, @Maker, @Model, @ManufacturedYear, @Engine, @Type, @BodyType, @FuelConsumption, @Image)";
                    cmd.Parameters.AddWithValue("@RegPlate", this.RegPlate);
                    cmd.Parameters.AddWithValue("@Maker", this.Maker);
                    cmd.Parameters.AddWithValue("@Model", this.Model);
                    cmd.Parameters.AddWithValue("@ManufacturedYear", this.ManufacturedYear);
                    cmd.Parameters.AddWithValue("@Engine", this.Engine);
                    cmd.Parameters.AddWithValue("@Type", this.Type);
                    cmd.Parameters.AddWithValue("@BodyType", this.BodyType);
                    cmd.Parameters.AddWithValue("@FuelConsumption", this.FuelConsumption);
                    cmd.Parameters.AddWithValue("@Image", this.Image);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (MySqlException)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        #endregion
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
                if (columnName.Equals("FuelConsumption"))
                {
                    if (FuelConsumption <= 0)
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
