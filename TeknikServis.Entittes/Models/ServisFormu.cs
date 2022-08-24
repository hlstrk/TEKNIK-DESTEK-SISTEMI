namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ServisFormu
    {
     
        public string FirmaUnvanı { get; set; }

        public string VergiKimlik { get; set; }

        public string yetkili { get; set; }

        public string iletisimadres  { get; set; }
        public string belgetarihi { get; set; }

        public string Tel { get; set; }

        public string Eposta { get; set; }

        public string KDVDahilFiyat { get; set; }   

        public string FiyatTuru { get; set; }   

        public string ariza { get; set; }

        public string arizaaciklama { get; set; }

        public string BelgeNo { get; set; }
       

        public string servisbaslangic { get; set; } 

        public string servisbitis { get; set; }


        public string kullanici { get; set; }
        public List<TeknikDetayView> YapilanIslemler { get; set; }


        




    }
}
