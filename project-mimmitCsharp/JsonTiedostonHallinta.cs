using System;
using System.IO;
using Newtonsoft.Json;

namespace project_mimmitCsharp
{
    class JsonTiedostonHallinta
    {
        static string projektiJuuri = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)).FullName).FullName).FullName).FullName;
        static string HenkiloJsonTiedostonOsoite = projektiJuuri + "/Henkilot.json";
        static string MaksuJsonTiedostonOsoite = projektiJuuri + "/Maksut.json";
        static string MaksetutJsonTiedostonOsoite = projektiJuuri + "/Maksetut.json";
        static string HinnastonOsoite = projektiJuuri + "/Hinnasto.json";

        public static void TallennaHenkilot(object[] henkilot)
        {
            string henkilotJson = JsonConvert.SerializeObject(henkilot);

            // tallenna tiedostoon
            using (StreamWriter sw = new StreamWriter(HenkiloJsonTiedostonOsoite, false))
            {
                sw.WriteLine(henkilotJson);
            }
            Console.WriteLine("Henkilö tallennettu.");
        }


        public static void TallennaMaksut(object[] maksut)
        {
            string maksutJson = JsonConvert.SerializeObject(maksut);

            // tallenna tiedostoon
            using (StreamWriter sw = new StreamWriter(MaksuJsonTiedostonOsoite, false))
            {
                sw.WriteLine(maksutJson);
            }
            Console.WriteLine("Maksu tallennettu.");
        }

        public static void TallennaMaksetut(object[] maksut)
        {
            string maksutJson = JsonConvert.SerializeObject(maksut);

            // tallenna tiedostoon
            using (StreamWriter sw = new StreamWriter(MaksetutJsonTiedostonOsoite, true))
            {
                sw.WriteLine(maksutJson);
            }
            Console.WriteLine("Maksettu lasku tallennettu.");
        }


        public static void TallennaHinnasto(KorvausHinnasto korvausHinnasto)
        {
            string hintaJson = JsonConvert.SerializeObject(korvausHinnasto);

            // tallenna tiedostoon
            using (StreamWriter sw = new StreamWriter(HinnastonOsoite, false))
            {
                sw.WriteLine(hintaJson);
            }
            Console.WriteLine("Hinnasto tallennettu.");
        }


        public static Henkilo[] LataaHenkilot()
        {
            string olemassaOlevatHenkilot = "";

            try
            {
                using (StreamReader sr = new StreamReader(HenkiloJsonTiedostonOsoite))
                {
                    olemassaOlevatHenkilot = sr.ReadToEnd();
                }

                Henkilo[] henkilot = JsonConvert.DeserializeObject<Henkilo[]>(olemassaOlevatHenkilot);

                return henkilot;
            }

            catch(Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("Tiedostoa ei voi avata.");
                Console.WriteLine(e.Message);

                return new Henkilo[0];

            }

        }


        public static yksittäinenMaksu[] LataaMaksut()
        {
            string olemassaOlevatMaksut = "";

            try
            {
                using (StreamReader sr = new StreamReader(MaksuJsonTiedostonOsoite))
                {
                    olemassaOlevatMaksut = sr.ReadToEnd();
                }

                yksittäinenMaksu[] maksut = JsonConvert.DeserializeObject<yksittäinenMaksu[]>(olemassaOlevatMaksut);

                return maksut;
            }

            catch (Exception e)
            {
                // virheilmoitus
                Console.WriteLine("Tiedostoa ei voi lukea.");
                Console.WriteLine(e.Message);

                return new yksittäinenMaksu[0];
            }
        }


        // lataa hinnasto km-korvaukset vero.fi sivulta
        public static KorvausHinnasto LataaHinnasto()
        {
            string olemassaOlevaHinnasto = "";

            try
            {
                using (StreamReader sr = new StreamReader(HinnastonOsoite))
                {
                    olemassaOlevaHinnasto = sr.ReadToEnd();
                }

                KorvausHinnasto hinnat = JsonConvert.DeserializeObject<KorvausHinnasto>(olemassaOlevaHinnasto);

                return hinnat;
            }

            catch (Exception e)
            {
                // virheilmoitus
                Console.WriteLine("Tiedostoa ei voi avata.");
                Console.WriteLine(e.Message);

                KorvausHinnasto korvausHinnasto = new KorvausHinnasto();
                return korvausHinnasto;
            }
        }
    }
}
