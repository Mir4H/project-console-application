using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_mimmitCsharp
{
    public class yksittäinenMaksu
    {
        public yksittäinenMaksu(int IDmatka, int IDmyyntiedustaja, string Aloituspv, string Lopetuspäivä, string Matkankesto,
            string Matkantarkoitus, string lahto, string Kohde, double menoKM, string paluu, double paluuKM, double Kustannus, double KustannusPv)
        {
            maksettu = false;
            maksuAika = new DateTime();
            matkanId = IDmatka;
            myyntiedustajanId = IDmyyntiedustaja;
            matkanAloitusPäivä = Aloituspv;
            matkanLopetusPäivä = Lopetuspäivä;
            matkaKestoTunteina = Matkankesto;
            matkanTarkoitus = Matkantarkoitus;
            lähtöPaikka = lahto;
            kohde = Kohde;
            menoKilometrit = menoKM;
            paluuPaikka = paluu;
            paluuKilometrit = paluuKM;
            kmKustannus = Kustannus;
            pvRahaKustannus = KustannusPv;
        }

        public bool maksettu { get; set; }
        public DateTime maksuAika { get; set; }
        public int matkanId { get; set; }
        public int myyntiedustajanId { get; set; }
        public string matkanAloitusPäivä { get; set; }
        public string matkanLopetusPäivä { get; set; }
        public string matkaKestoTunteina { get; set; }
        public string matkanTarkoitus { get; set; }
        public string lähtöPaikka { get; set; }
        public string kohde { get; set; }
        public double menoKilometrit { get; set; }
        public string paluuPaikka { get; set; }
        public double paluuKilometrit { get; set; }

        public double kmKustannus { get; set; }
        public double pvRahaKustannus { get; set; }



        override public string ToString()
        {
            return "Matkan ID: " + matkanId + " Myyntiedustajan ID: " + myyntiedustajanId + " Matkan tarkoitus: " + matkanTarkoitus + "\n KM-kustannus: " + kmKustannus + "€" + " Päivärahakustannus: " + pvRahaKustannus + "€";
        }




    }

}
