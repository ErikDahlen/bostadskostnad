namespace bostadskostnad
{
    class Bostadsrätt 
    {
        private int avgift;
        public int Avgift {
            get { return avgift; }
            set { avgift = value; }
        }
        private decimal avgiftsökning;
        public decimal Avgiftsökning {
            get { return avgiftsökning; }
            set { avgiftsökning = value; }
        }
        private int inköpspris;
        public int Inköpspris {
            get { return inköpspris; }
            set { inköpspris = value; }
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
        private decimal aktieportfölj;
        public decimal Aktieportfölj {
            get { return aktieportfölj; }
            set { aktieportfölj = value; }
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
                return avgift*12 + (int)(lån*ränta) + (int)ammortering + underhåll*12;
            }
            return avgift*12 + (int)(lån*ränta) + fastAmmortering*12 + underhåll*12;
        }
        public void ÖkaKostnaden() {
            avgift = (int)((decimal)avgift*(1+avgiftsökning));
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