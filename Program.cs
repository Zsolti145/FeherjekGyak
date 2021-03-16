using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Feherjek
{
    class Elelmiszer
    {
        public string Nev { get; set; }
        public string Kategoria { get; set; }
        public int kJ { get; set; }
        public int kC { get; set; }

        public double Feherje { get; set; }
        public double Zsir { get; set; }
        public double Szenhidrat { get; set; }

        public Elelmiszer(string sor)
        {
            string[] adatok = sor.Split(';');
            Nev = adatok[0];
            Kategoria = adatok[1];
            kJ = int.Parse(adatok[2]);
            kC = int.Parse(adatok[3]);
            Feherje = double.Parse(adatok[4]);
            Zsir = double.Parse(adatok[5]);
            Szenhidrat = double.Parse(adatok[6]);
        }
    }
    class Program
    {
        public static List<Elelmiszer> Beolvas()
        {
            List<Elelmiszer> l = new List<Elelmiszer>();

            FileStream fs = new FileStream("feherjek.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            string sor = sr.ReadLine();
            sor = sr.ReadLine();
            while (sor != null)
            {
                l.Add(new Elelmiszer(sor));
                sor = sr.ReadLine();
            }


            sr.Close();
            fs.Close();

            return l;
        }
        static void Main(string[] args)
        {
            #region feladat2
            List<Elelmiszer> elemiszerek = Beolvas();
            #endregion

            #region feladat3
            Console.WriteLine("3.Feladat: Élelmiszerek száma: " + elemiszerek.Count);
            #endregion

            #region feladat4
            var res4 = from etel in elemiszerek
                       where etel.Feherje ==
                (from kaja in elemiszerek
                 select kaja.Feherje).Max()
                       select new
                       {
                           etel.Nev,
                           etel.Kategoria,
                           etel.Feherje
                       };
            Console.WriteLine();

            Console.WriteLine("4.feladat: A legnagyobb feherje tartalom: ");
            foreach (var item in res4)
            {
                Console.WriteLine("Étel neve: \t" + item.Nev);
                Console.WriteLine("Kategoriája: \t" + item.Kategoria);
                Console.WriteLine("Mennyiség: \t" + item.Feherje + "gramm");
            }

            #endregion
            Console.WriteLine();
            #region feladat5
            var res5 = (from etel in elemiszerek
                        where etel.Kategoria.ToLower().Equals("Gabonafélék".ToLower())
                        select etel.Feherje).Average();
            Console.WriteLine("5.Felat: Gabonafélék átlagos fehérjetartalma: " + res5 + " gramm");
            #endregion

            #region feladat6
            Console.Write("6.Feladadt: Kérek egy karakterláncot: ");
            string be = Console.ReadLine();
            var res6 = from etel in elemiszerek
                       where etel.Nev.ToLower().Contains(be.ToLower())
                       select new
                       {
                           nev = "Név: " + etel.Nev + " ",
                           kat = "Kategória: " + etel.Kategoria + " ",
                           feh = "Fehérje: " + etel.Feherje + " gramm"
                       };

            if (res6.Count() > 0)
                foreach (var item in res6)
                    Console.WriteLine("\t" + item.nev + item.kat + item.feh);
            else
                Console.WriteLine("\t Nincs egyezés");

            #endregion

            #region feladat7
            var res7 = from etel in elemiszerek
                       group etel by etel.Kategoria into kategoriak
                       where kategoriak.Count() < 10
                       select new
                       {
                           Etelkat = kategoriak.Key,
                           KatDB = kategoriak.Count()
                       };
            Console.WriteLine("7.Feladat Statisztika");

            foreach (var item in res7)
                Console.WriteLine("\t" + item.Etelkat + " - " + item.KatDB);

            #endregion


            Console.WriteLine("8.Feladat gaabonafele.txt");
            var res8 = from etel in elemiszerek
                       where etel.Kategoria.ToLower().Equals("Gabonafélék".ToLower())
                       select new
                       {
                           etel.Nev,
                           etel.Feherje
                       };

            FileStream fs = new FileStream("gabonafelek.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine("Nev;Feherje");
            foreach (var items in res8)
                sw.WriteLine(items.Nev + ";" + items.Feherje);

            sw.Close();
            fs.Close();


            Console.ReadKey();
        }
    }
}
