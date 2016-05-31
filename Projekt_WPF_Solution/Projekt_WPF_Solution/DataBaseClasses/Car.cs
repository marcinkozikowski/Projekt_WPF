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
        private int id { get; set; }                    //ID PRIMARY KEY
        private string regPlate { get; set; }           //Numery rejestacyjne
        private string maker { get; set; }               // Producent
        private string model { get; set; }               // Model
        private int manufacturedYear { get; set; }       // Data produkcji auta
        private int engine { get; set; }            // Pojemność silnika
        private int type { get; set; }               // Klasa/Rodzaj auta terenowe, miejskie, premium
        private string bodyType { get; set; }           //Rodzaj nadwozia
        private double fuelConsumption { get; set; }     // Spalanie na 100/km
        private string image { get; set; }              // Zdjecie samochodu
        private bool rented { get; set; }              // Czy samochod jest aktualnie wypozyczony
        private bool booked { get; set; }               // Czy dany samochod został zarezerwowany
        #endregion
        #region Properties
        public int ID { get { return id; } set { id = value; } }
        public string RegPlate { get { return regPlate; } set { regPlate = value; } }
        public string Maker { get { return maker; } set { maker = value; } }
        public string Model { get { return model; } set { model = value; } }
        public int ManufacturedYear { get { return manufacturedYear; } set { manufacturedYear = value; } }
        public int Engine { get { return engine; } set { engine = value; } }
        public int Type { get { return type; } set { type = value; } }
        public string BodyType { get { return bodyType; } set { bodyType = value; } }
        public double FuelConsumption { get { return fuelConsumption; } set { fuelConsumption = value; } }
        public string Image { get { return image; } set { image = value; OnPropertyChanged("Image"); } }
        public bool Rented { get { return rented; } set { rented = value; } }
        public bool Booked { get { return booked; } set { booked = value; } }
        public string MakerAndModel { get { return maker + " " + model; } }
        #endregion
        #region Constructors
        public Car()
        {
            this.RegPlate = string.Empty;
            this.Maker = string.Empty;
            this.Model = string.Empty;
            this.BodyType = string.Empty;
            this.Image = "brakZdjecia.gif";
        }
        public Car(int id, string regPlate, string maker, string model, int manufacturedYear, int engine, int type, string bodyType, double fuelConsumption, string image)
        {
            this.id = id;
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
        public Car(Car car)
        {
            this.id = car.id;
            this.regPlate = car.regPlate;
            this.maker = car.maker;
            this.model = car.model;
            this.manufacturedYear = car.manufacturedYear;
            this.engine = car.engine;
            this.type = car.type;
            this.bodyType = car.bodyType;
            this.fuelConsumption = car.fuelConsumption;
            this.image = car.image;
        }
        #endregion
        #region SQL
        public bool SqlInsert()
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
        public bool SqlUpdate()
        {
            IDBaccess db = new IDBaccess();
            if (db.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = db.CreateCommand();
                    cmd.CommandText = "UPDATE cars SET RegPlate = @RegPlate, Maker = @Maker, Model = @Model, ManufacturedYear = @ManufacturedYear, Engine = @Engine, Type = @Type, BodyType = @BodyType, FuelConsumption = @FuelConsumption, Image = @Image WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@RegPlate", this.RegPlate);
                    cmd.Parameters.AddWithValue("@Maker", this.Maker);
                    cmd.Parameters.AddWithValue("@Model", this.Model);
                    cmd.Parameters.AddWithValue("@ManufacturedYear", this.ManufacturedYear);
                    cmd.Parameters.AddWithValue("@Engine", this.Engine);
                    cmd.Parameters.AddWithValue("@Type", this.Type);
                    cmd.Parameters.AddWithValue("@BodyType", this.BodyType);
                    cmd.Parameters.AddWithValue("@FuelConsumption", this.FuelConsumption);
                    cmd.Parameters.AddWithValue("@Image", this.Image);
                    cmd.Parameters.AddWithValue("@ID", this.ID);
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
        public bool SqlDelete()
        {
            IDBaccess db = new IDBaccess();
            if(db.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = db.CreateCommand();
                    cmd.CommandText = "DELETE FROM cars WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", this.ID);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch(MySqlException)
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

        public void PropertyUpdate(Car car)
        {
            this.id = car.id;
            this.regPlate = car.regPlate;
            this.maker = car.maker;
            this.model = car.model;
            this.manufacturedYear = car.manufacturedYear;
            this.engine = car.engine;
            this.type = car.type;
            this.bodyType = car.bodyType;
            this.fuelConsumption = car.fuelConsumption;
            this.image = car.image;
        }
    }
}
