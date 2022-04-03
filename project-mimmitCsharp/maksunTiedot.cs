using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;


namespace project_mimmitCsharp
{
    class maksunTiedot
    {
        static ArrayList MaksutList = new ArrayList();
        static IDGeneraattori maksuIdGeneraattori = new IDGeneraattori(0);

        public static void LataaOlemassaOlevatMaksut()
        {
            // Lataa olemassa olevat maksut
            yksittäinenMaksu[] luodutMaksut = JsonTiedostonHallinta.LataaMaksut();
            MaksutList.AddRange(luodutMaksut);

            // Tarkista mikä viimeinen maksuID oli käytössä
            if (luodutMaksut.Length > 0)
            {
                maksuIdGeneraattori.AsetaViimeinenId(((yksittäinenMaksu)MaksutList[MaksutList.Count - 1]).matkanId);
            }
        }


    public void kysyTiedot()
        {
            varmistaja Varmistaja = new varmistaja(); //Rivit 32-42 = "alustetaan" variables
            kysymykset Kysymykset = new kysymykset();
            ArrayList henkilot = new ArrayList();

            DateTime saatuAloituspäivä = new DateTime(); ;
            DateTime saatuLopetuspäivä = new DateTime(); ;

            string lähtöPaikka = "";
            string kohde = "";
            double menoKilometrit = 0;
            string paluuPaikka = "";
            double paluuKilometrit = 0;
            double kmKustannus = 0;
            double pvRahaKustannus = 0;
            bool jatketaan = true;

            Henkilo[] luodutHenkilot = JsonTiedostonHallinta.LataaHenkilot();
            henkilot.AddRange(luodutHenkilot);
            Console.Clear();
            myyntiedustajienTulostus.HenkiloRekisterissa(henkilot);

            int valittuMyyntiedustaja = kysymykset.Idvalinta();
            jatketaan = Varmistaja.myyntiedustajanVahvistus(ref jatketaan, valittuMyyntiedustaja);
            Console.Clear();

            if (jatketaan)
            {
                Kysymykset.päivämäärät(Varmistaja, ref saatuAloituspäivä, ref saatuLopetuspäivä); //käynnistää kysymykset classissa olevan päivämäärät metodin, josta saadaan variables saatualoituspäivä ja saatulopetuspäivä
                TimeSpan matkanKesto = Kysymykset.matkanKesto(saatuAloituspäivä, saatuLopetuspäivä); //variable matkanKesto arvoksi otetaan kysymykset classin matkankesto metodin return arvo
                string matkanTarkoitus = Kysymykset.Tarkoitus(); //variable matkanTarkoitus on kysymykset classin Tarkoitus metodin return arvo
                bool väärätPaikat = true;
                while (väärätPaikat) //kun bool väärät paikat on true tämä while loop pyörii
                {
                    lähtöPaikka = Kysymykset.Lähtö(); //lähtöPaikka arvoksi Lähtö metodin return arvo (nämä kaikki metodit on kysymykset classissa)                
                    kohde = Kysymykset.Kohde(); //kohde arvoksi Kohde metodin return arvo
                    menoKilometrit = Kysymykset.kmMeno(); //menoKilometrit arvoksi kmMeno metodin return arvo
                    paluuPaikka = Kysymykset.Paluu(); //paluuPaikka arvoksi Paluu metodin return arvo
                    paluuKilometrit = Kysymykset.kmPaluu(); //paluuKilometrit arvoksi kmPaluu metodin return arvo
                    kmKustannus = Kysymykset.LaskeKm(menoKilometrit, paluuKilometrit); //kmKustannus arvoksi LaskeKm metodin return arvo
                    pvRahaKustannus = Kysymykset.LaskePvRaha(saatuAloituspäivä, saatuLopetuspäivä);
                    Console.Clear();
                    Kysymykset.TulostaSyötetytTiedot(lähtöPaikka, kohde, menoKilometrit, paluuPaikka, paluuKilometrit, kmKustannus); // tulostaa kysymykset classin metodin TulostaSyötetytTiedot sisällön
                    väärätPaikat = Varmistaja.varmistus(väärätPaikat); //varmistaja classin varmistus antaa arvon väärätPaikat variablelle - jos true, while loop käynnistyy uudelleen jos false, jatketaan eteenpäin. 
                }
                Console.Clear();
                Kysymykset.TulostaSaadutTiedot(valittuMyyntiedustaja, saatuAloituspäivä, saatuLopetuspäivä, lähtöPaikka, kohde, menoKilometrit, paluuPaikka, paluuKilometrit, kmKustannus, pvRahaKustannus);
                tietojenTallennus(saatuAloituspäivä, saatuLopetuspäivä, lähtöPaikka, kohde, menoKilometrit, paluuPaikka, paluuKilometrit, kmKustannus, pvRahaKustannus, valittuMyyntiedustaja, matkanKesto, matkanTarkoitus);

            }

        }

        public void tietojenTallennus(DateTime saatuAloituspäivä, DateTime saatuLopetuspäivä, string lähtöPaikka, string kohde, double menoKilometrit, string paluuPaikka, double paluuKilometrit, double kmKustannus, double pvRahaKustannus, int valittuMyyntiedustaja, TimeSpan matkanKesto, string matkanTarkoitus)
        {
            Console.WriteLine("Haluatko tallentaa tiedot, aloittaa alusta vai lopettaa? \nAnna: T = Tallenna tai A = Alusta tai L = Lopeta."); //kysytään user inputtia
            var vastaus = Console.ReadKey().KeyChar.ToString().ToLower();
            Console.WriteLine();

            while (vastaus != "t" && vastaus != "a" && vastaus != "l") //jos input muuta kuin kysyttyä, kysy uudelleen
            {
                Console.WriteLine("Anna: T tai A tai L");
                vastaus = Console.ReadLine();
            }

            switch (vastaus) //vastauksen perusteella toteutetaan yksi seuraavista
            {
                case "t":
                    // 2o. Generoi ID numero laskulle/maksulle
                    int maksuId = maksuIdGeneraattori.HaeSeuraavaID();
                    Console.WriteLine("Maksun/Laskun ID numero: " + maksuId);


                    yksittäinenMaksu maksu = new yksittäinenMaksu(maksuId, valittuMyyntiedustaja, saatuAloituspäivä.ToString(), saatuLopetuspäivä.ToString(), matkanKesto.TotalHours.ToString(), matkanTarkoitus, lähtöPaikka, kohde, menoKilometrit, paluuPaikka, paluuKilometrit, kmKustannus, pvRahaKustannus); //luo yksittäiselle maksulle annetut tiedot class maksuun

                    // Tallenna maksu Maksut.json tiedostoon
                    MaksutList.Add(maksu);
                    JsonTiedostonHallinta.TallennaMaksut(MaksutList.ToArray());

                    Console.WriteLine($"Paina mitä tahansa näppäintä palataksesi päävalikkoon.");

                    break;

                case "a":
                    kysyTiedot(); //aloita kyselyrimpsu alusta
                    break;

                case "l":

                    break;

            }
        }







        // Hae avoimet laskut
        public static List<yksittäinenMaksu> HaeAvoimetLaskut()
        {
            return MaksutList.Cast<yksittäinenMaksu>().ToList().Where(yksittäinenMaksu => !yksittäinenMaksu.maksettu).ToList();
        }


        // Hae maksetut laskut
        public static List<yksittäinenMaksu> HaeMaksetutLaskut()
        {
            return MaksutList.Cast<yksittäinenMaksu>().ToList().Where(yksittäinenMaksu => yksittäinenMaksu.maksettu).ToList();
        }

    }

}
