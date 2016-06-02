using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_WPF_Solution.DataBaseClasses
{
    public class BottomPanelData
    {
        private int notReturned, booked, rented;
        public int NotReturned
        {
            get { return notReturned; }
            set { notReturned = value; }
        }
        public int Booked
        {
            get { return booked; }
            set { booked = value; }
        }
        public int Rented
        {
            get { return rented; }
            set { rented = value; }
        }
    }
}
