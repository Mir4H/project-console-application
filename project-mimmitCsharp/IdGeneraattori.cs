using System;
namespace project_mimmitCsharp
{
    class IDGeneraattori
    {
        private int viimeinenID;


        public void AsetaViimeinenId(int viimeisinId)
        {
            viimeinenID = viimeisinId;
        }


        public IDGeneraattori(int aViimeinenID)
        {
            viimeinenID = aViimeinenID;
        }


        public int HaeSeuraavaID()
        {
            int seuraavaID = viimeinenID + 1;
            viimeinenID = seuraavaID;

            return seuraavaID;
        }
    }
}
