using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Media.Imaging;

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
        private DataBaseClasses.Type type { get; set; } // Klasa/Rodzaj auta terenowe, miejskie, premium
        private string bodyType { get; set; }           //Rodzaj nadwozia
        private double fuelConsumption { get; set; }     // Spalanie na 100/km
        private BitmapImage image1 { get; set; }
        private BitmapImage image2 { get; set; }
        #endregion
        #region Properties
        public int ID { get { return id; } set { id = value; } }
        public string RegPlate { get { return regPlate; } set { regPlate = value; } }
        public string Maker { get { return maker; } set { maker = value; } }
        public string Model { get { return model; } set { model = value; } }
        public int ManufacturedYear { get { return manufacturedYear; } set { manufacturedYear = value; } }
        public int Engine { get { return engine; } set { engine = value; } }
        public DataBaseClasses.Type Type { get { return type; } set { type = value; } }
        public string BodyType { get { return bodyType; } set { bodyType = value; } }
        public double FuelConsumption { get { return fuelConsumption; } set { fuelConsumption = value; } }
        public BitmapImage Image1 { get { return image1; } set { image1 = value; OnPropertyChanged("Image1"); } }
        public BitmapImage Image2 { get { return image2; } set { image2 = value; OnPropertyChanged("Image2"); } }
        public string MakerAndModel { get { return maker + " " + model; } }
        #endregion
        #region Constructors
        public Car()
        {
            this.RegPlate = string.Empty;
            this.Maker = string.Empty;
            this.Model = string.Empty;
            this.BodyType = string.Empty;
            this.Image1 = Converters.ImageConverter.GetNoPhoto();
            this.Image2 = Converters.ImageConverter.GetNoPhoto();
        }
        public Car(int id, string regPlate, string maker, string model, int manufacturedYear, int engine, DataBaseClasses.Type type, string bodyType, double fuelConsumption, BitmapImage image1, BitmapImage image2)
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
            this.image1 = image1;
            this.image2 = image2;
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
            this.image1 = car.image1;
            this.image2 = car.image2;
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
                    cmd.CommandText = "INSERT INTO cars (RegPlate, Maker, Model, ManufacturedYear, Engine, Type, BodyType, FuelConsumption, Image1, Image2) VALUES (@RegPlate, @Maker, @Model, @ManufacturedYear, @Engine, @Type, @BodyType, @FuelConsumption, @Image1, @Image2)";
                    cmd.Parameters.AddWithValue("@RegPlate", this.RegPlate);
                    cmd.Parameters.AddWithValue("@Maker", this.Maker);
                    cmd.Parameters.AddWithValue("@Model", this.Model);
                    cmd.Parameters.AddWithValue("@ManufacturedYear", this.ManufacturedYear);
                    cmd.Parameters.AddWithValue("@Engine", this.Engine);
                    cmd.Parameters.AddWithValue("@Type", this.Type.Id);
                    cmd.Parameters.AddWithValue("@BodyType", this.BodyType);
                    cmd.Parameters.AddWithValue("@FuelConsumption", this.FuelConsumption);
                    cmd.Parameters.AddWithValue("@Image1", Converters.ImageConverter.ImageToBytes(this.Image1));
                    cmd.Parameters.AddWithValue("@Image2", Converters.ImageConverter.ImageToBytes(this.Image2));
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
                    cmd.CommandText = "UPDATE cars SET RegPlate = @RegPlate, Maker = @Maker, Model = @Model, ManufacturedYear = @ManufacturedYear, Engine = @Engine, Type = @Type, BodyType = @BodyType, FuelConsumption = @FuelConsumption, Image1 = @Image1, Image2 = @Image2 WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@RegPlate", this.RegPlate);
                    cmd.Parameters.AddWithValue("@Maker", this.Maker);
                    cmd.Parameters.AddWithValue("@Model", this.Model);
                    cmd.Parameters.AddWithValue("@ManufacturedYear", this.ManufacturedYear);
                    cmd.Parameters.AddWithValue("@Engine", this.Engine);
                    cmd.Parameters.AddWithValue("@Type", this.Type.Id);
                    cmd.Parameters.AddWithValue("@BodyType", this.BodyType);
                    cmd.Parameters.AddWithValue("@FuelConsumption", this.FuelConsumption);
                    cmd.Parameters.AddWithValue("@Image1", Converters.ImageConverter.ImageToBytes(this.Image1));
                    cmd.Parameters.AddWithValue("@Image2", Converters.ImageConverter.ImageToBytes(this.Image2));
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
            if (db.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = db.CreateCommand();
                    cmd.CommandText = "DELETE FROM cars WHERE ID = @ID";
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
            this.Image1 = car.Image1;
            this.Image2 = car.Image2;
        }
    }
}
