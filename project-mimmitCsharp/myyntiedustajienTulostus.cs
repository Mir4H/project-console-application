using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_mimmitCsharp
{
    class myyntiedustajienTulostus
    {
        public static void HenkiloRekisterissa(ArrayList henkilot)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\nHenkilöt rekisterissä\n");
            Console.ResetColor();

            Console.WriteLine("ID numero\tHenkilö");
            foreach (Henkilo henkilo in henkilot)
            {
                Console.WriteLine("  " + henkilo.Id + "\t\t" + henkilo.Nimi);
            }

            Console.WriteLine();
        }

        public static void HenkiloidenTiedot(ArrayList henkilot)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\nHenkilöt rekisterissä\n");
            Console.ResetColor();

            Console.WriteLine("ID numero\tHenkilö\tSähköposti");
            foreach (Henkilo henkilo in henkilot)
            {
                Console.WriteLine("  " + henkilo.Id + "\t\t" + henkilo.Nimi + "\t\t" + henkilo.Sposti);
            }

            Console.WriteLine();
        }
    }
}
