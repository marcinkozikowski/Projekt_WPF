using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_WPF_Solution.DataBaseClasses
{
    public class Rent
    {
        #region Variables
        int id;
        DateTime rentStart, rentEnd;
        Car rentedCar;
        Client rentingPerson;
        #endregion
        #region Properties
        public int Id { get { return id; } set { id = value; } }
        public DateTime RentStart { get { return rentStart; } set { rentStart = value; } }
        public DateTime RentEnd { get { return rentEnd; } set { rentEnd = value; } }
        public Car RentedCar { get { return rentedCar; } set { rentedCar = value; } }
        public Client RentingPerson { get { return rentingPerson; } set { rentingPerson = value; } }
        #endregion

        public Rent()
        {

        } 
        public Rent(int id, DateTime start, DateTime end, Car car, Client client)
        {
            this.id = id;
            this.rentStart = start;
            this.rentEnd = end;
            this.rentedCar = car;
            this.rentingPerson = client;
        }

        #region SQL
        
        #endregion
    }
}
