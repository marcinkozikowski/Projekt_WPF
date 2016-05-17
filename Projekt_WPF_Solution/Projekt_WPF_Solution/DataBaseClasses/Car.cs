using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_WPF_Solution.DataBaseClasses
{
    class Car
    {
        private string maker { get; set; }               // Producent
        private string model { get; set; }               // Model
        private int manufacturedYear { get; set; }       // Data produkcji auta
        private string type { get; set; }               // Klasa/Rodzaj auta terenowe, miejskie, premium
        private double combusion { get; set; }          // Spalanie na 100/km
        private string image { get; set; }              // Zdjecie samochodu
        private bool rented { get; set;  }              // Czy samochod jest aktualnie wypozyczony
        private bool booked { get; set; }               // Czy dany samochod został zarezerwowany

        public Car()
        {
            maker = "";
            model = "";
            manufacturedYear = 0;
            type = "";
            combusion = 0;
            image = "";
            rented = false;
            booked = false;
        }
    }
}
