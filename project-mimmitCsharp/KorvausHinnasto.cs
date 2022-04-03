using System;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace project_mimmitCsharp
{
    public class KorvausHinnasto
    {
        public string[] kulkuneuvo;
        public double[] hinta;
        public int vuosi = 2021;

        public KorvausHinnasto()
        {
            // luo ja lisää palkansaajan omalla kulkuneuvolla matkojen korvaus max. euro määrät kilmometrilta
            kulkuneuvo = new string[]
            {
                "Auto, perus korvaus",
                "Auto, perävauna lisä",
                "Auto, asuntovaunu lisä",
                "Auto, raskas kuorma lisä",
                "Auto, suuri esine lisä",
                "Auto, koira lisä",
                "Auto, rajoitettu liikumisalue lisä",
                "Moottorivene enintään 50hv",
                "Moottorivene yli 50hv",
                "Moottorikelkka",
                "Mönkijä",
                "Moottoripyörä",
                "Mopo",
                "Muu kulkuneuvo",
                "Mukana yksi henkilö (tai mukana olevien henkilöiden määrä kerrotaan korvausmäärä)",
                "Työauto käytössä, korvaus työhön liittyvästä bensaostoksesta"
            };


            hinta = new double[]
            {
                0.44,
                0.07,
                0.11,
                0.22,
                0.03,
                0.03,
                0.09,
                0.76,
                1.11,
                1.06,
                0.99,
                0.33,
                0.18,
                0.10,
                0.03,
                0.10
            };
        }


        public void naytaHinnasto()
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n ***** Kilometri korvaushinnasto vuonna " + vuosi + " ***** \n\n");
            Console.WriteLine("HUOM! Korvaushinta on euro kilometrilta\n");
            Console.ResetColor();


            for (int i = 0; i < kulkuneuvo.Length; i++)
            {
                Console.WriteLine(i + 1 + ". " + kulkuneuvo[i] + ": " + hinta[i]);
            }
            Console.WriteLine();
        }


        public void paivitaHinnasto()
        {
            Console.Write("Muutetaan korvaushinta, Kyllä (k) tai Ei (e): ");
            char muutaHinnasto = char.Parse(Console.ReadLine());
            Console.WriteLine();


            if (muutaHinnasto == 'k')
            {
                Console.Write("Minkä vuoden kilometrikorvaus hinnasto on? : ");
                vuosi = int.Parse(Console.ReadLine());

                Console.WriteLine();
                Console.Write("Syötä uudet hinnat kulkuneuvolle.\n\n");

                for (int i = 0; i < kulkuneuvo.Length; i++)
                {
                    Console.Write(i + 1 + ". " + kulkuneuvo[i] + " : ");
                    double uusiHinta = double.Parse(Console.ReadLine());
                    hinta[i] = uusiHinta;
                }

                JsonTiedostonHallinta.TallennaHinnasto(this);
            }

            Console.WriteLine("Paina mitä tahansa näppäintä palataksesi päävalikkoon");
            
        }
    }
}
