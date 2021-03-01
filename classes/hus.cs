namespace bostadskostnad
{
    class Hus 
    {
        private int driftkostnad;
        public int Driftkostnad {
            get { return driftkostnad; }
            set { driftkostnad = value; }
        }
        private decimal driftkostnadsökning;
        public decimal Driftkostnadsökning {
            get { return driftkostnadsökning; }
            set { driftkostnadsökning = value; }
        }
        private decimal aktieportfölj;
        public decimal Aktieportfölj {
            get { return aktieportfölj; }
            set { aktieportfölj = value; }
        }
        private int underhåll;
        public int Underhåll {
            get { return underhåll; }
            set { underhåll = value; }
        }
        private decimal underhållÖkning;
        public decimal UnderhållÖkning {
            get { return underhållÖkning; }
            set { underhållÖkning = value; }
        }
        private int inköpspris;
        public int Inköpspris {
            get { return inköpspris; }
            set { inköpspris = value; }
        }
        private int inköpskostnad;
        public int Inköpskostnad {
            get { return inköpskostnad; }
            set { inköpskostnad = value; }
        }
        private int lån;
        public int Lån {
            get { return lån; }
            set { lån = value; }
        }
        private decimal värde;
        public decimal Värde {
            get { return värde; }
            set { värde = value; }
        }
        private decimal ränta;
        public decimal Ränta {
            get { return ränta; }
            set { ränta = value; }
        }
        private int fastAmmortering;
        public int FastAmmortering {
            get { return fastAmmortering; }
            set { fastAmmortering = value; }
        }
        private bool ammorteraEnligtAmmorteringsreglerna;
        public bool AmmorteraEnligtAmmorteringsreglerna {
            get { return ammorteraEnligtAmmorteringsreglerna; }
            set { ammorteraEnligtAmmorteringsreglerna = value; }
        }
        private bool lågLönExtraAmmortering;
        public bool LågLönExtraAmmortering {
            get { return lågLönExtraAmmortering; }
            set { lågLönExtraAmmortering = value; }
        }
        public int Årskostnad() {
            if (ammorteraEnligtAmmorteringsreglerna) {
                decimal ammortering = 0;
                if (lån/värde > 0.7M) {
                    ammortering = lån*0.02M;
                } else if (lån/värde > 0.5M) {
                    ammortering = lån*0.01M;
                }
                if (lågLönExtraAmmortering) {
                    ammortering = ammortering + lån*0.01M;
                }
                return driftkostnad*12 + (int)(lån*ränta) + (int)ammortering + underhåll*12;
            }
            return driftkostnad*12 + (int)(lån*ränta) + fastAmmortering*12 + underhåll*12;
        }
        public void ÖkaKostnaden() {
            driftkostnad = (int)((decimal)driftkostnad*(1+driftkostnadsökning));
            underhåll = (int)((decimal)underhåll*(1+underhållÖkning));
        }
        public void MinskaLån() {
            if (ammorteraEnligtAmmorteringsreglerna) {
                decimal ammortering = 0;
                if (lån/värde > 0.7M) {
                    ammortering = lån*0.02M;
                } else if (lån/värde > 0.5M) {
                    ammortering = lån*0.01M;
                }
                if (lågLönExtraAmmortering) {
                    ammortering = ammortering + lån*0.01M;
                }
                lån = lån - (int)ammortering;
            } else {
                lån = lån - fastAmmortering*12;
            }
        }
    }
}