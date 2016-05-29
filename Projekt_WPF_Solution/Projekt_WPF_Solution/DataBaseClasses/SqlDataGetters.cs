using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_WPF_Solution.DataBaseClasses;

namespace Projekt_WPF_Solution
{
    public static class SqlDataGetters
    {
        #region Variables
        private static ObservableCollection<Car> cars;
        private static ObservableCollection<Client> clients;
        private static ObservableCollection<Rent> rents;
        private static List<string> brands;
        private static List<string> bodyTypes;

        #endregion
        #region Properties
        public static ObservableCollection<Car> Cars { get { return cars; } set { cars = value; } }
        public static ObservableCollection<Client> Clients { get { return clients; } set { clients = value; } }
        public static ObservableCollection<Rent> Rents { get { return rents; } set { rents = value; } }
        public static List<string> Brands { get { return brands; } set { brands = value; } }
        public static List<string> BodyTypes { get { return bodyTypes; } set { bodyTypes = value; } }
        #endregion

        public static void GetAll()
        {
            GetCars();
            GetClients();
            GetRents();
            GetBrands();
            GetBodyTypes();
        }
        private static void GetCars()
        {
            Cars = new ObservableCollection<Car>();
            IDBaccess db = new IDBaccess();
            if (db.OpenConnection() == true)
            {
                MySqlCommand cmd = db.CreateCommand();
                cmd.CommandText = "SELECT RegPlate, Maker, Model, ManufacturedYear, Engine, Type, BodyType, FuelConsumption, Image FROM cars";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Car newCar = new Car(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6), reader.GetDouble(7), reader.GetString(8));
                    Cars.Add(newCar);
                }
                db.CloseConnection();
            }
        }
        private static void GetClients()
        {
            Clients = new ObservableCollection<Client>();
            IDBaccess db = new IDBaccess();
            if (db.OpenConnection() == true)
            {
                MySqlCommand cmd = db.CreateCommand();
                cmd.CommandText = "SELECT Pesel, Name, Surname, Born, IsMale, PhoneNumber, Address, City, Type, Image from Clients";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Client newClient = new Client(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetBoolean(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                    Clients.Add(newClient);
                }
                db.CloseConnection();
            }
        }
        private static void GetRents()
        {
            Rents = new ObservableCollection<Rent>();
            IDBaccess db = new IDBaccess();
            if (db.OpenConnection() == true)
            {
                MySqlCommand cmd = db.CreateCommand();
                cmd.CommandText = "SELECT ID, CarID, ClientID, RentStart, RentEnd from rents";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    Car car = Cars.Single(i => i.ID == reader.GetInt32(1));
                    Client client = Clients.Single(i => i.ID == reader.GetInt32(2));
                    DateTime start = reader.GetDateTime(3);
                    DateTime end = reader.GetDateTime(4);

                    Rent rent = new Rent(id, start, end, car, client);
                    Rents.Add(rent);
                }
                db.CloseConnection();
            }
        }
        private static void GetBrands()
        {
            Brands = new List<string>();
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
            BodyTypes = new List<string>();
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

    }
}
