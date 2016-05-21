using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_WPF_Solution.DataBaseClasses
{
    public class Client
    {
        #region Variables
        private int id;
        private string name, surname, pesel, idNumber;
        private bool isMale;

        #endregion
        #region Properties
        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return Name1; } set { Name1 = value; } }
        public string Surname { get { return surname; } set { surname = value; } }
        public string Pesel { get { return pesel; } set { pesel = value; } }
        public string IdNumber { get { return idNumber; } set { idNumber = value; } }
        public bool IsMale { get { return isMale; } set { isMale = value; } }

        public string Name1
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
        #endregion

        public Client()
        {
            Name1 = surname = pesel = idNumber = string.Empty;
            id = int.MaxValue;
        }
    }
}
