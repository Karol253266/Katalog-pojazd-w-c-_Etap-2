using System;
using System.Collections.Generic;
using System.Text;

namespace Kozik.Model
{
    public class Pojazd
    {
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Rocznik { get; set; }
        public int Pojemnosc { get; set; }
        public int Przebieg { get; set; }
        public bool Automat { get; set; }

        public override string ToString()
        {
            return "Marka: " + Marka + " Model:" + Model + " Rocznik:" + Rocznik + " " + Pojemnosc + "cm^3 " + Przebieg + "km " + (Automat ? "Automat" : "Manual");
        }

        public void UzupelnijModel()
        {
            try
            {
                Console.Write("Podaj marke:");
                Marka = Console.ReadLine();

                Console.Write("Podaj model:");
                Model = Console.ReadLine();

                Console.Write("Podaj rocznik:");
                Rocznik = Convert.ToInt32(Console.ReadLine());

                Console.Write("Podaj pojemnosc:");
                Pojemnosc = Convert.ToInt32(Console.ReadLine());

                Console.Write("Podaj przebieg:");
                Przebieg = Convert.ToInt32(Console.ReadLine());

                bool koniecSkrzyniaBiegow = true;
                while (koniecSkrzyniaBiegow)
                {
                    Console.WriteLine("Automat - A, Manual - M");

                    string wybor = Console.ReadLine().ToUpper();
                    switch (wybor)
                    {
                        case "A":
                            Automat = true;
                            koniecSkrzyniaBiegow = false;
                            break;
                        case "M":
                            Automat = false;
                            koniecSkrzyniaBiegow = false;
                            break;
                        default:
                            Console.WriteLine("Bledny wybor.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
