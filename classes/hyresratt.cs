namespace bostadskostnad
{
    class Hyresrätt 
    {
        private int hyra;
        public int Hyra {
            get { return hyra; }
            set { hyra = value; }
        }
        private decimal hyresökning;
        public decimal Hyresökning {
            get { return hyresökning; }
            set { hyresökning = value; }
        }
        private decimal aktieportfölj;
        public decimal Aktieportfölj {
            get { return aktieportfölj; }
            set { aktieportfölj = value; }
        }
        public int Årskostnad() {
            return hyra*12;
        }
        public void ÖkaHyran() {
            hyra = (int)((decimal)hyra*(1+hyresökning));
        }
    }
}