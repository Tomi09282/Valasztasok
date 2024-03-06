using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szabo_Tamas_Valasztasokű
{

    internal class Valasztas
    {
        static List<Valasztas> valasztasok = new List<Valasztas>();
        int sorszam, szavazatok;
        string vnev, knev;
        string part;

        public Valasztas(int sorszam, int szavazatok, string vnev, string knev, string part)
        {
            this.sorszam = sorszam;
            this.szavazatok = szavazatok;
            this.vnev = vnev;
            this.knev = knev;
            this.part = part;
        }

        public int Sorszam { get => sorszam; set => sorszam = value; }
        public int Szavazatok { get => szavazatok; set => szavazatok = value; }
        public string Vnev { get => vnev; set => vnev = value; }
        public string Knev { get => knev; set => knev = value; }
        public string Part { get => part; set => part = value; }

        public static void Beolvas()
        {
            StreamReader sr = new("szavazatok.txt");


            while (!sr.EndOfStream)
            {
                string[] adatok = sr.ReadLine().Trim().Split(' ');
                if (adatok[4] == "-")
                {
                    adatok[4] = "Fuggetlen";
                }

                valasztasok.Add(new Valasztas(int.Parse(adatok[0]), int.Parse(adatok[1]), adatok[2], adatok[3], adatok[4]));
            }
        }


        public static int Darab()
        {
            return valasztasok.Count();
        }

        public static int SzavazatokDB(string vnev, string knev)
        {
             return valasztasok.Where(x => x.vnev == vnev && x.knev == knev).Select(x => x.szavazatok).ToList()[0];
        } 

        public static void Arany()
        {
            int jsz = 12345;
            double arany = (double)valasztasok.Sum(x => x.szavazatok) / jsz * 100;

            Console.WriteLine($"A választáson {valasztasok.Sum(x => x.szavazatok)} állampolgár {Math.Round(arany,2)}% a vett részt");
        }

        public static void PartArany()
        {
            var partok = valasztasok.GroupBy(x => x.part).Select(x => new { part = x.Key, szavazatok = x.Sum(x => x.szavazatok) }).OrderByDescending(x => x.szavazatok);
            Console.WriteLine("Pártokra leadott szavazatok aranya:");
            foreach (var part in partok)
            {
                double arany = (double)part.szavazatok / valasztasok.Sum(x => x.szavazatok) * 100;
                Console.WriteLine($"{part.part}= {Math.Round(arany,2)}%");
            }
        }

        public static void LegtobbSzavazat()
        {
            int legtobbSzavazat = valasztasok.Max(x => x.szavazatok);
            var legtobbetKapok = valasztasok.Where(x => x.szavazatok == legtobbSzavazat);
            Console.WriteLine("Legtöbbet kapták:");
            foreach (var kv in legtobbetKapok)
            {
                Console.WriteLine($"{kv.vnev} {kv.knev} : {kv.part}");
            }
        }


        public static void Gyoztesek()
        {
            for (int i = 1; i < 9; i++)
            {
                int darab = valasztasok.Where(x => x.sorszam == i).Max(x => x.szavazatok);
                Console.WriteLine($"{i}. kerulet: {darab}DB szavazattal {valasztasok.Where(x => x.szavazatok == darab && x.sorszam == i).Select(x => x.vnev).ToList()[0]} {valasztasok.Where(x => x.szavazatok == darab).Select(x => x.knev).ToList()[0]}");
            }

        }
    }
}
