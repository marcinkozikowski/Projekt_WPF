using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_WPF_Solution.DataBaseClasses
{
    public class Type 
    {
        #region Variables
        private int id;
        private string cartype;
        private double price; 
        #endregion
        #region Properties
        public int Id { get { return id; } set { id = value; } }
        public string CarType { get { return cartype; } set { cartype = value; } }
        public double Price { get { return price; } set { price = value; } }

        #endregion
        #region Constructors
        public Type()
        {
            this.cartype = string.Empty;
        }

        public Type(int idu,string typeu,double priceu)
        {
            this.id = idu;
            this.cartype = typeu;
            this.price = priceu;
        }

        public Type(Type t)
        {
            this.id = t.id;
            this.cartype = t.cartype;
            this.price = t.price;
        }
        #endregion
    }


}
