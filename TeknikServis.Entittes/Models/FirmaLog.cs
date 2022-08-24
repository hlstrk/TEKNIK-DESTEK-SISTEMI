namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FirmaLog")]
    public partial class FirmaLog
    {
        public int? FirmaID { get; set; }


        [StringLength(30)]
        public string MusteriTuru { get; set; }

        public int? FirmaGrubuID { get; set; }

        [StringLength(200)]
        public string FirmaAdi { get; set; }

        public int? FirmaBirimID { get; set; }

        public string AdSoyad { get; set; }

        public byte? AlisKdvDurumID { get; set; }
        [Phone()]
        public string CepTelefonu { get; set; }

        public string Email { get; set; }

        public decimal? Kdv { get; set; }

        public decimal? ToplamFiyati { get; set; }

        public byte? SatisKdvDurumID { get; set; }

        public int? MevcutFirmaAdeti { get; set; }

        public int? KritikFirmaAdeti { get; set; }

        public bool? Aktif { get; set; }

        public int? KullaniciID { get; set; }

        public DateTime? KayitZamani { get; set; }

        [StringLength(30)]
        public string MACCAdress { get; set; }

        public string Notlar { get; set; }

        [StringLength(100)]
        public string ResimUrl { get; set; }

        public bool? HizliMenuAktif { get; set; }

        public int? HizliMenuSira { get; set; }

        public string Email2 { get; set; }

        public byte? SatisKdvDurumID2 { get; set; }

        public string Email3 { get; set; }

        public byte? SatisKdvDurumID3 { get; set; }

        [StringLength(30)]
        public string OzelKodu1 { get; set; }

        [StringLength(30)]
        public string OzelKodu2 { get; set; }

        [Column(TypeName = "image")]
        public byte[] Resim { get; set; }

        public int? MarkaID { get; set; }

        public int? UrunMalzemeID { get; set; }

        public int? UrunCinsiyetID { get; set; }

        public int? IsyeriID { get; set; }

        public int? AltFirmaGrubID { get; set; }

        public int? AltFirmaGrubID2 { get; set; }

        public int? AltFirmaGrubID3 { get; set; }

        public int? AktifServis { get; set; }

        public int? NumaraID { get; set; }

        public int? UzunlukID { get; set; }

        public string Email4 { get; set; }

        public byte? SatisKdvDurumID4 { get; set; }

        public string Email5 { get; set; }

        public byte? SatisKdvDurumID5 { get; set; }

        [StringLength(200)]
        public string FirmaAdi2 { get; set; }

        [StringLength(255)]
        public string FirmaKisaAciklama { get; set; }

        [StringLength(255)]
        public string FirmaAnahtarKelime { get; set; }

        public int? ParaBirimiID { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string Islem { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime LogZamani { get; set; }
    }
}
