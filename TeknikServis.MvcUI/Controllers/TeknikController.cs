using LinqKit;
using Microsoft.AspNet.SignalR;

using OfficeOpenXml;
using TeknikServis.Bll;
using TeknikServis.Dal.Concrete.EntityFramework.Repository;
using TeknikServis.Entittes.Models;
using TeknikServis.Interfaces;
using TeknikServis.MvcUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;

using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using SelectPdf;
using AutoMapper;

namespace TeknikServis.MvcUI.Controllers
{
    [AllowAnonymous]

    public class TeknikController : Controller
    {
        IIslemService islemService = new IslemManager(new EfIslemRepository());
        ITeknikService teknikService = new TeknikManager(new EfTeknikRepository());
        ITanimService kategoriService = new TanimManager(new EfTanimRepository());
        IGenericService<Islem> genericService2 = new GenericManager<Islem>(new EfGenericRepository<Islem>());
        IGenericService<Teknik> genericService1 = new GenericManager<Teknik>(new EfGenericRepository<Teknik>());
        IGenericService<KullaniciView> kullanicigeneric = new GenericManager<KullaniciView>(new EfGenericRepository<KullaniciView>());
        IGenericService<TeknikDetayView> islemgeneric = new GenericManager<TeknikDetayView>(new EfGenericRepository<TeknikDetayView>());
        IGenericService<Tanim> genericService3 = new GenericManager<Tanim>(new EfGenericRepository<Tanim>());
        IGenericService<StokView> stokgenericservice = new GenericManager<StokView>(new EfGenericRepository<StokView>());
        IGenericService<TeknikView> teknikgeneric = new GenericManager<TeknikView>(new EfGenericRepository<TeknikView>());
        IGenericService<ArizaTurleri> arizageneric = new GenericManager<ArizaTurleri>(new EfGenericRepository<ArizaTurleri>());
        IGenericService<Durumlar> durumgeneric = new GenericManager<Durumlar>(new EfGenericRepository<Durumlar>());


        CacheFonsiyon cacheFonsiyon;
        IKullaniciService KullaniciService;
        IFirmaService FirmaService;

        public TeknikController(IKullaniciService kullaniciService, IFirmaService firmaService)
        {
           
            KullaniciService = kullaniciService;
            FirmaService = firmaService;
            cacheFonsiyon = new CacheFonsiyon();

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

        public ActionResult TeknikListesi(string TeknikServisID = "", string teknikAdi = "")
        {

            if (Session["Role"] == null)
            {
                return  RedirectToAction("Giris","Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {

                    var predicate = PredicateBuilder.True<TeknikView>();

                    predicate = predicate.And(x => x.Aktif != false);
                    predicate = predicate.And(x => x.SilinmisMi != true);



                   

                   
                    var filtrefirma = Convert.ToInt32(Session["KfirmaID"]);
                    var filtrekullanici = Convert.ToInt32(Session["KullaniciID"]);
                    var filtrekullaniciid = Convert.ToInt32(Session["KullaniciID"]);
                    if (Session["Role"].ToString() == "Firma")
                    {
                        predicate = predicate.And(x => x.AitOlduguFirmaID==(filtrefirma));
                    }
                       
                    if (Session["Role"].ToString() == "Personel")
                    {
                        predicate = predicate.And(x => x.AtanmisPersonelID==filtrekullanici);
                    }
                    





                       
                    if (Session["Role"].ToString() == "FirmaPersonel")


                    {
                        predicate = predicate.And(x => x.AitOlduguKullaniciID==filtrekullaniciid);
                    }
                 


                    var liste = teknikgeneric.GetAll(predicate);
                  
                    


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeknikServisID, x.TeknikAdi, x.TeknikServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return  RedirectToAction("Giris","Kullanici");
            }
        }


        public ActionResult TeknikGeriAl(int TeknikServisID)
        {

            if (Session["Role"] == null)
            {
                RedirectToAction("Giris", "Kullanici");
                return new HttpStatusCodeResult(204);

            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {

                    #region Sil




                    var tekniksilinen = teknikService.Get(TeknikServisID);

                    tekniksilinen.Aktif = true;
                    tekniksilinen.SilinmisMi = false;

                    teknikService.Update(tekniksilinen);



                    return new HttpStatusCodeResult(204);




                    #endregion
                }
                else
                {
                    RedirectToAction("Giris", "Kullanici");
                    return new HttpStatusCodeResult(204);
                }



            }
        }




        [AllowAnonymous]

        public ActionResult ServisFormuOlusturSayfaIslem(int id)
        {

           


            var serisdetayfiltre = PredicateBuilder.True<TeknikDetayView>();
            var firmafiltre = PredicateBuilder.True<Firma>();
            

            serisdetayfiltre = serisdetayfiltre.And(x => x.TeknikServisID == id);

            var liste = new ServisFormu();

            var anaservis = teknikgeneric.Get(id);
            firmafiltre = firmafiltre.And(x => x.FirmaAdi.Contains(anaservis.AitOlduguFirma));
          
            var servisdetay = islemgeneric.GetAll(serisdetayfiltre);

            var FirmaVerileri = FirmaService.GetAll(firmafiltre)[0];

            liste.FirmaUnvanı = anaservis.AitOlduguFirma;
          
            liste.belgetarihi = anaservis.KapanisTarihi.ToString().Substring(0, anaservis.KapanisTarihi.ToString().Length - 6); ;
            liste.VergiKimlik = FirmaVerileri.vergidairesi +" / "+ FirmaVerileri.VergiKimlikNo;
            liste.yetkili = FirmaVerileri.AdSoyad;
            liste.iletisimadres = FirmaVerileri.Adres;
            liste.Tel = FirmaVerileri.CepTelefonu;
            liste.Eposta = FirmaVerileri.Email;
            liste.BelgeNo = anaservis.TicketString.ToString();
            liste.kullanici = anaservis.OlusturanKullaniciAdi;
            liste.servisbaslangic = anaservis.KayitZamani.ToString().Substring(0, anaservis.KayitZamani.ToString().Length - 6);
            liste.servisbitis = anaservis.KapanisTarihi.ToString().Substring(0, anaservis.KapanisTarihi.ToString().Length - 6);
            liste.ariza = anaservis.ArizaTuru;
            liste.arizaaciklama = anaservis.MusteriBeyani;


            liste.KDVDahilFiyat = anaservis.Fiyat.ToString();
            liste.KDVDahilFiyat = liste.KDVDahilFiyat.Replace("?", "₺");
           
            liste.FiyatTuru = anaservis.ParaBirimi.Replace("?", "₺"); ;


            liste.YapilanIslemler = servisdetay;





            return View(liste);
        }


        [AllowAnonymous]

        public ActionResult FaturaFormuOlusturSayfaIslem(int id)
        {


      

            //var serisdetayfiltre = PredicateBuilder.True<TeknikDetayView>();
            //var firmafiltre = PredicateBuilder.True<Firma>();


            //serisdetayfiltre = serisdetayfiltre.And(x => x.TeknikServisID == id);

            var liste = new ServisFormu();

            //var anaservis = teknikgeneric.Get(id);
            //firmafiltre = firmafiltre.And(x => x.FirmaAdi.Contains(anaservis.AitOlduguFirma));

            //var servisdetay = islemgeneric.GetAll(serisdetayfiltre);

            //var FirmaVerileri = FirmaService.GetAll(firmafiltre)[0];

            //liste.FirmaUnvanı = anaservis.AitOlduguFirma;

            //liste.belgetarihi = anaservis.KapanisTarihi.ToString().Substring(0, anaservis.KapanisTarihi.ToString().Length - 6); ;
            //liste.VergiKimlik = FirmaVerileri.vergidairesi + " / " + FirmaVerileri.VergiKimlikNo;
            //liste.yetkili = FirmaVerileri.AdSoyad;
            //liste.iletisimadres = FirmaVerileri.Adres;
            //liste.Tel = FirmaVerileri.CepTelefonu;
            //liste.Eposta = FirmaVerileri.Email;
            //liste.BelgeNo = anaservis.TicketString.ToString();
            //liste.kullanici = anaservis.OlusturanKullaniciAdi;
            //liste.servisbaslangic = anaservis.KayitZamani.ToString().Substring(0, anaservis.KayitZamani.ToString().Length - 6);
            //liste.servisbitis = anaservis.KapanisTarihi.ToString().Substring(0, anaservis.KapanisTarihi.ToString().Length - 6);
            //liste.ariza = anaservis.ArizaTuru;
            //liste.arizaaciklama = anaservis.MusteriBeyani;

            //liste.KDVDahilFiyat = anaservis.Fiyat.ToString();
            //liste.KDVDahilFiyat = liste.KDVDahilFiyat.Replace("?", "₺");
            //liste.FiyatTuru = anaservis.ParaBirimi;


            //liste.YapilanIslemler = servisdetay;





            return View(liste);
        }





        public ActionResult StokListesiKart(string StokServisID = "", string stokAdi = "")
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {
                    var predicate = PredicateBuilder.True<StokView>();
                     

                    var liste = stokgenericservice.GetAll(predicate);


                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        [AllowAnonymous]
        public ActionResult ServisFormuOlustur(int id)
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

                    var dosyaadi = "MTX_SERVİS_FORMU_" + id + ".pdf";
                    var something = new Rotativa.ActionAsPdf("ServisFormuOlusturSayfaIslem", new { id = itemid, makeCvSession, FileName = dosyaadi });

                    return something;






                }



                return RedirectToAction("Giris", "Kullanici");
            }
        }


        [AllowAnonymous]
        public ActionResult FaturaFormuOlustur(int id)
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

                    var dosyaadi = "MTX_FATURA_" + id + ".pdf";
                    var something = new Rotativa.ActionAsPdf("FaturaFormuOlusturSayfaIslem", new { id = itemid, makeCvSession, FileName = dosyaadi });

                    return something;






                }



                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult TeknikTamamlanan(string TeknikServisID = "", string teknikAdi = "")
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                    var predicate = PredicateBuilder.True<TeknikView>();

                    predicate = predicate.And(x => x.Aktif == false);
                    predicate = predicate.And(x => x.SilinmisMi != true);









                    var filtrefirma = Convert.ToInt32(Session["KfirmaID"]);
                    var filtrekullanici = Convert.ToInt32(Session["KullaniciID"]);
                    var filtrekullaniciid = Convert.ToInt32(Session["KullaniciID"]);
                    if (Session["Role"].ToString() == "Firma")
                        predicate = predicate.And(x => x.AitOlduguFirmaID==(filtrefirma));
                    if (Session["Role"].ToString() == "Personel")



                        predicate = predicate.And(x => x.AtanmisPersonelID==(filtrekullanici));
                    if (Session["Role"].ToString() == "FirmaPersonel")



                        predicate = predicate.And(x => x.AitOlduguKullaniciID==filtrekullaniciid);



                    var liste = teknikgeneric.GetAll(predicate);




                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeknikServisID, x.TeknikAdi, x.TeknikServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult TeknikSilinen(string TeknikServisID = "", string teknikAdi = "")
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                    var predicate = PredicateBuilder.True<TeknikView>();

                    predicate = predicate.And(x => x.SilinmisMi == true);






                    var liste = teknikgeneric.GetAll(predicate);




                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeknikServisID, x.TeknikAdi, x.TeknikServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult TeknikKarti(int TeknikServisID = 0 /*string ArizaTuru = ""*/)
        {
            if (Session["Role"] == null)
            {
                return  RedirectToAction("Giris","Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
                    ViewBag.ArizaTurleri = cacheFonsiyon.ArizaGetir();
                    ViewBag.Personeller = cacheFonsiyon.PersonelGetir();
                    ViewBag.TeknikDurum = cacheFonsiyon.DurumGetir();
                    ViewBag.FirmalarBAG = cacheFonsiyon.FirmaGetir();
                    ViewBag.HizliKullaniciBAG = cacheFonsiyon.KullaniciGetir();

                    var teknik = new TeknikView();

                    if (TeknikServisID != 0)
                    {
                        Session["YeniKayitMi"] = "Hayır";
                        teknik = teknikgeneric.Get(TeknikServisID);
                        Session["IslemAktarim"] = TeknikServisID;
                        Session["TBarkodAktarim"] = teknik.TicketString;
                        Session["AitOlduguFirmaID"] = teknik.AitOlduguFirmaID;

                        if(teknik.ParaBirimi=="?")
                            teknik.ParaBirimi= teknik.ParaBirimi.Replace("?", "₺");
                

                    }

                    else
                    {
                        teknik.KayitZamani = DateTime.Now;
                        Session["YeniKayitMi"] = "Evet";
                    }

                    return View(teknik);
                }

                return  RedirectToAction("Giris","Kullanici");

            }
        }
        [HttpPost]
        public JsonResult TeknikKarti(TeknikView stk)
        {
            if (Session["Role"] == null)
            {
                 RedirectToAction("Giris","Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
                    IslemSonucModel islem;
                    
                    try
                    {
                        if (stk.TeknikServisID == 0)
                        {
                            #region Kaydet
                            Session["YeniKayitMi"] = "Evet";
                           



                            if (stk.KayitZamani == null)
                            {
                                stk.KayitZamani = DateTime.Now;
                            }

                            if(Session["Role"].ToString()!="Admin")
                            {
                                var filtrekullaniciid = Convert.ToInt32(Session["KullaniciID"]);
                                stk.AitOlduguKullaniciID = filtrekullaniciid;
                                stk.AtanmisPersonelID=KullaniciService.GetAll().Where(x=>x.AdSoyad.Contains("Henüz")).ToList()[0].KullaniciID;
                                if(Session["Role"].ToString()=="Firma")
                                {
                                    stk.DurumID = durumgeneric.GetAll().Where(y => y.DurumAdi.Contains("İşlemde")).ToList()[0].DurumID;
                                }
                                else
                                {
                                    stk.DurumID = durumgeneric.GetAll().Where(y => y.DurumAdi.Contains("Onay")).ToList()[0].DurumID;
                                }

                            }
                            

                            

                            stk.DepartmanID = Convert.ToInt32(Session["DepartmanID"]);

                            if (stk.Durumu == "Tamamlandı" || stk.Durumu == "Reddedildi")
                            {
                                stk.Aktif = false;
                                stk.KapanisTarihi = stk.KapanisTarihi;
                            }
                            else
                            {
                                stk.Aktif = true;
                            }

                          
                            stk.TicketString = new TicketGenerator().TicketOlustur();
                    
                            var konu = "MTX-KURUMSAL DESTEK : DESTEK TALEBİ BİLDİRİMİ";




                            string icerik = System.IO.File.ReadAllText(Server.MapPath(@"~/Content/mailbildirim.cshtml"));

                            icerik = icerik.Replace("mtxolusturankullanici", stk.OlusturanKullaniciAdi);
                            icerik = icerik.Replace("mtxdestekkategorisi", stk.ArizaTuru);
                            icerik = icerik.Replace("mtxdestekicerigi", stk.MusteriBeyani);
                            if (Session["Role"].ToString() != "Admin")
                            {
                                stk.AitOlduguFirmaID = Convert.ToInt32(Session["KfirmaID"]);

                                stk.BolumID = Convert.ToInt32(Session["BolumID"]);
                                stk.AltDepartmanID = Convert.ToInt32(Session["AltDepartmanID"]);
                                stk.DepartmanID = Convert.ToInt32(Session["DepartmanID"]);
                              


                            }
                            else
                            {
                                var olkullanici = kullanicigeneric.Get(stk.AitOlduguKullaniciID);
                                stk.DepartmanID = olkullanici.DepartmanID;
                                stk.AltDepartmanID = olkullanici.AltDepartmanID;
                                stk.BolumID = olkullanici.BolumID;

                            }

                            if (stk.KayitZamani == null || stk.KayitZamani.ToString().Contains("0001"))
                            {
                                stk.KayitZamani = DateTime.Now;
                            }
                         

                            var admineposta = "teams@mtxsoft.net";
                            //EPostaGonder(icerik, admineposta, konu);
                           
                            Teknik mappedstk = Mapper.Map<Teknik>(stk);
                            var sonuc = teknikService.Add(mappedstk);

                           



                            if (sonuc != null)
                            {
                                var bildirim = new Bildirim()
                                {
                                    bildirimID = 0,
                                    bildirimIcerigi = "Yeni Bir Kayıt Oluşturuldu",
                                    olusturanFirma = stk.AitOlduguFirma,
                                    olusturanKullanici = stk.OlusturanKullaniciAdi,
                                    arizaTuru = stk.ArizaTuru,
                                    musteriBeyani = stk.MusteriBeyani,
                                    sayac = 3

                                };
                                IBildirimService bildirimService = new BildirimManager(new EfBildirimRepository());
                                bildirimService.Add(bildirim);
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Destek talebi başarıyla oluşturuldu..",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.TeknikServisID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Teknik Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString(),
                                      Data = sonuc.TeknikServisID
                                };
                            }
                            #endregion
                        }

                        else
                        {
                            #region Güncelle
                           
                            var nesne = teknikgeneric.Get(stk.TeknikServisID);

                            if (Session["Role"].ToString() != "Admin")
                            {


                                stk.AitOlduguFirmaID = Convert.ToInt32(Session["KfirmaID"]);

                                stk.BolumID = Convert.ToInt32(Session["BolumID"]);
                                stk.AltDepartmanID = Convert.ToInt32(Session["AltDepartmanID"]);
                                stk.DepartmanID = Convert.ToInt32(Session["DepartmanID"]);
                         
                            }

                            
                            nesne.TeknikServisID = stk.TeknikServisID;
                            if (stk.ArizaTuruID != 0)
                            {
                                nesne.ArizaTuruID = stk.ArizaTuruID;
                            }
                            if (stk.Fiyat!=0)
                            {
                                nesne.Fiyat = stk.Fiyat;
                            }
                        
                         if(stk.ArizaTuruID!=0)
                            {
                                nesne.ArizaTuruID = stk.ArizaTuruID;
                            }
                          
                            
                           
                     
                                if (stk.KayitZamani == null || stk.KayitZamani.ToString().Contains("0001"))
                                {
                                    stk.KayitZamani = DateTime.Now;
                                }
                                if (stk.KayitZamani == null)
                                {
                                    nesne.KayitZamani = stk.KayitZamani;
                                }
                    
                            
                            if (stk.OdenecekTutar == null)
                            {
                                nesne.OdenecekTutar = stk.OdenecekTutar;
                            }
                            if(stk.AitOlduguFirmaID!=0)
                            {
                                nesne.AitOlduguFirmaID = stk.AitOlduguFirmaID;
                            }
                   
                           
                  if(stk.CalisanYorumu!=null)
                            {
                                nesne.CalisanYorumu = stk.CalisanYorumu;
                            }

                          if(stk.MusteriBeyani!=null)
                            {
                                nesne.MusteriBeyani = stk.MusteriBeyani;
                            }
                           
                            if(stk.Fiyat!=null)
                            {
                                nesne.Fiyat = stk.Fiyat;
                            }
               
                            if (stk.ParaBirimi != null)
                                nesne.ParaBirimi = stk.ParaBirimi;
                         
                            if (stk.KapanisTarihi != null)
                                nesne.KapanisTarihi = stk.KapanisTarihi;
                        
                            nesne.SonIslemZamani = DateTime.Now;
                            nesne.Aktif = stk.Aktif;
                            if (stk.DurumID==13||stk.DurumID==12)
                            {
                                nesne.Aktif = false;
                                nesne.KapanisTarihi = stk.KapanisTarihi;
                            }
                            if (stk.DurumID != 0)
                            { 

                            nesne.DurumID = stk.DurumID;
                            }
                            if (stk.AtanmisPersonelID != 0)
                            {
                                nesne.AtanmisPersonelID = stk.AtanmisPersonelID;
                            }
                           if(Session["Role"].ToString()=="Admin")
                            {
                                nesne.Garanti = stk.Garanti;
                                nesne.Anlasmali = stk.Anlasmali;
                                if(nesne.AitOlduguKullaniciID!=stk.AitOlduguKullaniciID)
                                {
                                    var olkullanici = kullanicigeneric.Get(stk.AitOlduguKullaniciID);
                                    stk.DepartmanID = olkullanici.DepartmanID;
                                    stk.AltDepartmanID = olkullanici.AltDepartmanID;
                                    stk.BolumID = olkullanici.BolumID;
                                }
                            }
                           else if(Session["Role"].ToString() == "FirmaPersonel")
                            {
                                stk.AitOlduguKullaniciID = Convert.ToInt32(Session["KullaniciID"]);
                            }
                            if (stk.AitOlduguKullaniciID != 0)
                            {
                                nesne.AitOlduguKullaniciID = stk.AitOlduguKullaniciID;
                            }





                            if (stk.AtanmisPersonelID==0)
                            {
                                stk.AtanmisPersonelID = 200;
                            }
                         
                       
                           

                            Session["YeniKayitMi"] = "Hayır";

                        

                            Session["IslemAktarim"] = nesne.TeknikServisID;


                                var mappedstk = Mapper.Map<Teknik>(nesne);
                             if(mappedstk.DurumID == 0)
                            {
                                mappedstk.DurumID = 10;
                            }
                                var sonucUpdate = teknikService.Update(mappedstk);

                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Teknik Başarıyla Güncellendi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonucUpdate.TeknikServisID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Teknik Güncellenemedi.",

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
                            Aciklama = "Teknik İşlem Sırasında Hata Oluştu.",
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

        public ActionResult PersonelGoruntuleKart(int KullaniciID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "FirmaPersonel" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma")
                {






                    var kullanici = new KullaniciView();

                    if (KullaniciID != 0)
                        kullanici = kullanicigeneric.Get(KullaniciID);

               


                    kullanici.Sifre = null;

                    kullanici.SifreSifirlaToken = null;
                    kullanici.KullaniciParola = null;
                    if(kullanici.CepTelNo==null)
                    {
                        kullanici.CepTelNo = "Bu kullanıcıya ait telefon numarası bulunmamaktadır.";
                    }
                    else
                    {
                        kullanici.CepTelNo = "+90 " + kullanici.CepTelNo;

                    }



                    return View(kullanici);
                }
                else
                {
                    return RedirectToAction("Giris", "Kullanici");
                }
            }
        }


        public ActionResult KullaniciGoruntuleKart(int KullaniciID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "FirmaPersonel" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma")
                {






                    var kullanici = new KullaniciView();

                    if (KullaniciID != 0)
                        kullanici = kullanicigeneric.Get(KullaniciID);




                    kullanici.Sifre = null;

                    kullanici.SifreSifirlaToken = null;
                    kullanici.KullaniciParola = null;
                    kullanici.CepTelNo = "+90 " + kullanici.CepTelNo;



                    return View(kullanici);
                }
                else
                {
                    return RedirectToAction("Giris", "Kullanici");
                }
            }
        }

        public ActionResult KullanicininKayitlari(int KullaniciID = 0)
        {


            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {
                   

                    var liste = teknikgeneric.GetAll().Where(x=>x.AitOlduguKullaniciID==KullaniciID);

             



                    return View(liste.ToList());
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult TeknikIslemleri(int TeknikServisID = 0)
        {


            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {


                    var liste = islemgeneric.GetAll().Where(x => x.TeknikServisID == TeknikServisID);





                    return View(liste.ToList());
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult TeknikIslemCount(int TeknikServisID = 0)
        {


            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {


                    var liste = islemgeneric.GetAll().Where(x => x.TeknikServisID == TeknikServisID);





                    return Json(liste.Count(),JsonRequestBehavior.AllowGet);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }


        [HttpPost]
        public ActionResult EPostaGonder(string body, string receiver, string subject)
        {
            try
            {

                var fromAddress = new MailAddress("bilgi@mtxsoft.net");
                var toAddress = new MailAddress(receiver);

                using (var smtp = new SmtpClient
                {
                    Host = "smtp.yandex.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, "jwawuopddeieayrd")

                })
                {
                    using (var message = new System.Net.Mail.MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                    {
                        message.IsBodyHtml = true;
                        smtp.Send(message);
                    }
                }

                return View();
            }

            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }
        public ActionResult IslemListesiKart(int IslemServisID = 0, int TeknikServisID = 0)
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
                    var predicateislem = PredicateBuilder.New<TeknikDetayView>();






                    if (TeknikServisID != 0)
                    {
                        Session["IslemAktarimID"] = TeknikServisID;
                    }
                    else
                    {
                        TeknikServisID = Convert.ToInt32(Session["IslemAktarimID"]);

                    }


                    predicateislem = predicateislem.And(x => x.TeknikServisID == TeknikServisID);









                    var liste = islemgeneric.GetAll(predicateislem);


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.IslemServisID, x.IslemAdi, x.IslemServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult IslemKontrolList(int IslemServisID = 0, int TeknikServisID = 0)
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
                    var predicateislem = PredicateBuilder.New<TeknikDetayView>();






                    if (TeknikServisID != 0)
                    {
                       



                    }
                   


                    predicateislem = predicateislem.And(x => x.TeknikServisID == TeknikServisID);









                    var liste = islemgeneric.GetAll(predicateislem);


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.IslemServisID, x.IslemAdi, x.IslemServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }
        public ActionResult IslemKartiTeknik(int IslemServisID = 0, int TeknikServisID = 0)
        {
         
            var islem = new Islem();
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {


                    if (TeknikServisID != 0)
                    {
                        ViewBag.FirmalarBAG = cacheFonsiyon.FirmaGetir();
                        Session["IslemAktarimID"] = TeknikServisID;
                    
                        if (Session["AitOlduguFirmaID"] != null)
                        {
                            islem.FirmaID = Convert.ToInt32(Session["AitOlduguFirmaID"]);
                        }
                       
                    }
                   





                    return View(islem);
                }

                return RedirectToAction("Giris", "Kullanici");
        

            }
        }



    
        public ActionResult  IslemSil(int id)
        {

            if (Session["Role"] == null)
            {
                RedirectToAction("Giris", "Kullanici");
                return new HttpStatusCodeResult(204);

            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {

                    #region Sil
                    var islem = new TeknikDetayView();
                    islem =islemgeneric.Get(id);
                    var silinecekbakiye = islem.KDVDahilFiyat;
                    var eksilecekteknikid = islem.TeknikServisID;
                    var teknik=teknikService.Get(eksilecekteknikid);
                    teknik.Fiyat = (Convert.ToDouble(teknik.Fiyat) - Convert.ToDouble(silinecekbakiye));

                    teknik.TeknikServisID = eksilecekteknikid;
                    teknikService.Update(teknik);
                


                    islemService.Remove(id);
                    return new HttpStatusCodeResult(204);




                    #endregion
                }
                else
                {
                    RedirectToAction("Giris", "Kullanici");
                    return new HttpStatusCodeResult(204);
                }

          

            }
        }




        



        [HttpPost]
        public JsonResult IslemKarti(Islem stk)
        {
            
            if (Session["Role"] == null)
            {
                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {
                    IslemSonucModel islem;
                    var sonuc = new Islem();

                    try
                    {

                        if(stk.ParaBirimi=="?")
                        {
                            stk.ParaBirimi = "₺";
                        }
                        if (stk.IslemServisID == 0)
                        {
                            #region Kaydet
                            if(stk.KayitZamani== null)
                                {
                                stk.KayitZamani = DateTime.Now;
                            }
                            

                      
                        if(stk.KdvOrani==null)
                            {
                                stk.KdvOrani = 18;
                            }
                        if(stk.GenelToplam==null)
                            {
                                stk.GenelToplam = stk.BirimFiyat * stk.Miktar;
                            }
                
                                stk.KDVDahilFiyat = stk.GenelToplam + stk.KDVFiyat;
                                stk.TLFiyat = stk.DolarKuru * stk.KDVDahilFiyat;
                      

                            stk.TeknikServisID = Convert.ToInt32(Session["IslemAktarimID"]);
                    
                            stk.Aktif = true;
                            

                           
                       
                            if (Session["KullaniciID"] != null)
                            {
                                stk.KullaniciID = Convert.ToInt32(Session["KullaniciID"]);
                            }
                            var sonkayit = (islemService.GetAll().Where(x=>x.TeknikServisID==stk.TeknikServisID)).ToList();

                            if(sonkayit.Count!= 0)
                            {
                                if (sonkayit[0].ParaBirimi == "?")
                                {
                                    sonkayit[0].ParaBirimi = "₺";
                                }
                                if (sonkayit[0].ParaBirimi!=stk.ParaBirimi)
                                {
                                    islem = new IslemSonucModel()
                                    {
                                        IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                        Aciklama = "Aynı destek kaydına birden fazla para birimi işlenemez.",
                                        Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString(),
                                
                                    };
                                    return Json(islem);


                                }
                                else
                                {
                                   sonuc = islemService.Add(stk);



                                    #region Destek Talebi Fiyat Güncelle ve İşlem barkodu oluştur
                                    var varteknik = teknikService.Get(stk.TeknikServisID);
                                    stk.BarkodNo = varteknik.TicketString + "-" + sonuc.IslemServisID.ToString();
                                    sonuc = islemService.Update(stk);

                                    varteknik.Fiyat = (Convert.ToDouble(varteknik.Fiyat) + Convert.ToDouble(stk.KDVDahilFiyat));
                                    varteknik.SonIslemZamani = DateTime.Now;
                                    varteknik.ParaBirimi = stk.ParaBirimi;
                                    
                                    teknikService.Update(varteknik);

                                    #endregion
                                }
                            }
                            else
                            {
                                sonuc = islemService.Add(stk);

                                
                          

                                #region Destek Talebi Fiyat Güncelle Ve İşlem barkodu oluştur
                                var varteknik = teknikService.Get(stk.TeknikServisID);
                                stk.BarkodNo = varteknik.TicketString + "-" + sonuc.IslemServisID.ToString();
                                sonuc = islemService.Update(stk);
                                varteknik.Fiyat = (Convert.ToDouble(varteknik.Fiyat) + Convert.ToDouble(stk.KDVDahilFiyat));
                                varteknik.SonIslemZamani = DateTime.Now;
                                varteknik.ParaBirimi = stk.ParaBirimi;

                                teknikService.Update(varteknik);

                                #endregion
                            }





                            if (sonuc != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Islem Başarıyla Kaydedildi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.IslemServisID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Islem Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            #endregion
                        }

                        else
                        {

                            #region Güncelle
                            var nesne = islemgeneric.Get(stk.IslemServisID);


                            nesne.IslemServisID = stk.IslemServisID;

                            nesne.YapilanIslem = stk.YapilanIslem;
                            nesne.ParaBirimi = stk.ParaBirimi;
                    
                            nesne.Aktif = stk.Aktif;

                            nesne.Anlasmali = stk.Anlasmali;
                            nesne.YapilanIslem = stk.YapilanIslem;

                            if (stk.YapilanIslem != null)
                            {
                                string.Join(",", stk.YapilanIslem.ToString());
                            }

                            nesne.YapilanIslem = stk.YapilanIslem;
                            Islem mappedstk = Mapper.Map<Islem>(nesne);
                            var sonucUpdate = islemService.Update(mappedstk);

                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Islem Başarıyla Güncellendi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonucUpdate.IslemServisID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Islem Güncellenemedi.",

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
                            Aciklama = "Islem İşlem Sırasında Hata Oluştu.",
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
        public JsonResult TeknikSilOnay(int id)
        {

            if (Session["Role"] == null)
            {
                 RedirectToAction("Giris","Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
                    IslemSonucModel islem;
                    try
                    {

                        #region Sil
                        var silinecek = teknikService.Get(id);
                        silinecek.SilinmisMi = true;
                        silinecek.TeknikServisID = id;
                        var sonuc = teknikService.Update(silinecek);


                        if (sonuc!=null)
                        {
                            islem = new IslemSonucModel()
                            {
                                IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                Aciklama = "Teknik Başarıyla Silindi.",
                                Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString()
                            };
                        }
                        else
                        {
                            islem = new IslemSonucModel()
                            {
                                IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                Aciklama = "Teknik Silinemedi.",
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
                            Aciklama = "Teknik Silme Sırasında Hata Oluştu.",
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



        


        public ActionResult KategoriListesi(string TanimID = "", string teknikAdi = "")
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" )
                {
                    var predicate = PredicateBuilder.True<Tanim>();





                    var kid = Convert.ToInt32(TanimID == "" ? "0" : TanimID);

                   



                        



                    var liste = genericService3.GetAll(predicate);


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeknikServisID, x.TeknikAdi, x.TeknikServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult TanimKarti(int TanimID = 0, int TanimGrupID=0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" )
                {
                    ViewBag.KategoriGrup = cacheFonsiyon.KategoriGrupGetir();


                    var tanim = new Tanim();

                    if (TanimID != 0)
                    {
                        tanim = kategoriService.Get(TanimID);
                        
                    }

                    return View(tanim);
                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }







        [HttpPost]
        public JsonResult TanimKarti(Tanim stk)
        {
            if (Session["Role"] == null)
            {
                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" )
                {
                    IslemSonucModel islem;

                    try
                    {
                        if (stk.TanimID == 0)
                        {
                            #region Kaydet
                           
                            stk.KayitZamani = DateTime.Now;
                            
                           
                            var sonuc = kategoriService.Add(stk);

                           


                            if (sonuc != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kategori başarıyla eklendi..",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.TanimID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kategori Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString(),
                                    Data = sonuc.TanimID
                                };
                            }
                            CacheFonsiyon fonksiyon = new CacheFonsiyon();
                            fonksiyon.CacheTemizle();
                            fonksiyon.CacheOlustur();
                            #endregion
                        }

                        else
                        {
                            #region Güncelle
                            var nesne = kategoriService.Get(stk.TanimID);



                            nesne.TanimID = stk.TanimID;
                            nesne.TanimGrupID = stk.TanimGrupID;
                            nesne.TanimAdi = stk.TanimAdi;
                            nesne.Aktif = stk.Aktif;
                            

                            

                           

                            var sonucUpdate = kategoriService.Update(nesne);

                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Tanım başarıyla eklendi..",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonucUpdate.TanimID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Tanım Eklenemedi!.",

                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            CacheFonsiyon fonksiyon = new CacheFonsiyon();
                            fonksiyon.CacheTemizle();
                            fonksiyon.CacheOlustur();
                            #endregion
                        }
                    }


                    catch (Exception error)
                    {
                        islem = new IslemSonucModel()
                        {
                            IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Hata,
                            Aciklama = "Tanım Ekleme Sırasında Hata Oluştu.",
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






        public ActionResult TeknikSil(int TeknikServisID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma"|| Session["Role"].ToString() == "FirmaPersonel")
                {
                 

                    if (TeknikServisID != 0)
                    {
                        var teknik = new TeknikView();


                        teknik = teknikgeneric.Get(TeknikServisID);


                        return View(teknik);

                    }

               

                   
                 
                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }



        public ActionResult TanimSil(int TanimID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {


                    if (TanimID != 0)
                    {
                        var tanim = new Tanim();


                        tanim = kategoriService.Get(TanimID);

                        return View(tanim);

                    }





                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }
















        [HttpPost]
        public JsonResult TeknikSil(Teknik stk)
        {
            if (Session["Role"] == null)
            {
                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
                    IslemSonucModel islem;

                    try
                    {
                        if (stk.TeknikServisID != 0)
                        {
                            #region SilYl
                            var silinecek = teknikService.Get(stk.TeknikServisID);
                            silinecek.SilinmisMi = true;
                            silinecek.TeknikServisID = stk.TeknikServisID;
                            var sonucUpdate = teknikService.Update(silinecek);







                         

                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Teknik başarıyla silindi..",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonucUpdate.TeknikServisID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Teknik Silinemedi!.",
                                    Data = sonucUpdate.TeknikServisID,
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
                            Aciklama = "Teknik Silme Sırasında Hata Oluştu.",
                            AciklamaDetay = error.Message,
                            Baslik = IslemSonucuEnum.IslemSonucKodlari.Hata.ToString()
                        };
                        return Json(islem);
                    }

                    
                }
                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
        }








        [HttpPost]
        public JsonResult TanimSil(Tanim stk)
        {
            if (Session["Role"] == null)
            {
                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
                    IslemSonucModel islem;

                    try
                    {
                        if (stk.TanimID != 0)
                        {
                            #region Sil
                   



                    







                            var sonucUpdate = kategoriService.Remove(stk.TanimID);
                            CacheFonsiyon fonksiyon = new CacheFonsiyon();
                            fonksiyon.CacheTemizle();
                            fonksiyon.CacheOlustur();
                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kategori başarıyla silindi..",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                  
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kategori Silinemedi!.",
                               
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
                            Aciklama = "Teknik Silme Sırasında Hata Oluştu.",
                            AciklamaDetay = error.Message,
                            Baslik = IslemSonucuEnum.IslemSonucKodlari.Hata.ToString()
                        };
                        return Json(islem);
                    }


                }
                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
        }




        //Rapor Oluşturma



        //public ActionResult ServisRaporu(Teknik teknik)

        //{
           





        //}





























    }
}