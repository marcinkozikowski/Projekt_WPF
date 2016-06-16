using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_WPF_Solution.DataBaseClasses
{
    public class Rent : IDataErrorInfo
    {
        #region Variables
        int id;
        DateTime rentStart, rentEnd;
        Car rentedCar;
        Client rentingPerson;
        bool isReturned;
        #endregion
        #region Properties
        public int ID { get { return id; } set { id = value; } }
        public DateTime RentStart { get { return rentStart; } set { rentStart = value; } }
        public DateTime RentEnd { get { return rentEnd; } set { rentEnd = value; } }
        public Car RentedCar { get { return rentedCar; } set { rentedCar = value; } }
        public Client RentingPerson { get { return rentingPerson; } set { rentingPerson = value; } }
        public bool IsReturned { get { return isReturned; } set { isReturned = value; } }


        #endregion

        public Rent()
        {
            RentStart = RentEnd = DateTime.Today.Date;
        } 
        public Rent(int id, DateTime start, DateTime end, Car car, Client client, bool isReturned)
        {
            this.id = id;
            this.rentStart = start;
            this.rentEnd = end;
            this.rentedCar = car;
            this.rentingPerson = client;
            this.isReturned = isReturned;
        }
        public Rent(Rent rent)
        {
            this.id = rent.id;
            this.rentStart = rent.rentStart;
            this.rentEnd = rent.rentEnd;
            this.rentedCar = rent.rentedCar;
            this.rentingPerson = rent.rentingPerson;
            this.isReturned = rent.isReturned;
        }

        #region SQL
        public bool SqlInsert()
        {
            IDBaccess db = new IDBaccess();
            if (db.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = db.CreateCommand();
                    cmd.CommandText = "INSERT INTO rents (ID, CarID, ClientID, RentStart, RentEnd, isReturned) VALUES (@ID, @CarID, @ClientID, @RentStart, @RentEnd, @isReturned)";
                    cmd.Parameters.AddWithValue("@ID", this.ID);
                    cmd.Parameters.AddWithValue("@CarID", this.RentedCar.ID);
                    cmd.Parameters.AddWithValue("@ClientID", this.RentingPerson.ID);
                    cmd.Parameters.AddWithValue("@RentStart", this.RentStart);
                    cmd.Parameters.AddWithValue("@RentEnd", this.RentEnd);
                    cmd.Parameters.AddWithValue("@isReturned", this.isReturned);
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
                    cmd.CommandText = "UPDATE rents SET CarID = @CarID, ClientID = @ClientID, RentStart = @RentStart, RentEnd = @RentEnd, isReturned = @isReturned WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", this.ID);
                    cmd.Parameters.AddWithValue("@CarID", this.RentedCar.ID);
                    cmd.Parameters.AddWithValue("@ClientID", this.RentingPerson.ID);
                    cmd.Parameters.AddWithValue("@RentStart", this.RentStart);
                    cmd.Parameters.AddWithValue("@RentEnd", this.RentEnd);
                    cmd.Parameters.AddWithValue("@isReturned", this.isReturned);
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
                    cmd.CommandText = "DELETE FROM rents WHERE ID = @ID";
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

        public void PropertyUpdate(Rent rent)
        {
            this.id = rent.id;
            this.rentStart = rent.rentStart;
            this.rentEnd = rent.rentEnd;
            this.rentedCar = rent.rentedCar;
            this.rentingPerson = rent.rentingPerson;
            this.isReturned = rent.isReturned;
        }

        public override string ToString()
        {
            string toString = string.Empty;
            if(rentingPerson != null)
            {
                toString += rentingPerson.ToString();
            }
            if(rentedCar != null)
            {
                toString += " " + rentedCar.ToString();
            }

            return toString;
        }
        #region IDataErrorInfo
        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                if(columnName.Equals("RentStart") || columnName.Equals("RentEnd"))
                {
                    if(RentStart > rentEnd)
                    {
                        return "Data Od musi być wcześniejsza";
                    }
                }
                return null;
            }
        }
        #endregion
    }
}
