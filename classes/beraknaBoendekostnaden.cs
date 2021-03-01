using System;

namespace bostadskostnad
{
    class BeräknaBoendekostnaden 
    {
        private Hyresrätt _hyresrätten = new Hyresrätt();
        private Bostadsrätt _bostadsrätten = new Bostadsrätt();
        private Hus _hus = new Hus();
        private int _antalÅr;
        private decimal _värdeökningAktier;
        private decimal _värdeökningBostadsrätt;
        private decimal _värdeökningHus;
        private int _delaSvaretMed;
        private int _försäljningskostnadBostad;
        public void Start() {
            AngeParametrar();

            //fyll på portföljerna
            var högstaPris = Math.Max(_bostadsrätten.Inköpspris, _hus.Inköpspris + _hus.Inköpskostnad);
            _hyresrätten.Aktieportfölj = högstaPris*0.15M;
            _bostadsrätten.Aktieportfölj = (högstaPris - _bostadsrätten.Inköpspris)*0.15M;
            _hus.Aktieportfölj = (högstaPris - (_hus.Inköpspris + _hus.Inköpskostnad))*0.15M;

            var hyresKostnad = 0;
            var bostadsrättKostnad = 0;
            var husKostnad = 0;
            for (int i = 0; i < _antalÅr; i++) {
                //fixa med hyreskostnaden
                hyresKostnad = hyresKostnad + _hyresrätten.Årskostnad();
                _hyresrätten.ÖkaHyran();

                //fixa med bostadsrattskostnaden
                bostadsrättKostnad = bostadsrättKostnad + _bostadsrätten.Årskostnad();
                _bostadsrätten.ÖkaKostnaden();
                _bostadsrätten.MinskaLån();

                //fixa med huskostnaden
                husKostnad = husKostnad + _hus.Årskostnad();
                _hus.ÖkaKostnaden();
                _hus.MinskaLån();

                var högstaÅrskostnad = HögstaÅrskostnaden();

                //fixa med hyresgästens portfölj
                _hyresrätten.Aktieportfölj = _hyresrätten.Aktieportfölj*(1+_värdeökningAktier) + högstaÅrskostnad - _hyresrätten.Årskostnad();

                //fixa med bostadsrättens portfölj
                _bostadsrätten.Värde = _bostadsrätten.Värde*(1+_värdeökningBostadsrätt);
                _bostadsrätten.Aktieportfölj = _bostadsrätten.Aktieportfölj*(1+_värdeökningAktier) + högstaÅrskostnad - _bostadsrätten.Årskostnad();

                //fixa med husets portfölj
                _hus.Värde = _hus.Värde*(1+_värdeökningHus);
                _hus.Aktieportfölj = _hus.Aktieportfölj*(1+_värdeökningAktier) + högstaÅrskostnad - _hus.Årskostnad();
            }
            Console.WriteLine("------------");
            Console.WriteLine("Totala lägsta bostadsutgifter för hyresrätt under {0} år är: {1}", _antalÅr, (int)(hyresKostnad/_delaSvaretMed));
            Console.WriteLine("Totala lägsta bostadsutgifter för bostadsrätt under {0} år är: {1}", _antalÅr, (int)(bostadsrättKostnad/_delaSvaretMed));
            Console.WriteLine("Totala lägsta bostadsutgifter för hus under {0} år är: {1}", _antalÅr, (int)(husKostnad/_delaSvaretMed));
            Console.WriteLine("------------");
            Console.WriteLine("Totala förmögenheten för hyresrätt efter {0} år är: {1}. Varav allt är aktievärde.", _antalÅr, (int)(_hyresrätten.Aktieportfölj/_delaSvaretMed));
            Console.WriteLine("Totala förmögenheten för bostadsrätt efter {0} år är: {1}. Varav husvärde {2} och aktievärde {3}.", 
                _antalÅr, 
                (int)((_bostadsrätten.Värde - _bostadsrätten.Lån + _bostadsrätten.Aktieportfölj - _försäljningskostnadBostad)/_delaSvaretMed),
                (int)((_bostadsrätten.Värde - _bostadsrätten.Lån - _försäljningskostnadBostad)/_delaSvaretMed),
                (int)(_bostadsrätten.Aktieportfölj/_delaSvaretMed));
            Console.WriteLine("Totala förmögenheten för hus efter {0} år är: {1}. Varav husvärde {2} och aktievärde {3}.", 
                _antalÅr, 
                (int)((_hus.Värde - _hus.Lån + _hus.Aktieportfölj - _försäljningskostnadBostad)/_delaSvaretMed),
                (int)((_hus.Värde - _hus.Lån - _försäljningskostnadBostad)/_delaSvaretMed),
                (int)(_hus.Aktieportfölj/_delaSvaretMed));
            Console.WriteLine("------------");
            Console.WriteLine("Tryck på en tangent för att stänga.");
            Console.ReadKey();
        }
        private void AngeParametrar() {
            //sätt generella parametrar
            _antalÅr = 20;
            _värdeökningAktier = 0.07M;
            _värdeökningBostadsrätt = 0.04M;
            _värdeökningHus = 0.04M;
            _delaSvaretMed = 1000; //använd gärna 1000 eller 1000*1000 för att få beloppet i tusental eller i miljoner
            _försäljningskostnadBostad = 80*1000; //exempelvis mäklare, fina till bostaden osv.

            //sätt parametrar för hyresrätten
            _hyresrätten.Hyra = 10000;
            _hyresrätten.Hyresökning = 0.02M;

            //sätt parametrar för bostadsrätten
            _bostadsrätten.Avgift = 4000;
            _bostadsrätten.Avgiftsökning = 0.02M;
            _bostadsrätten.Inköpspris = 2 * 1000 * 1000;
            _bostadsrätten.Lån = (int)(_bostadsrätten.Inköpspris*0.85M);
            _bostadsrätten.Värde = _bostadsrätten.Inköpspris;
            _bostadsrätten.Ränta = 0.014M;
            _bostadsrätten.FastAmmortering = 3000;
            _bostadsrätten.AmmorteraEnligtAmmorteringsreglerna = true;
            _bostadsrätten.LågLönExtraAmmortering = false;
            _bostadsrätten.Underhåll = 1000;
            _bostadsrätten.UnderhållÖkning = 0.02M;

            //sätt parametrar för hus
            _hus.Driftkostnad = 3000;
            _hus.Driftkostnadsökning = 0.02M;
            _hus.Inköpspris = 3 * 1000 * 1000;
            _hus.Inköpskostnad = (int)(_hus.Inköpspris*0.025M); //kostnad för lagfart och pantbrev, 2.5% antar att pantbrev finns på en del av lånekostnaden men inte allt.
            _hus.Lån = (int)(_bostadsrätten.Inköpspris*0.85M);
            _hus.Värde = _bostadsrätten.Inköpspris;
            _hus.Ränta = 0.014M;
            _hus.FastAmmortering = 3000;
            _hus.AmmorteraEnligtAmmorteringsreglerna = true;
            _hus.LågLönExtraAmmortering = false;
            _hus.Underhåll = 5000;
            _hus.UnderhållÖkning = 0.02M;
        }
        private int HögstaÅrskostnaden() {
            //räkna ut vem som har högst årskostnad
            var kostnader = new int[3];
            kostnader[0] = _hyresrätten.Årskostnad();
            kostnader[1] = _bostadsrätten.Årskostnad();
            kostnader[2] = _hus.Årskostnad();
            var högst = kostnader[0];
            foreach (var kostnad in kostnader) {
                if (kostnad > högst) {
                    högst = kostnad;
                }
            }
            return högst;
        }
    }
}