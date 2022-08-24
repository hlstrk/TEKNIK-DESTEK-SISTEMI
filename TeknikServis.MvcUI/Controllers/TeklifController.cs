using LinqKit;

using OfficeOpenXml;
using TeknikServis.Bll;
using TeknikServis.Dal.Concrete.EntityFramework.Repository;
using TeknikServis.Entittes.Models;
using TeknikServis.Interfaces;
using TeknikServis.MvcUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Net.NetworkInformation;
using System.IO;
using Rotativa;
using System.Net;

namespace TeknikServis.MvcUI.Controllers
{
    [AllowAnonymous]

    public class TeklifController : Controller
    {

        ITeklifService teklifService = new TeklifManager(new EfTeklifRepository());
        ITeklifDetayService teklifDetayService = new TeklifDetayManager(new EfTeklifDetayRepository());


        IGenericService<Teklif> genericService1 = new GenericManager<Teklif>(new EfGenericRepository<Teklif>());
        IGenericService<Firma> firmagenericservice = new GenericManager<Firma>(new EfGenericRepository<Firma>());
        IGenericService<TeklifDetay> genericService3 = new GenericManager<TeklifDetay>(new EfGenericRepository<TeklifDetay>());
        
        CacheFonsiyon cacheFonsiyon;
        IKullaniciService KullaniciService;
        IFirmaService FirmaService;

        public TeklifController(IKullaniciService kullaniciService, IFirmaService firmaService)
        {

            KullaniciService = kullaniciService;
            cacheFonsiyon = new CacheFonsiyon();
            FirmaService = firmaService;
        }

        public ActionResult Index()
        {
            if (Session["Role"] == null)
            {
                return  RedirectToAction("Giris","Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                    ViewBag.Kategoriler = cacheFonsiyon.KategoriGetir();
                    return View();
                }
                return  RedirectToAction("Giris","Kullanici");
            }
        }

        [AllowAnonymous]

        public ActionResult TeklifFormuOlusturSayfaIslem(int id)
        {
            
               
            
           
                var teklifdetay = PredicateBuilder.True<TeklifDetay>();
                var firma = PredicateBuilder.True<Firma>();

                teklifdetay = teklifdetay.And(x => x.TeklifID == id);

                var liste = new Teklifformu();

                var anateklif = genericService1.Get(id);
                firma = firma.And(x => x.FirmaAdi.Contains(anateklif.AitOlduguFirma));

                var teklificerigi = genericService3.GetAll(teklifdetay);

                var FirmaVerileri = firmagenericservice.GetAll(firma)[0];

                liste.FirmaUnvanı = anateklif.AitOlduguFirma;
                liste.kur = anateklif.DolarKuru.ToString();
                liste.tekliftarihi = anateklif.TeklifTarihi.ToString();
                liste.VergiKimlik = FirmaVerileri.vergidairesi + FirmaVerileri.VergiKimlikNo;
                liste.ilgili = FirmaVerileri.AdSoyad;
                liste.iletisimadres = FirmaVerileri.Adres;
                liste.TelEposta = FirmaVerileri.CepTelefonu + " & " + FirmaVerileri.Email;
                liste.TeklifNo = anateklif.TeklifID.ToString();
                liste.gecerliliksuresi = anateklif.GecerlilikSuresi;


          double toplam = 0;
            double kdv = 0;
            double geneltoplam = 0;
         for( var x=teklificerigi.Count; x>0;x--)
            {
                toplam= toplam + Convert.ToDouble(teklificerigi[x-1].GenelToplam);
                kdv = kdv + Convert.ToDouble(teklificerigi[x - 1].KDVFiyat);
                geneltoplam = geneltoplam + Convert.ToDouble(teklificerigi[x - 1].KDVDahilFiyat);

            }
         liste.toplam = toplam;
            liste.geneltoplam=geneltoplam;
            liste.kdvtoplam=kdv;



                liste.Teklifler = teklificerigi;



                return View(liste);
            }



        
        [AllowAnonymous]
        public ActionResult TeklifFormuOlustur(int id)
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {






                    var itemid = id;




                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeklifID, x.TeklifAdi, x.TeklifID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    var makeCvSession = Session["makeCV"];

                    var dosyaadi = "MTX_TEKLIF_FORMU_" + id + ".pdf";
                    var something =  new Rotativa.ActionAsPdf("TeklifFormuOlusturSayfaIslem", new { id = itemid, makeCvSession, FileName = dosyaadi });
                 
                    return something;

                    
                }
            

            
                return RedirectToAction("Giris", "Kullanici");
            }
        }
        public ActionResult VerilenTeklifListesi(string TeklifID = "", string teklifAdi = "")
        {

            if (Session["Role"] == null)
            {
                return  RedirectToAction("Giris","Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                    var predicate = PredicateBuilder.True<Teklif>();






                   
                    var filtrefirma = Convert.ToInt32(Session["KfirmaID"]);
                    var filtrekullanici = Convert.ToInt32(Session["KullaniciID"]);
                    if (Session["Role"].ToString() == "Firma")
                      
                        predicate = predicate.And(x => x.OlusturanFirmaID==(filtrefirma));


                    var liste = genericService1.GetAll(predicate);


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeklifID, x.TeklifAdi, x.TeklifID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return  RedirectToAction("Giris","Kullanici");
            }
        }


        public ActionResult AlinanTeklifListesi(string TeklifID = "", string teklifAdi = "")
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                    var predicate = PredicateBuilder.True<Teklif>();







                    var filtrefirma = Convert.ToInt32(Session["KfirmaID"]);
                    var filtrekullanici = Convert.ToInt32(Session["KullaniciID"]);
                    if (Session["Role"].ToString() == "Firma")
                        predicate = predicate.And(x => x.AitOlduguFirmaID==(filtrefirma));


                    var liste = genericService1.GetAll(predicate);


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeklifID, x.TeklifAdi, x.TeklifID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }


        [HttpPost]
        public JsonResult TeklifKarti(Teklif stk)
        {
            if (Session["Role"] == null)
            {
                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                    IslemSonucModel islem;

                    try
                    {
                        if (stk.TeklifID == 0)
                        {
                            #region Kaydet

                            if (stk.KayitZamani == null)
                            {
                                stk.KayitZamani = DateTime.Now.ToString("dd MMM yyyy HH:mm");
                            }
                            if (stk.Durumu == null)
                            {
                                stk.Durumu = "Onay Bekliyor";
                            }
                            stk.OlusturanKullaniciID = Convert.ToInt32(Session["KullaniciID"]);
                            stk.OlusturanFirmaID = Convert.ToInt32(Session["KfirmaID"]);
                            stk.TicketString = new TicketGenerator().TicketOlustur();


                            var sonuc = teklifService.Add(stk);


                            if (sonuc != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Teklif Başarıyla Kaydedildi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.TeklifID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Teklif Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            #endregion
                        }

                        else
                        {
                            #region Güncelle
                            var nesne = teklifService.Get(stk.TeklifID);



                            nesne.TeklifID = stk.TeklifID;
                            nesne.GecerlilikSuresi = stk.GecerlilikSuresi;
                            nesne.TeklifTarihi = stk.TeklifTarihi;
                            nesne.DolarKuru = stk.DolarKuru;
                            nesne.Durumu = stk.Durumu;
                            nesne.YoneticiNotu = stk.YoneticiNotu;
                            nesne.AitOlduguFirma = stk.AitOlduguFirma;
                            if (stk.FiyaTuru != null)
                                nesne.FiyaTuru = stk.FiyaTuru;
                            if(stk.Fiyat!=null)
                            nesne.Fiyat = stk.Fiyat;
                            nesne.Aktif = stk.Aktif;
                            if(stk.OlusturanFirma!=null)
                            nesne.OlusturanFirma = stk.OlusturanFirma;
                            if(stk.OlusturanKullanici!=null)
                            nesne.OlusturanKullanici = stk.OlusturanKullanici;
                            if (stk.GenelToplam != null) 
                            nesne.GenelToplam = stk.GenelToplam;

                           




                            var sonucUpdate = teklifService.Update(nesne);

                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Teklif Başarıyla Güncellendi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonucUpdate.TeklifID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Teklif Güncellenemedi.",

                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            #endregion
                        }
                    }
                    catch (Exception error)
                    {
                        islem = new IslemSonucModel()
                        {
                            IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Hata,
                            Aciklama = "Teklif İşlem Sırasında Hata Oluştu.",
                            AciklamaDetay = error.Message,
                            Baslik = IslemSonucuEnum.IslemSonucKodlari.Hata.ToString()
                        };
                    }

                    return Json(islem);
                }
                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
        }


        [HttpPost]
        public JsonResult TeklifDetayKarti(TeklifDetay stk)
        {
            if (Session["Role"] == null)
            {
                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                    IslemSonucModel islem;

                    try
                    {
                      
                            #region Kaydet

                            if (stk.EklenmeTarihi == null)
                            {
                                stk.EklenmeTarihi = DateTime.Now.ToString("dd MMM yyyy HH:mm");
                            }
                          
                            stk.EkleyenKisiID = Convert.ToInt32(Session["KullaniciID"]);

                            var sonuc = teklifDetayService.Add(stk);


                            if (sonuc != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Teklif İçeriği Başarıyla Kaydedildi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.TeklifID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Teklif Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            #endregion
                        

                       
                    }
                    catch (Exception error)
                    {
                        islem = new IslemSonucModel()
                        {
                            IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Hata,
                            Aciklama = "Teklif İşlem Sırasında Hata Oluştu.",
                            AciklamaDetay = error.Message,
                            Baslik = IslemSonucuEnum.IslemSonucKodlari.Hata.ToString()
                        };
                    }

                    return Json("");
                }
                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
        }




        public ActionResult TeklifKarti(int TeklifID = 0)
        {
            if (Session["Role"] == null)
            {
                return  RedirectToAction("Giris","Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                    ViewBag.ArizaTurleri = cacheFonsiyon.ArizaGetir();
                    ViewBag.Personeller = cacheFonsiyon.KullaniciGetir();
                    ViewBag.TeklifDurum = cacheFonsiyon.DurumGetir();
                    ViewBag.FirmalarBAG = cacheFonsiyon.FirmaGetir();

                    var teklif = new Teklif();

                    if (TeklifID != 0)
                    {
                        teklif = teklifService.Get(TeklifID);
                        Session["TeklifDetayID"] = teklif.TeklifID;
                    }

                    else
                    {
                        if (NetworkInterface.GetIsNetworkAvailable() == true)
                        {
                            //MessageBox.Show("Güncel Kur Oranları Merkez Bankasından Alındı.", "Başarılı Bilgi Alımı");


                            /////////////////////////////////////////////////////////////////////KUR BİLGİLERİNİ GETİREN KOD
                            // Bugün (en son iş gününe) e ait döviz kurları için
                            string today = "http://www.tcmb.gov.tr/kurlar/today.xml";

                            var xmlDoc = new XmlDocument();
                            xmlDoc.Load(today);

                            // Xml içinden tarihi alma - gerekli olabilir
                            DateTime exchangeDate = Convert.ToDateTime(xmlDoc.SelectSingleNode("//Tarih_Date").Attributes["Tarih"].Value);

                            string USD = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;

                            var kur = USD.Replace(".", ",");
                            kur= kur.Substring(0, 5);
                           teklif.DolarKuru = Convert.ToDouble(kur);

                        }
                     }

                        return View(teklif);
                }

                return  RedirectToAction("Giris","Kullanici");
            }
        }


        public ActionResult TeklifDetayListesi(int TeklifID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {


                    var predicateteklif = PredicateBuilder.New<TeklifDetay>();


                    if (Session["TeklifDetayID"]!=null)
                    {
                        var filtreid = Convert.ToInt32(Session["TeklifDetayID"]);

                        predicateteklif = predicateteklif.And(x => x.TeklifID == filtreid);

                    }
                 
                    


                  


                    var liste = genericService3.GetAll(predicateteklif);

                   
                 
                   


                    return View(liste);
                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }



        public ActionResult TeklifDetayKarti(int TeklifID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                    ViewBag.ArizaTurleri = cacheFonsiyon.ArizaGetir();
                    ViewBag.Personeller = cacheFonsiyon.KullaniciGetir();
                    ViewBag.TeklifDurum = cacheFonsiyon.DurumGetir();
                    ViewBag.FirmalarBAG = cacheFonsiyon.FirmaGetir();

                    var teklifdetay = new TeklifDetay();
                    teklifdetay.TeklifID = TeklifID;
                    

                    return View(teklifdetay);
                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }
        public ActionResult TeklifSilKarti(int TeklifID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {


                    
                        var teklifdetay = new TeklifDetay();


                     


                        return View(teklifdetay);

                    





                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }


        [HttpPost]
        public JsonResult TeklifSil(int id)
        {

            if (Session["Role"] == null)
            {
                 RedirectToAction("Giris","Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                    IslemSonucModel islem;
                    try
                    {

                        #region Sil
                        var sonuc = teklifService.Remove(id);

                        if (sonuc)
                        {
                            islem = new IslemSonucModel()
                            {
                                IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                Aciklama = "Teklif Başarıyla Silindi.",
                                Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString()
                            };
                        }
                        else
                        {
                            islem = new IslemSonucModel()
                            {
                                IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                Aciklama = "Teklif Silinemedi.",
                                Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                            };
                        }
                        #endregion
                    }
                    catch (Exception error)
                    {
                        islem = new IslemSonucModel()
                        {
                            IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Hata,
                            Aciklama = "Teklif Silme Sırasında Hata Oluştu.",
                            AciklamaDetay = error.Message,
                            Baslik = IslemSonucuEnum.IslemSonucKodlari.Hata.ToString()
                        };
                    }

                    return Json(islem);
                }
                 RedirectToAction("Giris","Kullanici");
                return Json("");
            }
        }

      
        //public ActionResult PersonelGetir()
        //{
        //    var items = Kullanici..ToList();
        //    if (items != null)
        //    {
        //        ViewBag.data = items;
        //    }

        //    return View();
        //}


    }
}