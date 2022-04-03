using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_mimmitCsharp
{
    class kysymykset
    {
        varmistaja Varmistaja = new varmistaja();

        const string VIRHE_PAIVAMAARA_MUOTO = "Antamasi arvot eivät kelpaa. Anna päivämäärä ja aika muodossa pv.kk.vvvv hh.mm.";
        const string valiviivaa = "=================================";

        public void päivämäärät(varmistaja Varmistaja, ref DateTime saatuAloituspäivä, ref DateTime saatuLopetuspäivä)
        {
            bool vääräPvmäärä = true;
            while (vääräPvmäärä)
            {
                string muoto = "dd.MM.yyyy HH.mm";
                bool pvmOK = false;
                Console.WriteLine(valiviivaa);
                Console.WriteLine("Anna matkan aloituspäivämäärä ja -aika muodossa pv.kk.vvvv hh.mm.");
                while (!DateTime.TryParseExact(Console.ReadLine(), muoto, null, DateTimeStyles.None, out saatuAloituspäivä))
                    Console.WriteLine(VIRHE_PAIVAMAARA_MUOTO);

                while (!pvmOK)
                {
                    Console.WriteLine("Anna matkan lopetuspäivämäärä ja -aika muodossa pv.kk.vvvv hh.mm.");
                    while (!DateTime.TryParseExact(Console.ReadLine(), muoto, null, DateTimeStyles.None, out saatuLopetuspäivä))
                        Console.WriteLine(VIRHE_PAIVAMAARA_MUOTO);
                    if (saatuAloituspäivä < saatuLopetuspäivä)
                    {
                        pvmOK = true;
                    }
                    else
                    {
                        pvmOK = false;
                        Console.WriteLine("Lopetuspäivämäärä ei voi olla ennen aloituspäivämäärää. Syötä lopetuspäivämäärä uudelleen:");
                    }
                }
                Console.Clear();
                Console.WriteLine(valiviivaa);
                Console.WriteLine("Antamasi päivämäärät ja ajat ovat:");
                Console.WriteLine(saatuAloituspäivä.ToString("dddd, dd MMMM yyyy klo. HH:mm") + " - " + saatuLopetuspäivä.ToString("dddd, dd MMMM yyyy klo. HH:mm"));
                Console.WriteLine(valiviivaa);
                vääräPvmäärä = Varmistaja.varmistus(vääräPvmäärä);


            }

        }



        public TimeSpan matkanKesto(DateTime saatuAloituspäivä, DateTime saatuLopetuspäivä)
        {
            Console.Clear();
            TimeSpan matkanKesto = saatuLopetuspäivä.Subtract(saatuAloituspäivä);
            Console.WriteLine(valiviivaa);
            Console.WriteLine("Matkan kesto on: " + matkanKesto.Days + " päivää sekä " + matkanKesto.Hours + " tuntia ja " + matkanKesto.Minutes + " minuuttia.");

            if (matkanKesto.TotalMinutes <= 360)
            {
                Console.WriteLine("Matkasta ei makseta päivärahaa.");
                Console.WriteLine(valiviivaa);
            }
            else if (matkanKesto.TotalMinutes > 360 && matkanKesto.TotalMinutes < 600)
            {
                Console.WriteLine("Matkasta maksetaan osapäivärahaa.");
                Console.WriteLine(valiviivaa); ;
            }
            else
            {
                Console.WriteLine("Matkasta maksetaan kokopäivärahaa.");
                Console.WriteLine(valiviivaa);
            }

            return matkanKesto;
        }

        // Päivärahan laskeminen (v.2021): 
        // Jos työmatka yli 6h = 20€
        // Jos työmatka yli 10h = 44€
        // Kun matkaan käytetty aika ylittää viimeisen täyden matkavuorokauden vähintään kahdella tunnilla päivärahaa maksetaan 20€ lisää
        // tai jos ylittyy 6h maksetaan lisää 44€
        public double LaskePvRaha(DateTime saatuAloituspäivä, DateTime saatuLopetuspäivä)
        {
            TimeSpan matkanKesto = saatuLopetuspäivä.Subtract(saatuAloituspäivä);
            double pvRahaKustannus;
            double puoliPvRaha = 20;
            double kokoPvRaha = 44;

            // Laskee yli kuuden tunnin, mutta max 10 tunnin päivärahan (= puolipäivä 20€)
            if (matkanKesto.TotalHours > 6 && matkanKesto.TotalHours <= 10)
            {
                pvRahaKustannus = puoliPvRaha;
                return pvRahaKustannus;
            }

            //Laskee yli 10 tunnin, mutta alle 24+2 tunnin päivärahan (= kokopäiväraha 44€)
            else if (matkanKesto.TotalHours > 10 && matkanKesto.TotalHours < 26)
            {
                pvRahaKustannus = kokoPvRaha;
                return pvRahaKustannus;
            }

            else
            {
                // Laskee yli 24 tunnin matkan päivärahan, kun matkaan käytetty aika ylittää viimeisen täyden matkavuorokauden vähintään kahdella tunnilla, mutta maksimissaan
                // 6 tunnilla. Silloin päivärahaksi siis lasketaan x-määrä kokopäivärahaa ja yksi puolipäiväraha joka tulee lyhyeltä viimeiseltä max kuuden tunnin päivältä.
                while (matkanKesto.TotalDays >= 1 && 2 <= (matkanKesto.TotalHours - (matkanKesto.Days * 24)) && 6 >= (matkanKesto.TotalHours - (matkanKesto.Days * 24)))
                {
                    pvRahaKustannus = kokoPvRaha * matkanKesto.Days + puoliPvRaha;
                    return pvRahaKustannus;
                }

                // Laskee päivärahan jos kokopäivärahaa maksetaan vähintään kahdelta päivältä ja viimeinen täysi matkavuorokausi jää alle 2 tunnin.
                while (matkanKesto.TotalHours > 30 && (matkanKesto.TotalHours - (matkanKesto.Days * 24)) < 2 && (matkanKesto.TotalHours - (matkanKesto.Days * 24)) >= 0)
                {
                    pvRahaKustannus = kokoPvRaha * matkanKesto.Days;
                    return pvRahaKustannus;
                }

                // Laskee päivärahan jos kokopäivärahaa maksetaan vähintään kahdelta päivältä ja viimeinen täysi matkavuorokausi on yli 6 tuntia.
                pvRahaKustannus = kokoPvRaha * matkanKesto.Days + kokoPvRaha;
                return pvRahaKustannus;
            }
        }

        public string Tarkoitus()
        {
            string matkanTarkoitus = "";
            bool vääräTarkoitus = true;
            while (vääräTarkoitus)
            {

                Console.WriteLine("Kirjoita matkan tarkoitus:");
                while (string.IsNullOrWhiteSpace(matkanTarkoitus = Console.ReadLine()))
                    Console.WriteLine("Kirjoita matkan tarkoitus:");
                Console.Clear();
                Console.WriteLine(valiviivaa);
                Console.WriteLine($"Matkan tarkoitus on {matkanTarkoitus}");
                Console.WriteLine(valiviivaa);
                vääräTarkoitus = Varmistaja.varmistus(vääräTarkoitus);


            }
            return matkanTarkoitus;
        }

        public static int Idvalinta()
        {
            int valittuMyyntiedustaja;
            Console.WriteLine("\nValitse matkan tehnyt myyntiedustaja ID-numeron perusteella: ");
            while (!int.TryParse(Console.ReadLine(), out valittuMyyntiedustaja))
                Console.WriteLine("Myyntiedustajan ID: ");
            return valittuMyyntiedustaja;
        }




        public double kmPaluu()
        {
            double paluuKilometrit;
            Console.WriteLine("Paluumatkan kilometrimäärä:");
            while (!double.TryParse(Console.ReadLine(), out paluuKilometrit))
                Console.WriteLine("Anna paluumatkan kilometrimäärä:");
            return paluuKilometrit;
        }

        public string Paluu()
        {
            string paluuPaikka;
            Console.WriteLine(valiviivaa);
            Console.WriteLine("Anna paluumatkan tiedot.");
            Console.WriteLine(valiviivaa);
            Console.WriteLine("Paluupaikka:");
            while (string.IsNullOrWhiteSpace(paluuPaikka = Console.ReadLine()))
                Console.WriteLine("Paluupaikka:");
            return paluuPaikka;
        }

        public double kmMeno()
        {
            double menoKilometrit;
            Console.WriteLine("Menomatkan kilometrimäärä:");
            while (!double.TryParse(Console.ReadLine(), out menoKilometrit))
                Console.WriteLine("Anna menomatkan kilometrimäärä:");
            return menoKilometrit;
        }

        public string Kohde()
        {
            string kohde;
            Console.WriteLine("Kohde:");
            while (string.IsNullOrWhiteSpace(kohde = Console.ReadLine()))
                Console.WriteLine("Kohde:");
            return kohde;
        }

        public string Lähtö()
        {
            string lähtöPaikka;
            Console.Clear();
            Console.WriteLine(valiviivaa);
            Console.WriteLine("Anna menomatkan tiedot.");
            Console.WriteLine(valiviivaa);
            Console.WriteLine("Lähtöpaikka:");
            while (string.IsNullOrWhiteSpace(lähtöPaikka = Console.ReadLine()))
                Console.WriteLine("Lähtöpaikka:");
            return lähtöPaikka;
        }

        public double LaskeKm(double menoKilometrit, double paluuKilometrit)
        {
            KorvausHinnasto korvausHinnasto = new KorvausHinnasto();
            double kmKustannus;
            kmKustannus = (menoKilometrit + paluuKilometrit) * korvausHinnasto.hinta[0];
            return kmKustannus;
        }

        public void TulostaSyötetytTiedot(string lähtöPaikka, string kohde, double menoKilometrit, string paluuPaikka, double paluuKilometrit, double kmKustannus)
        {
            Console.WriteLine(valiviivaa);
            Console.WriteLine($"Menomatka: {lähtöPaikka} - {kohde}, {menoKilometrit}km.");
            Console.WriteLine($"Menomatka: {kohde} - {paluuPaikka}, {paluuKilometrit}km.");
            Console.WriteLine($"Kilometrit yhteensä: {menoKilometrit + paluuKilometrit}km.");
            Console.WriteLine($"Kilometrikorvaus (henkilöauto, peruskorvaus): {kmKustannus.ToString("0.##")}€.");
            Console.WriteLine(valiviivaa);
        }
        public void TulostaSaadutTiedot(int valittuMyyntiedustaja, DateTime saatuAloituspäivä, DateTime saatuLopetuspäivä, string lähtöPaikka, string kohde, double menoKilometrit, string paluuPaikka, double paluuKilometrit, double kmKustannus, double pvRahaKustannus)
        {
            Console.WriteLine(valiviivaa);
            Console.WriteLine("Syöttämäsi matkan tiedot ovat:");
            Console.WriteLine(valiviivaa);
            Console.WriteLine($"\n Myyntiedustaja: {valittuMyyntiedustaja}\n\n MENOMATKA \n {saatuAloituspäivä.ToString("dddd, dd MMMM yyyy klo. HH:mm")} \n {lähtöPaikka} - {kohde}, pituus: {menoKilometrit}km. " +
                $"\n\n PALUUMATKA \n {saatuLopetuspäivä.ToString("dddd, dd MMMM yyyy klo. HH:mm")} \n {kohde} - {paluuPaikka}, pituus: {paluuKilometrit}km.\n\n KORVAUKSET \n Kilometrikorvaus (henkilöauto, peruskorvaus): {kmKustannus.ToString("0.##")}€\n Päivärahan määrä: {pvRahaKustannus.ToString("0.##")}€");
            Console.WriteLine();
            Console.WriteLine(valiviivaa);
        }
    }
}
