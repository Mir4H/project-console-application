using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace project_mimmitCsharp
{
    class maksamattomatLaskut
    {
        int MaksuID = 0;
        bool vahvistus = true;
        
        
        
        
        public void Maksaminen()
        {
            

            Console.WriteLine("Maksettavat laskut");
            Console.WriteLine("==================");
            List<yksittäinenMaksu> avoimet = maksunTiedot.HaeAvoimetLaskut();

            for (int j = 0; j < avoimet.Count; j++) //käy maksamattomien listan läpi ja tulostaa ne
            {
                Console.WriteLine(avoimet[j]);
                Console.WriteLine("==================");
            }
            if (!avoimet.Any())
            {
                Console.WriteLine("Ei maksettavia laskuja.\nPaina mitä tahansa näppäintä palataksesi päävalikkoon.");
                vahvistus = false;

            }

            

            while (vahvistus) //Palauttaa maksun IDn
            {
                MaksuID = laskunValinta(); //Kysytään matkan id
                int index = avoimet.FindIndex(a => a.matkanId == MaksuID); //Etsii rivin ideksin valitulle maksulle
                try
                {

                    avoimet[index].maksuAika = DateTime.Now; //Lisää maksulle maksuajankohdan tiedon
                    avoimet[index].maksettu = true; //merkkaa maksu maksetuksi
                    bool maksuOk = MaksaMatka(); //Kysytään maksetaanko
                    if (maksuOk)
                    {
                        JsonTiedostonHallinta.TallennaMaksut(avoimet.ToArray()); //tallentaa maksamattomat json tiedostoon.
                        vahvistus = false;
                    }
                    else
                    {
                        vahvistus = true;
                    }

                }
                catch
                {
                    Console.WriteLine("Antamaasi maksua ei löytynyt. Syötä maksettava maksu uudelleen.");
                    vahvistus = true;
                }
                
            }
           
        
           
        

        int laskunValinta() //Kysyy maksettavan maksun numeron ja palauttaa sen
        {
            int valittuMaksu;
            Console.WriteLine("Valitse maksettavan matkan ID: ");
            while (!int.TryParse(Console.ReadLine(), out valittuMaksu))
                Console.WriteLine("Antamaasi maksua ei löytynyt. Syötä maksettava maksu uudelleen:");
            return valittuMaksu;
        }

        bool MaksaMatka() //Kysyy halutaanko matka maksaa, palauttaa vastauksen
        {
            bool Ok = false;
            Console.WriteLine("Haluatko maksaa valitsemasi matkan? K = Kyllä, E = Ei");
            char vastaus = Console.ReadKey().KeyChar; //käyttäjän antama vastaus
            Console.WriteLine();

            while (vastaus != 'k' && vastaus != 'e') //jos käyttäjä antaa muun kirjaimen/numeron 
            {
                Console.WriteLine("Anna K tai E"); //tulostuu tämä
                vastaus = Console.ReadKey().KeyChar;
                Console.WriteLine();
            }

            switch (vastaus)
            {
                    case 'k': //Kerrotaan mikä matka maksetaan
                    Console.WriteLine($"Merkitään matka ID {MaksuID} maksetuksi.");
                    Console.WriteLine("Paina mitä tahansa näppäintä palataksesi päävalikkoon");
                    Ok = true;
                    break;

                case 'e': //Kerrotaan, että matkaa ei maksettu
                    Console.WriteLine($"Matkaa ei maksettu.");
                    Ok = false;
                    break;
            }

            return Ok;
        }
            
        }


    }
}
