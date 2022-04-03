using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace project_mimmitCsharp
{
    public class Raportti
    {
        const int paavalikkoon = 4;
        int valinta = 0;
        bool suorita = false;

        public void raportinTulostus()
        {
            Console.Write("Valitse sen henkilön ID-numero, jonka matkoista haluat raportin: ");
            int henkiloId = int.Parse(Console.ReadLine());
            const string takaisin = "\nPaina mitä tahansa näppäintä palataksesi Raportti-menuun.";

            while (valinta !=paavalikkoon)
            {
                Console.Clear();
                RaporttiMenu();
                suorita = int.TryParse(Console.ReadLine(), out valinta);
                List<double> maksutYhteensä = new List<double>();

                if (suorita)
                {
                    switch (valinta)
                    {
                        case 1:
                            // Maksetut
                            Console.WriteLine("\nID:n " + henkiloId + " matkat, joita ei ole vielä maksettu: \n");
                            List<yksittäinenMaksu> avoimet = maksunTiedot.HaeAvoimetLaskut(); //Etsii avoimet maksut
                            foreach (var lasku in avoimet.FindAll(x => x.myyntiedustajanId == henkiloId))//suodattaa valitun ID:n mukaisesti
                            {
                                Console.WriteLine($"Maksun ID: {lasku.matkanId}\tkilometrikorvaus: {lasku.kmKustannus}€, päivärahakustannus: {lasku.pvRahaKustannus}€");
                                maksutYhteensä.Add(lasku.pvRahaKustannus);
                                maksutYhteensä.Add(lasku.kmKustannus);
                            }
                            Console.WriteLine($"\nMaksamattomien korvausten määrä myyntiedustajalle {henkiloId} yhteensä: {maksutYhteensä.Sum().ToString("0.##")}€");
                            maksutYhteensä.Clear();
                            Console.WriteLine(takaisin);

                            break;

                        case 2:
                            //Maksamattomat
                            Console.WriteLine("\nID:n " + henkiloId + " matkat, jotka on jo maksettu: \n");
                            List<yksittäinenMaksu> maksetut = maksunTiedot.HaeMaksetutLaskut(); // Etsii maksetut laskut                            
                            foreach (var lasku in maksetut.FindAll(y => y.myyntiedustajanId == henkiloId))//suodattaa valitun ID:n mukaisesti
                            {
                                Console.WriteLine($"MaksunID: {lasku.matkanId}\tkilometrikorvaus: {lasku.kmKustannus}€, päivärahakustannus: {lasku.pvRahaKustannus}€, maksettu: {lasku.maksuAika.ToString("dd MMMM yyyy klo. HH:mm")}\n");
                                maksutYhteensä.Add(lasku.pvRahaKustannus);
                                maksutYhteensä.Add(lasku.kmKustannus);
                            }
                            
                            Console.WriteLine($"\nMaksettujen korvausten määrä myyntiedustajalle {henkiloId} yhteensä: {maksutYhteensä.Sum().ToString("0.##")}€");
                            maksutYhteensä.Clear();
                            Console.WriteLine(takaisin);

                            break;

                        case 3:
                            //Maksetut + maksamattomat tietoineen
                            Console.WriteLine("\nID:n " + henkiloId + " matkat, joita ei ole vielä maksettu: \n");
                            List<yksittäinenMaksu> _avoimet = maksunTiedot.HaeAvoimetLaskut();
                            foreach (var lasku in _avoimet.FindAll(x => x.myyntiedustajanId == henkiloId))
                                Console.WriteLine($"Maksun ID: {lasku.matkanId}\tlähtöaika: {lasku.matkanAloitusPäivä}, lähtöpaikka: {lasku.lähtöPaikka}, kohde: {lasku.kohde}, menomatkan kilometrit: {lasku.menoKilometrit},  paluuaika: {lasku.matkanLopetusPäivä}, paluupaikka: {lasku.paluuPaikka}, paluumatkan kilometrit: {lasku.paluuKilometrit}, matkan tarkoitus: {lasku.matkanTarkoitus} kilometrikorvaus: {lasku.kmKustannus}€, päivärahakustannus: {lasku.pvRahaKustannus}€\n");

                            Console.WriteLine("\n=============================");


                            Console.WriteLine("\nID:n " + henkiloId + " matkat, jotka on jo maksettu: \n");
                            List<yksittäinenMaksu> _maksetut = maksunTiedot.HaeMaksetutLaskut();
                            foreach (var lasku in _maksetut.FindAll(y => y.myyntiedustajanId == henkiloId))
                                Console.WriteLine($"Maksun ID: {lasku.matkanId}\tlähtöaika: {lasku.matkanAloitusPäivä}, lähtöpaikka: {lasku.lähtöPaikka}, kohde: {lasku.kohde}, menomatkan kilometrit: {lasku.menoKilometrit},  paluuaika: {lasku.matkanLopetusPäivä}, paluupaikka: {lasku.paluuPaikka}, paluumatkan kilometrit: {lasku.paluuKilometrit}, matkan tarkoitus: {lasku.matkanTarkoitus} kilometrikorvaus: {lasku.kmKustannus}, päivärahakustannus: {lasku.pvRahaKustannus}€, maksettu päivämääränä: {lasku.maksuAika}€\n");

                            Console.WriteLine(takaisin);

                            break;


                        default:
                            if (valinta != paavalikkoon)
                            {
                                VirheIlmoitus();
                            }
                            Console.WriteLine("Jatka päävalikkoon painamalla mitä tahansa näppäintä.");
                            break;


                    }
                }
                else
                {
                    VirheIlmoitus();
                }
                Console.ReadKey();



            }
        }

        public static void RaporttiMenu()
        {
            Console.WriteLine("Raportti-menu\n");
            Console.WriteLine("Valitse:\n");
            Console.WriteLine("[1] Myyntiedustajan maksamattomat matkat\n[2] Myyntiedustajan maksetut matkat\n[3] Myyntiedustajan kaikki matkat ja matkojen tiedot\n[4] Palaa päävalikkoon");
        }

        private static void VirheIlmoitus()
        {
            Console.WriteLine("Näppäilyvirhe, paina mitä tahansa näppäintä.");
        }
    }
}
