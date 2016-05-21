using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_WPF_Solution.DataBaseClasses
{
    public class Rent
    {
        DateTime rentStart, rentEnd;
        Car rentedCar;
        Client rentingPerson;
        
        public Rent()
        {

        } 
        public Rent(DateTime start, DateTime end, Car car, Client client)
        {
            this.rentStart = start;
            this.rentEnd = end;
            this.rentedCar = car;
            this.rentingPerson = client;
        }
    }
}
