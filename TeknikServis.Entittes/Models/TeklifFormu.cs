namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Teklifformu
    {
     
        public string FirmaUnvanı { get; set; }

        public string VergiKimlik { get; set; }

        public string ilgili { get; set; }

        public string iletisimadres  { get; set; }

        public string kur { get; set; }

        public double toplam { get; set; } 

        public double geneltoplam { get; set; }    

        public double kdvtoplam { get; set; }

        public string tekliftarihi { get; set; }

  
        public string TeklifNo { get; set; }

        public string gecerliliksuresi { get; set; }


        public string TelEposta { get; set; }   

        public List<TeklifDetay> Teklifler { get; set; }

       



    }
}
