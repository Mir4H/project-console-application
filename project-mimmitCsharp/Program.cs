using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace project_mimmitCsharp
{
    class Program
    {        
        static ArrayList henkilot = new ArrayList();
        static IDGeneraattori henkiloIdGeneraattori = new IDGeneraattori(0);
        static KorvausHinnasto korvausHinnasto = new KorvausHinnasto();
        

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            PaaOhjelma(args);
        }


        static void PaaOhjelma(string[] args)
        {
            // Lataa olemassa olevat henkilot 
            Henkilo[] luodutHenkilot = JsonTiedostonHallinta.LataaHenkilot();
            henkilot.AddRange(luodutHenkilot);

            // Lataa olemassa olevat maksut
            maksunTiedot.LataaOlemassaOlevatMaksut();

            // Lataa olemassa oleva hinnasto
            korvausHinnasto = JsonTiedostonHallinta.LataaHinnasto();

            // Tarkista mikä viimeinen henkiloID oli käytössä
            if (luodutHenkilot.Length > 0)
            {
                henkiloIdGeneraattori.AsetaViimeinenId(((Henkilo)henkilot[henkilot.Count - 1]).Id);
            }


            const int lopetus = 6;
            int valinta = 0;
            bool suorita = false;
           

            // Näytetään päävalikko ja pyydetään käyttäjältä inputtina numero. While-looppi on päällä ja päävalikko näytetään siihen asti kunnes
            // käyttäjä valitsee numeron 6 eli lopetuksen.
            while (valinta != lopetus)
            {
                Console.Clear();
                PaaValikko();
                suorita = int.TryParse(Console.ReadLine(), out valinta);

                // If-looppi on päällä, jos käyttäjä valitsee numeron
                if (suorita)
                {
                    // Switch-looppi käy läpi caset 1-5 ja default tapahtuu jos nro on joku muu kuin 6
                    switch (valinta)
                    {
                        case 1:
                            Henkilo myyja = LuoHenkilo();
                            break;

                        case 2:
                            //Tähän "Syötä matka"

                            // tulosta olemassa olevat myyjät kun käyttäjä valitsee 2. Syötä matka
                            maksunTiedot maksu = new maksunTiedot();
                            maksu.kysyTiedot();
                            break;

                        case 3:
                            // Tähän "Muuta kustannustiedot"
                            korvausHinnasto.naytaHinnasto();
                            korvausHinnasto.paivitaHinnasto();
                            break;

                        case 4:
                            // Tähän "Kuittaa matka maksetuksi"
                            
                            maksamattomatLaskut maksuun = new maksamattomatLaskut();
                            maksuun.Maksaminen(); //Kysyyy maksettavan IDn ja vahvistuksen maksamiselle
                           
                            break;

                        case 5:
                            //Tähän "Tulosta raportti"
                            Raportti raportti = new Raportti();
                            myyntiedustajienTulostus.HenkiloRekisterissa(henkilot);
                            raportti.raportinTulostus();
                            break;

                        default:
                            if (valinta != lopetus)
                            {
                                // Näyttää viheilmoituksen, päävalikko palautuu ja käyttäjä voi valita uuden numeron
                                VirheIlmoitus();
                            }
                            break;
                    }
                }
                // Jos käyttäjä näppäilee jonkin muun kuin numeron, pyörähtää else-looppi päälle. 
                // Näyttää viheilmoituksen, päävalikko palautuu ja käyttäjä voi valita uuden numeron
                else
                {
                    VirheIlmoitus();
                }
                Console.ReadKey();
            }

        }


        public static void PaaValikko()
        {
            Console.WriteLine("Myyntiedustajien matkakulut");
            Console.WriteLine();
            Console.WriteLine("Valitse seuraavista:\n[1] Lisää myyntiedustaja\n[2] Syötä matka\n" +
                "[3] Muuta kustannustiedot\n[4] Kuittaa matka maksetuksi\n[5] Tulosta raportti\n[6] Lopeta");
        }


        private static void VirheIlmoitus()
        {
            Console.WriteLine("Näppäilyvirhe, paina mitä tahansa näppäintä.");
        }


        static Henkilo LuoHenkilo()
        {
            // 1a) Myyntiedustajan nimi
            Console.Write("Anna työntekijän etu- ja sukunimi: ");
            string henkiloNimi = Console.ReadLine();

            // 1c. Kysy myyntiedustajan sposti & tarkista spostin muoto
            string henkiloSposti = Henkilo.SpostiTarkastus();

            // 1b. Generoi ID numero henkilölle
            int henkiloId = henkiloIdGeneraattori.HaeSeuraavaID();
            Console.WriteLine("Työntekijän ID numero: " + henkiloId);

            Henkilo henkilo = new Henkilo(henkiloNimi, henkiloSposti, henkiloId);

            // tallenna henkilo henkilot-tiedostoon
            henkilot.Add(henkilo);
            JsonTiedostonHallinta.TallennaHenkilot(henkilot.ToArray());

            return henkilo;

        }


        
    }
}
