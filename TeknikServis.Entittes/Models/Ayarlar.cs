namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ayarlar")]
    public partial class Ayarlar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsyeriID { get; set; }

        public byte ButonGenislik { get; set; }

        public byte ButonYukseklik { get; set; }

        public byte Yetki { get; set; }

        public bool HizliSatis { get; set; }

        public bool Kasa { get; set; }

        public bool Cari { get; set; }

        public bool Firma { get; set; }

        public bool Irsaliye { get; set; }

        public bool Fatura { get; set; }

        public bool Banka { get; set; }

        public bool CekSenet { get; set; }

        public bool Taksit { get; set; }

       

        public bool Siparis { get; set; }

        public bool IrKasaBankayaYansit { get; set; }

        public bool? IrCariyeYansit { get; set; }

        public bool? FaStogaYansitma { get; set; }

        public bool? FaCariyeYansitma { get; set; }

        public bool? FaKasaBankayaYansitma { get; set; }

        public bool? FirmasuzUrunSatma { get; set; }

        public bool Rut { get; set; }

        public bool FaSikKullanilanKDVDahil { get; set; }

        public bool IrSikKullanilanKDVDahil { get; set; }

        [StringLength(5)]
        public string IrsaliyeSeri { get; set; }

        [StringLength(5)]
        public string FaturaSeri { get; set; }

        public int? UlkeVeyaSimgeKodu { get; set; }

        public int? MusteriTuru { get; set; }

        public int KDVOrani { get; set; }

        [StringLength(200)]
        public string FtpFirmaResimServerUrl { get; set; }

        [StringLength(200)]
        public string FtpFirmaResimErisimUrl { get; set; }

        [StringLength(100)]
        public string FtpFirmaResimKullaniciAdi { get; set; }

        [StringLength(30)]
        public string FtpFirmaResimParola { get; set; }

        [StringLength(200)]
        public string FtpFirmaResimEbatlamaUrl { get; set; }

        public bool HsDepoAktarmaAktif { get; set; }

        public bool FaDepoSecimiOtomatik { get; set; }

        public bool YeniSatisFaturasi { get; set; }

        public bool YeniAlisFaturasi { get; set; }

        public bool FirmaDurum { get; set; }

        public bool Ajanda { get; set; }

        public bool Sifre { get; set; }

        public byte FaBirimFiyatYuvarlamaHane { get; set; }

        public bool DovizKurlariMerkezBankasindanOtoGuncellensin { get; set; }

        public bool MobilIskontoTutariniCaridenDus { get; set; }

        public bool IrSatisIrsaliyesiKDVSifirHesapla { get; set; }

    
    }
}
