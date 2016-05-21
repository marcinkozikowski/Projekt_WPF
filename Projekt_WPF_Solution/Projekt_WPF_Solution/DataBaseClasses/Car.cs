using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_WPF_Solution.DataBaseClasses
{
    public class Car
    {
        #region Variables
        private string maker { get; set; }               // Producent
        private string model { get; set; }               // Model
        private int manufacturedYear { get; set; }       // Data produkcji auta
        private decimal engine { get; set; }            // Pojemność silnika
        private string type { get; set; }               // Klasa/Rodzaj auta terenowe, miejskie, premium
        private string bodyType { get; set; }           //Rodzaj nadwozia
        private double fuelConsumption { get; set; }     // Spalanie na 100/km
        private string regPlate { get; set; }           //Numery rejestacyjne
        private string image { get; set; }              // Zdjecie samochodu
        private bool rented { get; set; }              // Czy samochod jest aktualnie wypozyczony
        private bool booked { get; set; }               // Czy dany samochod został zarezerwowany
        #endregion

        #region Properties
        public string Maker { get { return maker; } set { maker = value; } }
        public string Model { get { return model; } set { model = value; } }
        public int ManufacturedYear { get { return manufacturedYear; } set { manufacturedYear = value; } }
        public decimal Engine {  get { return engine; } set { engine = value; } }
        public string Type { get { return type; } set { type = value; } }
        public string BodyType { get { return bodyType; } set { bodyType = value; } }
        public string RegPlate { get { return regPlate; } set { regPlate = value; } }
        public double FuelConsumption { get { return fuelConsumption; } set { fuelConsumption = value; } }
        public string Image { get { return image; } set { image = value; } }
        public bool Rented { get { return rented; } set { rented = value; } }
        public bool Booked { get { return booked; } set { booked = value; } }

        #endregion


        public Car()
        {
            maker = string.Empty;
            model = string.Empty; ;
            manufacturedYear = 0;
            type = string.Empty; ;
            fuelConsumption = 0;
            image = string.Empty; ;
            rented = false;
            booked = false;
        }
    }
}
