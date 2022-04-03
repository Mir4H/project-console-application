using System;

namespace project_mimmitCsharp
{
    class Henkilo
    {
        public Henkilo(string aNimi, string aSposti, int aIdNumber)
        {
            Nimi = aNimi;
            Sposti = aSposti;
            Id = aIdNumber;
        }

        public string Nimi { get; set; }
        
        public string Sposti { get; set; }

        public int Id { get; set; }
        

        override public string ToString() 
        {
            return Nimi + " " + Sposti + " " + Id;
        }


        public static string SpostiTarkastus()
        {
            string myyjanSposti = "";
            bool onOkSposti = false;

            while (!onOkSposti)
            {
                Console.Write("Anna työntekijän sposti: ");
                myyjanSposti = Console.ReadLine();

                if (myyjanSposti.Contains("@"))
                {
                    onOkSposti = true;
                }
                else
                {
                    onOkSposti = false;
                    Console.WriteLine("Syötämäsi sposti ei ole oikeassa muodossa!");
                }
            }
            return myyjanSposti;
        }
    }
}
