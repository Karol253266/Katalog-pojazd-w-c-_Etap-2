using Kozik.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kozik
{
    class Program
    {
        private static List<Pojazd> pojazdy;
        private const string NAZWA_KATALOGU = "Katalog.json";

        static void Main(string[] args)
        {
            pojazdy = new List<Pojazd>();

            while (Menu())
            {

            }
        }


        private static bool Menu()
        {
            try
            {
                Console.WriteLine("MENU:\n1.Wypisz wszystkie pojazdy\n2.Wyswietlanie po indeksie\n3.Wprowadz nowy samochod\n4. {...} \n5.Wczytaj katalog\n6.Zapisz katalog\n7.Usun samochod\n8.Wyszukaj po parametrach");
                Console.Write("Wybor --> ");
                int wybor = Convert.ToInt32(Console.ReadLine());

                switch (wybor)
                {
                    case 1:
                        foreach (Pojazd p in pojazdy)
                        {
                            Console.WriteLine(p.ToString());
                        }
                        break;
                    case 2:
                        WyswietleniePoIndeksie();
                        break;
                    case 3:
                        WprowadzNowySamochod();
                        break;
                    case 4:
                        WyswietlaniePoStalychParametrach();
                        break;
                    case 5:
                        WczytajKatalog();
                        break;
                    case 6:
                        ZapiszKatalog();
                        break;
                    case 7:
                        UsunSamochodPoIndeksie();
                        break;
                    case 8:
                        WyszukaniePoParametrach();
                        break;
                    case 9:
                        return false;
                    default:
                        Console.WriteLine("Nieprawidlowa opcja...");
                        break;
                }
                Console.WriteLine("Gotowe.");
                Console.ReadKey();
                Console.Clear();
                return true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return true;
            }
        }

        private static void ZapiszKatalog()
        {
            try
            {
                string pojazdyJson = JsonConvert.SerializeObject(pojazdy);
                File.WriteAllText(NAZWA_KATALOGU, pojazdyJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Blad podczas zapisu pliku: " + ex.Message);
                return;
            }
        }

        private static void WczytajKatalog()
        {

            if (!File.Exists(NAZWA_KATALOGU))
            {
                File.Create(NAZWA_KATALOGU);
            }

            string zawartoscPliku;
            try
            {
                zawartoscPliku = File.ReadAllText(NAZWA_KATALOGU);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Blad podczas odczytywania zawartosci pliku : " + ex.Message);
                return;
            }

            if (string.IsNullOrWhiteSpace(zawartoscPliku.Trim()))
            {
                Console.WriteLine("Plik jest pusty.");
                return;
            }

            try
            {
                pojazdy = JsonConvert.DeserializeObject<List<Pojazd>>(zawartoscPliku);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Blad spojnosci danych z pliku: " + ex.Message);
                return;
            }
        }


        private static void WyszukaniePoParametrach()
        {
            try
            {
                string markaFilter;
                string modelFilter;
                int rocznikFilter;
                int minPrzedzialFilter;
                int maxPrzedzialFilter;

                Console.Write("Podaj marke: ");
                markaFilter = Console.ReadLine();
                Console.WriteLine("Podaj model");
                modelFilter = Console.ReadLine();
                Console.WriteLine("Podaj rocznik");
                rocznikFilter = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Podaj minimalny przebieg");
                minPrzedzialFilter = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Podaj maksymalny przebieg");
                maxPrzedzialFilter = Convert.ToInt32(Console.ReadLine());

                List<Pojazd> przefiltrowanaLista = pojazdy.Where(
                    x =>
                        x.Marka.ToLower().Equals(markaFilter) &&
                        x.Model.ToLower().Equals(modelFilter) &&
                        x.Rocznik == rocznikFilter &&
                        x.Przebieg >= minPrzedzialFilter &&
                        x.Przebieg <= maxPrzedzialFilter
                    ).ToList();

                foreach (Pojazd p in przefiltrowanaLista)
                {
                    Console.WriteLine(p.ToString());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Blad podczas wyszukiwania po parametrach: " + ex.Message);
            }
        }

        private static void WyswietlaniePoStalychParametrach()
        {
            try
            {


                List<Pojazd> przefiltrowanaLista = pojazdy.Where(
                    x =>
                        x.Przebieg < 100000
                         || x.Pojemnosc < 100
                    ).ToList();

                foreach (Pojazd p in przefiltrowanaLista)
                {
                    Console.WriteLine(p.ToString());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Blad podczas wyszukiwania po parametrach: " + ex.Message);
            }
        }

        private static void WprowadzNowySamochod()
        {
            try
            {
                Pojazd p = new Pojazd();
                p.UzupelnijModel();
                pojazdy.Add(p);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Blad poczas wprowadzania samochodu: " + ex.Message);
            }
        }

        private static void UsunSamochodPoIndeksie()
        {
            if (pojazdy.Count == 0)
            {
                Console.WriteLine("Nie ma w bazie zadnego pojazdu.");
            }
            else
            {
                Console.WriteLine("Jezeli chcesz usunac samochod po indeksie musisz zmiescic sie w zakresie 0 - " + (pojazdy.Count - 1));
                int indeks = Convert.ToInt32(Console.ReadLine());
                if (indeks < 0 || indeks >= pojazdy.Count)
                {
                    Console.WriteLine("Wyszedles poza zakres.");
                }
                else
                {
                    pojazdy.RemoveAt(indeks);
                }
            }
        }


        private static void WyswietleniePoIndeksie()
        {
            if (pojazdy.Count == 0)
            {
                Console.WriteLine("Nie ma w bazie zadnego pojazdu.");
            }
            else
            {
                Console.WriteLine("Jezeli chcesz wybrac samochod po indeksie musisz zmiescic sie w zakresie 0 - " + (pojazdy.Count - 1));
                int indeks = Convert.ToInt32(Console.ReadLine());
                if (indeks < 0 || indeks >= pojazdy.Count)
                {
                    Console.WriteLine("Wyszedles poza zakres.");
                }
                else
                {
                    Console.WriteLine(pojazdy.ElementAt(indeks).ToString());
                }
            }
        }
    }
}
