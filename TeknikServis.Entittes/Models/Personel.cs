namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Personel")]
    public partial class Personel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personel()
        {


        }



        public int? KartNo { get; set; }





        public long? TCKimlikNo { get; set; }

        public short? CalismaTipiID { get; set; }

        [StringLength(20)]
        public string SSKNo { get; set; }

        public short? KurumID { get; set; }

        public short? DepartmanID { get; set; }

        public short? BolumID { get; set; }

        public short? GrupID { get; set; }

        public short? ServisID { get; set; }

        [StringLength(500)]
        public string Adres { get; set; }

        [StringLength(20)]
        public string Il { get; set; }

        [StringLength(20)]
        public string Ilce { get; set; }

        public int? PostaKodu { get; set; }

        [StringLength(15)]
        public string IsTelNo { get; set; }

        [StringLength(15)]
        public string EvTelNo { get; set; }

        [StringLength(15)]
        public string CepTelNo { get; set; }

        [StringLength(30)]
        public string EMail { get; set; }

        [Column(TypeName = "date")]
        public DateTime? IseBaslamaTarihi { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? IseBaslamaSaati { get; set; }

        [Column(TypeName = "date")]
        public DateTime? IstenCikisTarihi { get; set; }

        public DateTime? IstenCikisSaati { get; set; }

        public short? BankaID { get; set; }

        [StringLength(10)]
        public string BankaSubeKodu { get; set; }

        [StringLength(20)]
        public string BankaHesapNo { get; set; }

        [StringLength(34)]
        public string BankaIbanNo { get; set; }

        public short? OgrenimDurumuID { get; set; }

        [StringLength(500)]
        public string OzelNot { get; set; }

        [StringLength(10)]
        public string KimlikSeri { get; set; }

        [StringLength(10)]
        public string KimlikNo { get; set; }

        [StringLength(30)]
        public string KimlikBabaAdi { get; set; }

        [StringLength(30)]
        public string KimlikAnaAdi { get; set; }

        [StringLength(30)]
        public string KimlikDogumYeri { get; set; }

        [Column(TypeName = "date")]
        public DateTime? KimlikDogumTarihi { get; set; }

        public short? KimlikMedeniHalId { get; set; }

        [StringLength(30)]
        public string KimlikIl { get; set; }

        [StringLength(30)]
        public string KimlikIlce { get; set; }

        [StringLength(30)]
        public string KimlikMahalleKoy { get; set; }

        [StringLength(10)]
        public string KimlikCiltNo { get; set; }

        [StringLength(10)]
        public string KimlikAileSiraNo { get; set; }

        [StringLength(10)]
        public string KimlikSiraNo { get; set; }

        [StringLength(10)]
        public string KimlikKayitNo { get; set; }

        [StringLength(30)]
        public string KimlikVerildigiYer { get; set; }

        public short? KimlikVerilisNedeniID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? KimlikVerilisTarihi { get; set; }

        public short? KanGrubuID { get; set; }

        public short? CinsiyetID { get; set; }

        [StringLength(30)]
        public string GiysiBedeni { get; set; }

        [StringLength(30)]
        public string IkametDurumu { get; set; }

        public decimal? KiraBedeli { get; set; }

        public byte? AyakkabiNo { get; set; }

        public byte? CocukSayisi { get; set; }

        public decimal? Boy { get; set; }

        public decimal? Kilo { get; set; }

        public DateTime? KayitZamani { get; set; }

        public int? KullaniciID { get; set; }

        [StringLength(30)]
        public string MACCAdress { get; set; }

        public bool? Aktif { get; set; }

        


    } 
    }

