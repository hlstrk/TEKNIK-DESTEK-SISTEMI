namespace TeknikServis.Dal.Concrete.EntityFramework.Context
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using TeknikServis.Dal;
    using TeknikServis.Entittes.Models;
    using TeknikServis.Entittes.PocoModels;

    public partial class TeknikServisContext : DbContext
    {
        public TeknikServisContext()
            : base("name=TeknikServisContext")
        {
            //Configuration.LazyLoadingEnabled = false;
        }

       
        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<Ayarlar> Ayarlar { get; set; }
      
        public virtual DbSet<Teklif> Teklif { get; set; }

        public virtual DbSet<Stok> Stok { get; set; }

        public virtual DbSet<Durumlar> Durumlar { get; set; }


        public virtual DbSet<Not> Not { get; set; }

        public virtual DbSet<TeknikDetayView> TeknikDetayView { get; set; }
        public virtual DbSet<Markalar> Markalar { get; set; }

        

        public virtual DbSet<ArizaTurleri> ArizaTurleri { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }

        public virtual DbSet<FirmaView> FirmaView { get; set; }

        public virtual DbSet<Gorev> Gorev { get; set; }
        ////public virtual DbSet<GorevView> GorevView { get; set; }

        public virtual DbSet<il> il { get; set; }

        public virtual DbSet<ilce> ilce { get; set; }
        public virtual DbSet<Model> Model { get; set; }
       
        public virtual DbSet<Resources> Resources { get; set; }
      
        
       
        public virtual DbSet<Firma> Firma { get; set; }

        public virtual DbSet<TeklifDetay> TeklifDetay { get; set; }


        public virtual DbSet<Envanter> Envanter { get; set; }



        public virtual DbSet<StokView> StokView { get; set; }

        public virtual DbSet<StokKategorileri> StokKategorileri { get; set; }

        public virtual DbSet<StokMarkalari> StokMarkalari { get; set; }

        public virtual DbSet<StokTurleri> StokTurleri { get; set; }

        public virtual DbSet<StokTurleriView> StokTurleriView { get; set; }

        public virtual DbSet<EnvanterView> EnvanterView { get; set; }

        public virtual DbSet<NotView> NotView { get; set; }

        public virtual DbSet<Bildirim> Bildirim { get; set; }


        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tanim> Tanim { get; set; }
        public virtual DbSet<TanimGrup> TanimGrup { get; set; }
        public virtual DbSet<Teknik> Teknik { get; set; }


        public virtual DbSet<PocoFirmaListesi> PocoFirmaListesi { get; set; }

        public virtual DbSet<Islem> Islem { get; set; }



        public virtual DbSet<KullaniciView> KullaniciView { get; set; }


        public virtual DbSet<TeknikView> TeknikView { get; set; }

        public virtual DbSet<EnvanterTurleri> EnvanterTurleri { get; set; }
        public virtual DbSet<EnvanterMarkalari> EnvanterMarkalari { get; set; }

        public virtual DbSet<Bolumler> Bolumler { get; set; }


        public virtual DbSet<Departmanlar> Deprartmanlar { get; set; }
        public virtual DbSet<AltDepartmanlar> AltDeprartmanlar { get; set; }





        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ayarlar>()
                .Property(e => e.IrsaliyeSeri)
                .IsUnicode(false);

            modelBuilder.Entity<Ayarlar>()
                .Property(e => e.FaturaSeri)
                .IsUnicode(false);

           
           
              


            modelBuilder.Entity<Kullanici>()
                .Property(e => e.CepTelNo)
                .IsUnicode(false);

            modelBuilder.Entity<Kullanici>()
                .Property(e => e.EMail)
                .IsUnicode(false);

            modelBuilder.Entity<Kullanici>()
                .Property(e => e.TCKimlikNo)
                .IsUnicode(false);

          

           

            modelBuilder.Entity<Model>()
                .Property(e => e.ModelAdi)
                .IsUnicode(false);

            

           


          

           

           


          

            modelBuilder.Entity<FirmaHareket>()
                .Property(e => e.Miktar)
                .HasPrecision(18, 0);

            modelBuilder.Entity<FirmaResim>()
                .Property(e => e.MaccAdress)
                .IsUnicode(false);

           

            modelBuilder.Entity<TanimGrup>()
                .HasMany(e => e.Tanim)
                .WithRequired(e => e.TanimGrup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FirmaLog>()
                .Property(e => e.Islem)
                .IsUnicode(false);
        }
    }
}
