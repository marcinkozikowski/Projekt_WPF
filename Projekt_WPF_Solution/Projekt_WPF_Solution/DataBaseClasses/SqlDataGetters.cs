using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_WPF_Solution.DataBaseClasses;
using System.Windows.Media.Imaging;

namespace Projekt_WPF_Solution
{
    public static class SqlDataGetters
    {
        #region Variables
        private static ObservableCollection<Car> cars = new ObservableCollection<Car>();
        private static ObservableCollection<Client> clients = new ObservableCollection<Client>();
        private static ObservableCollection<Rent> rents = new ObservableCollection<Rent>();
        private static List<string> brands = new List<string>();
        private static List<string> bodyTypes = new List<string>();
        private static List<DataBaseClasses.Type> cartypes = new List<DataBaseClasses.Type>();
        private static BottomPanelData bottomPanel = new BottomPanelData();

        #endregion
        #region Properties
        public static ObservableCollection<Car> Cars { get { return cars; } set { cars = value; } }
        public static ObservableCollection<Client> Clients { get { return clients; } set { clients = value; } }
        public static ObservableCollection<Rent> Rents { get { return rents; } set { rents = value; } }
        public static List<string> Brands { get { return brands; } set { brands = value; } }
        public static List<string> BodyTypes { get { return bodyTypes; } set { bodyTypes = value; } }
        public static List<DataBaseClasses.Type> CarTypes { get { return cartypes; } set { cartypes = value; } }
        public static BottomPanelData BottomPanel { get { return bottomPanel; } set { bottomPanel = value; } }
        #endregion

        public static List<Car> GetAvailableCars(DateTime? start, DateTime? end)
        {
            List<Car> cars = new List<Car>(Cars);
            if(start.HasValue && end.HasValue)
            {
                foreach (Rent rental in Rents)
                {
                    if ((start.Value.Date <= rental.RentEnd.Date && end.Value.Date >= rental.RentStart.Date)) //Jeżeli oba "zbiory" dat pokrywają się (w dany termin pojazd jest wypożyczony)
                    {
                        cars.Remove(rental.RentedCar);
                    }                
                }
            }
            return cars;
        }
        public static void GetAll()
        {
            GetTypes();
            GetCars();
            GetClients();
            GetRents();
            GetBrands();
            GetBodyTypes();
            GetBottomPanelInfo();
        }

        private static void GetTypes()
        {
            cartypes.Clear();
            IDBaccess db = new IDBaccess();
            if (db.OpenConnection() == true)
            {
                MySqlCommand cmd = db.CreateCommand();
                cmd.CommandText = "SELECT ID, Type, Price from car_type";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DataBaseClasses.Type newType = new DataBaseClasses.Type(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2));
                    CarTypes.Add(newType);
                }
                db.CloseConnection();
            }
        }
        private static void GetCars()
        {
            cars.Clear();
            IDBaccess db = new IDBaccess();
            if (db.OpenConnection() == true)
            {
                MySqlCommand cmd = db.CreateCommand();
                cmd.CommandText = "SELECT ID, RegPlate, Maker, Model, ManufacturedYear, Engine, Type, BodyType, FuelConsumption FROM cars";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    DataBaseClasses.Type type = SqlDataGetters.CarTypes.Single(i => i.Id == reader.GetInt32(6));
                    BitmapImage image = Converters.ImageConverter.GetImage("SELECT Image FROM cars WHERE ID = @ID", id);
                    Car newCar = new Car(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), type, reader.GetString(7), reader.GetDouble(8), image);
                    Cars.Add(newCar);
                }
                db.CloseConnection();
            }
        }
        private static void GetClients()
        {
            clients.Clear();
            IDBaccess db = new IDBaccess();
            if (db.OpenConnection() == true)
            {
                MySqlCommand cmd = db.CreateCommand();
                cmd.CommandText = "SELECT ID, Pesel, Name, Surname, Born, IsMale, PhoneNumber, Address, City, Type, Image from Clients";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    BitmapImage image = Converters.ImageConverter.GetImage("Select Image FROM clients Where ID = @ID", id);
                    Client newClient = new Client(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4), reader.GetBoolean(5), reader.GetInt32(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), image);
                    Clients.Add(newClient);
                }
                db.CloseConnection();
            }
        }
        private static void GetRents()
        {
            rents.Clear();
            IDBaccess db = new IDBaccess();
            if (db.OpenConnection() == true)
            {
                MySqlCommand cmd = db.CreateCommand();
                cmd.CommandText = "SELECT ID, CarID, ClientID, RentStart, RentEnd, isReturned from rents";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    Car car = Cars.Single(i => i.ID == reader.GetInt32(1));
                    Client client = Clients.Single(i => i.ID == reader.GetInt32(2));
                    DateTime start = reader.GetDateTime(3);
                    DateTime end = reader.GetDateTime(4);
                    bool isReturned = reader.GetBoolean(5);

                    Rent rent = new Rent(id, start, end, car, client, isReturned);
                    Rents.Add(rent);
                }
                db.CloseConnection();
            }
        }
        private static void GetBrands()
        {
            Brands.Clear();
            IDBaccess db = new IDBaccess();
            if (db.OpenConnection() == true)
            {
                MySqlCommand cmd = db.CreateCommand();
                cmd.CommandText = "SELECT brand from car_brand";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Brands.Add(reader.GetString(0));
                }
                db.CloseConnection();
            }
        }
        private static void GetBodyTypes()
        {
            BodyTypes.Clear();
            IDBaccess db = new IDBaccess();
            if (db.OpenConnection() == true)
            {
                MySqlCommand cmd = db.CreateCommand();
                cmd.CommandText = "SELECT bodytype from car_bodytype";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BodyTypes.Add(reader.GetString(0));
                }
                db.CloseConnection();
            }
        }
        private static void GetBottomPanelInfo()
        {
            bottomPanel.Booked = bottomPanel.NotReturned = bottomPanel.Rented = 0;
            foreach(Rent rent in Rents)
            {
                if(DateTime.Today.Date < rent.RentStart)
                {
                    bottomPanel.Booked++;
                }
                else if(!rent.IsReturned && DateTime.Today.Date > rent.RentEnd)
                {
                    bottomPanel.NotReturned++;
                }
                else if (DateTime.Today.Date <= rent.RentEnd.Date && DateTime.Today.Date >= rent.RentStart.Date)
                {
                    bottomPanel.Rented++;
                }                    
            }

        }

    }
}
