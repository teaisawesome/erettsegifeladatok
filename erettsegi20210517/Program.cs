using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace erettsegi20210517
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> adatok = new List<int>();

            // Read file using StreamReader. Reads file line by line    
            using (StreamReader file = new StreamReader("melyseg.txt"))
            {
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    adatok.Add(Convert.ToInt32(ln));
                }

                file.Close();
            }

            // 1. feladat
            Console.WriteLine("1. feladat");
            Console.WriteLine("Az adatforrás {0} darab adatot tartalmaz", adatok.Count);

            // 2. feladat
            Console.WriteLine("\n2. feladat");

            int tavolsagErtek;

            do
            {
                Console.Write("Adjon meg egy távolságértéket![max. {0}] ", adatok.Count);
                tavolsagErtek = Convert.ToInt32(Console.ReadLine());
            }
            while (tavolsagErtek > adatok.Count);

            Console.WriteLine("Ezen a helyen a felszín {0} méter mélyen van.", adatok[tavolsagErtek-1]);

            // 3. feladat
            Console.WriteLine("\n3. feladat");
            int szum = 0;

            foreach (int adat in adatok) {
                if (adat == 0) szum++;
            }

            Console.WriteLine("Az érintetlen terület aránya {0:F2}%.", ((double)szum / adatok.Count) * 100);

            int index = 1;

            // 4. feladat
            using (StreamWriter file = new StreamWriter("godrok.txt"))
            {
                List<int> godor = new List<int>();
                

                foreach (int adat in adatok)
                {
                    if (adat != 0) {
                        godor.Add(adat);
                    }

                    if (adat == 0 && godor.Count > 0) {

                        for (int i = 0; i < godor.Count - 1; i++) {
                            file.Write(godor[i] + " ");
                        }

                        file.WriteLine(godor[godor.Count - 1]);

                        godor.Clear();
                    }
                }


                file.Close();
            }

            // gödörindex meghatározás
            using (StreamWriter file2 = new StreamWriter("godrokIndexek.txt"))
            {
                List<int> godorIndexek = new List<int>();

                foreach (int adat in adatok)
                {
                    if (adat != 0)
                    {
                        godorIndexek.Add(index);
                    }

                    if (adat == 0 && godorIndexek.Count > 0)
                    {

                        for (int i = 0; i < godorIndexek.Count - 1; i++)
                        {
                            file2.Write(godorIndexek[i] + " ");
                        }

                        file2.WriteLine(godorIndexek[godorIndexek.Count - 1]);

                        godorIndexek.Clear();
                    }

                    index++;
                }

                file2.Close();
            }

            // 5. feladat
            Console.WriteLine("\n5. feladat");
            int godrokSzama = 0;

            using (StreamReader file = new StreamReader("godrok.txt"))
            {
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    godrokSzama++;
                }

                file.Close();
            }

            Console.WriteLine("A gödrök száma: {0}", godrokSzama);

            // 6. feladat
            Console.WriteLine("\n6. feladat");
            Console.WriteLine("a)");

            int[] tavolsagokMasolat = null;

            int minMeter = 0;
            int maxMeter = 0;

            bool vanGodor = false;

            using (StreamReader file = new StreamReader("godrokIndexek.txt"))
            {
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    string[] tavolsagokString = ln.Split(' ');

                    int[] tavolsagok = Array.ConvertAll(tavolsagokString, int.Parse);

                    if (tavolsagok.Contains(tavolsagErtek))
                    {
                        Console.WriteLine("A gödör kezdete: {0} méter, a gödör vége: {1} méter.", tavolsagok[0], tavolsagok[tavolsagok.Length - 1]);

                        tavolsagokMasolat = tavolsagok;

                        minMeter = tavolsagok[0];

                        maxMeter = tavolsagok[tavolsagok.Length - 1];

                        vanGodor = true;
                    }

                }

                if (!vanGodor)
                {
                    Console.WriteLine("Az adott helyen nincsen gödör.");
                }


                file.Close();
            }

            // c feladatrész
            Console.WriteLine("c)");

            int maxMelyseg = adatok[minMeter - 1];

            for (int i = minMeter - 1; i < maxMeter; i++) {
                if (maxMelyseg < adatok[i]) {
                    maxMelyseg = adatok[i];
                }
            }

            Console.WriteLine("A legnagyobb mélysége {0} méter.", maxMelyseg);

            // d feladatrész
            Console.WriteLine("d)");
            // szélesség 10m
            // mélység majd adott
            // hosszúság 1 m
            int terfogat = 0;

            for (int i = minMeter - 1; i < maxMeter; i++)
            {
                terfogat += (10 * 1 * adatok[i]);                
            }

            Console.WriteLine("A térfogata {0} m^3.", terfogat);

            // e feladatrész
            Console.WriteLine("e)");

            int vizmennyiseg = 0;

            for (int i = minMeter - 1; i < maxMeter; i++)
            {
                if (adatok[i] > 1) {
                    vizmennyiseg += (10 * 1 * (adatok[i] - 1));
                }
            }

            Console.WriteLine("A vízmennyiség {0} m^3.", vizmennyiseg);

            Console.ReadKey();
        }
    }
}
