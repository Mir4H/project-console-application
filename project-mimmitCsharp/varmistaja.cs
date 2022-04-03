using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_mimmitCsharp
{
    class varmistaja
    {

        public bool varmistus(bool tulos)
        {

            Console.WriteLine("Ovatko antamasi tiedot oikein vai haluatko syöttää ne uudelleen?\nV = Vahvista / U = Uudelleen");
            var vastaus = Console.ReadKey().KeyChar.ToString().ToLower(); //käyttäjän antama vastaus
            Console.WriteLine();

            while (vastaus != "v" && vastaus != "u") //jos käyttäjä antaa muun kirjaimen/numeron 
            {
                Console.WriteLine("Anna v tai u"); //tulostuu tämä
                vastaus = Console.ReadLine(); 
            }


            switch (vastaus) //jos käyttäjän antama vastaus on v, palauttaa false arvon, jos u palauttaa true arvon
            {
                case "v":
                    tulos = false;
                    break;

                case "u":
                    tulos = true;
                    break;

            }

            return tulos;
        }

        public bool myyntiedustajanVahvistus(ref bool jatketaan, int valittuMyyntiedustaja)
        {
            Console.WriteLine($"=====================================");
            Console.WriteLine($"Valitsemasi myyntiedustajan ID on: {valittuMyyntiedustaja}");
            Console.WriteLine($"=====================================");
            Console.WriteLine($"Haluatko syöttää matkan myyntiedustajalle {valittuMyyntiedustaja}?\nK = Kyllä / E = Ei");
            var jatketaanko = Console.ReadKey().KeyChar.ToString().ToLower(); //käyttäjän antama vastaus
            Console.WriteLine();

            while (jatketaanko != "k" && jatketaanko != "e") //jos käyttäjä antaa muun kirjaimen/numeron 
            {
                Console.WriteLine("Anna k tai e"); //tulostuu tämä
                jatketaanko = Console.ReadLine();
            }


            switch (jatketaanko)
            {
                case "k":
                    jatketaan = true;
                    break;

                case "e":
                    jatketaan = false;
                    Console.WriteLine($"Paina mitä tahansa näppäintä palataksesi päävalikkoon.");
                    break;

            }
            return jatketaan;
        }




    }
}