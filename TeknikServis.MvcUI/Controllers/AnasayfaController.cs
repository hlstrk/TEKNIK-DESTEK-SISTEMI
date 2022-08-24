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
using AutoMapper;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Net.Mail;
using System.Net;

namespace TeknikServis.MvcUI.Controllers
{
    [AllowAnonymous]

    public class AnasayfaController : Controller
    {
        IGenericService<Departmanlar> DepartmanService = new GenericManager<Departmanlar>(new EfGenericRepository<Departmanlar>());
        IGenericService<ArizaTurleri> arizageneric = new GenericManager<ArizaTurleri>(new EfGenericRepository<ArizaTurleri>());
        IGenericService<Durumlar> durumgeneric = new GenericManager<Durumlar>(new EfGenericRepository<Durumlar>());
        ITeknikService teknikService = new TeknikManager(new EfTeknikRepository());
        IIslemService islemService = new IslemManager(new EfIslemRepository());
        IKullaniciService KullaniciService;
        ITeklifService teklifService = new TeklifManager(new EfTeklifRepository());
        IGenericService<Teknik> genericService1 = new GenericManager<Teknik>(new EfGenericRepository<Teknik>());
        IGenericService<TeknikView> teknikgeneric = new GenericManager<TeknikView>(new EfGenericRepository<TeknikView>());
        IGenericService<TeknikDetayView> islemgeneric = new GenericManager<TeknikDetayView>(new EfGenericRepository<TeknikDetayView>());
        IGenericService<KullaniciView> kullanicigeneric = new GenericManager<KullaniciView>(new EfGenericRepository<KullaniciView>());
        INotService notService = new NotManager(new EfNotRepository());

        IGenericService<Not> genericService4 = new GenericManager<Not>(new EfGenericRepository<Not>());
        IGenericService<NotView> notgeneric = new GenericManager<NotView>(new EfGenericRepository<NotView>());


        //IGenericService<GorevView> Gorevgeneric = new GenericManager<GorevView>(new EfGenericRepository<GorevView>());
        CacheFonsiyon cacheFonsiyon;

        IGenericService<Gorev> gorevservice = new GenericManager<Gorev>(new EfGenericRepository<Gorev>());

        IFirmaService FirmaService;




        public AnasayfaController(IKullaniciService kullaniciService, IFirmaService firmaService)
        {
            cacheFonsiyon = new CacheFonsiyon();
            KullaniciService = kullaniciService;
            FirmaService = firmaService;
        }

        public ActionResult Index()
        {
            if (Session["Role"] == null)
            {

                return RedirectToAction("Giris", "Kullanici");


            }

            else
            {

                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {

                    var kullaniciadi = Convert.ToInt32(Session["KullaniciID"]);
                    var filtredepartman =0;
                    if (Session["DepartmanID"] != null)
                    {
                        filtredepartman = Convert.ToInt32(Session["DepartmanID"]);
                    }


                    var filtre = 0;
                    if (Session["KfirmaID"] != null)
                    {
                        filtre = Convert.ToInt32(Session["KfirmaID"]);
                    }



                    var predicate = PredicateBuilder.True<TeknikView>();
                    if (Session["Role"].ToString() != "Admin")
                        predicate = predicate.And(x => x.AitOlduguFirmaID == filtre);

                    var liste = teknikgeneric.GetAll(predicate);
                    //Toplam İşlem Adeti
                    Session["toplamislemadedi"] = liste.Count(x => x.SilinmisMi != true);




                    //Admin Toplam İşlem Adedi
                    Session["toplamislemadediadmin"] = liste.Count(x => x.SilinmisMi != true);

                    //Admin Tamamlanan İşlem Adedi
                    Session["FTIAAdmin"] = liste.Count(x => (x.SilinmisMi != true)&&(x.Durumu.Contains("Tamamlandı")));

                    //Admin Onay Bekleyen Talep
                    Session["OnayBekleyenAdmin"] = liste.Count(x => (x.DurumID==(10) && (x.Aktif != false) && (x.SilinmisMi != true)));

                    //Kullanıcının oluşturduğu İşlem Adeti
                    Session["KOIA"] = liste.Count(x => (x.AitOlduguKullaniciID == kullaniciadi) && (x.Aktif != false) && (x.SilinmisMi != true));

                    //Personele Atanan İşlem Adeti
                    if (Session["Role"].ToString() == "Personel")

                        Session["KAIA"] = liste.Count(x => x.AtanmisPersonelID==kullaniciadi && (x.SilinmisMi != true));


                    //Kullanıcının oluşturduğu Tamamlanan İşlem Adeti

                    Session["KOTIA"] = liste.Count(x => (x.AitOlduguKullaniciID == kullaniciadi) && (x.Aktif == false) && (x.SilinmisMi != true));


                    //Firmanın Tamamlanan İşlem Adedi

                    Session["FTIA"] = liste.Count(x => (x.SilinmisMi != true));


                    //Kullanıcının oluşturduğu İşlemde Olan İşlem Adeti
                    Session["KOIOIM"] = liste.Count(x => (x.AitOlduguKullaniciID == kullaniciadi) && (x.Aktif == true) && (x.SilinmisMi != true));




                    //Firmanın onaylayacağı işlem adedi


                    Session["FOIA"] = liste.Count(x => (x.DurumID==10) && (x.Aktif == true) && (x.SilinmisMi != true) && (x.DepartmanID == filtredepartman));




                    //Kullanıcının Onay Bekleyen İşlem Adedi


                    Session["KOBIA"] = liste.Count(x => (x.DurumID == 10) && (x.AitOlduguKullaniciID == kullaniciadi) && (x.SilinmisMi != true));


                }
                else
                {
                    return View();
                }
            }
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
                    ViewBag.Kategoriler = cacheFonsiyon.KategoriGetir();
                    return View();
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        //public ActionResult OlusturulanGorevlerKart()
        //{

        //    if (Session["Role"] == null)
        //    {
        //        return RedirectToAction("Giris", "Kullanici");
        //    }
        //    else
        //    {
        //        if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
        //        {





        //            var filtrefirma = Convert.ToInt32(Session["KfirmaID"]);
        //            var filtrekullanici = Convert.ToInt32(Session["KullaniciID"]);


        //            var analiste = Gorevgeneric.GetAll().Where(x => (x.FirmaID == filtrefirma && x.OlusturanKullaniciID == filtrekullanici) && x.silinmismi != true).ToList();



        //            for (int i = analiste.Count(); i > 0; i--)
        //            {
        //                analiste.ToList()[i-1].KullaniciResim = KullaniciService.Get(analiste.ToList()[i-1].AitOlanKullaniciID).ProfilResmi;
        //            };

        //            return View(analiste.ToList());
        //        }
        //        return RedirectToAction("Giris", "Kullanici");
        //    }
        //}


        //public ActionResult AitGorevlerKart()
        //{

        //    if (Session["Role"] == null)
        //    {
        //        return RedirectToAction("Giris", "Kullanici");
        //    }
        //    else
        //    {
        //        if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
        //        {

        //            var filtrefirma = Convert.ToInt32(Session["KfirmaID"]);
        //            var filtrekullanici = Convert.ToInt32(Session["KullaniciID"]);

        //            var analiste = Gorevgeneric.GetAll().Where(x => (x.FirmaID == filtrefirma && x.AitOlanKullaniciID == filtrekullanici) && x.silinmismi != true || (x.toplugorevmi == true) && x.silinmismi != true&&x.OlusturanKullaniciID!=filtrekullanici).ToList();
        //            for (int i = analiste.Count(); i >0; i--)
        //            {
        //                analiste.ToList()[i-1].KullaniciResim = KullaniciService.Get(analiste.ToList()[i-1].OlusturanKullaniciID).ProfilResmi;
        //            };

        //            return View(analiste.ToList());
        //        }
        //        return RedirectToAction("Giris", "Kullanici");
        //    }
        //}

        public ActionResult GorevKarti(int GorevID = 0)
        {

            var liste = new Gorev();
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                var FirmaID = Convert.ToInt32(Session["KfirmaID"]);
                    ViewBag.PersonelGorev = KullaniciService.GetAll().Where(x=>x.FirmaID==FirmaID);

                    if (GorevID != 0)
                    {



                      liste = gorevservice.Get(GorevID);
                        

                    }






                    return View(liste);
                }

                return RedirectToAction("Giris", "Kullanici");


            }
        }





        public ActionResult NotEkle()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {


                return View();


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
                    if (kullanici.CepTelNo == null)
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


                    var liste = teknikgeneric.GetAll().Where(x => x.AitOlduguKullaniciID == KullaniciID);





                    return View(liste.ToList());
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }



        public ActionResult OnaylanacakTeklifListesi()
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
                    predicate = predicate.And(x => x.Durumu.Contains("Onay Bekliyor"));


                    var liste = teklifService.GetAll(predicate);


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeklifID, x.TeklifAdi, x.TeklifID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }



        public ActionResult NotEkleKarti(int NotID)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {

                    var Not = new Not();

                    if (NotID != 0)
                        Not = notService.Get(NotID);

                    return View(Not);
                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }


        public ActionResult DuyuruEkle(int NotID)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {

                    var Not = new Not();

                    if (NotID != 0)
                        Not = notService.Get(NotID);

                    return View(Not);
                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }




        public ActionResult NotListesi(string NotID = "", string NotTuru = "")
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
                    var predicate = PredicateBuilder.New<Not>();



                    var filtrefirma = Convert.ToInt32(Session["KfirmaID"]);
                    var filtrekullanici = Convert.ToInt32(Session["KullaniciID"]);


                    predicate = predicate.And(x => x.FirmaID == filtrefirma);



                    var analiste = notgeneric.GetAll().Where(x => (x.FirmaID == filtrefirma && x.DuyuruMu != false && x.MTXDuyurusuMu != true && x.YapilacakMi != true) || (x.KullaniciID == filtrekullanici && x.MTXDuyurusuMu != true && x.YapilacakMi != true));




                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.NotServisID, x.NotAdi, x.NotServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(analiste.ToList());
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult Duyurular()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
                    var predicate = PredicateBuilder.New<NotView>();






                    predicate = predicate.And(x => x.MTXDuyurusuMu == true);



                    var analiste = notgeneric.GetAll(predicate);

                    analiste.Reverse();
                    var anan = analiste.Take(3);
                    // Reverse the list






                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.NotServisID, x.NotAdi, x.NotServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(anan.ToList());
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }



        public ActionResult DuyuruListesi()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
                    var predicate = PredicateBuilder.New<NotView>();






                    predicate = predicate.And(x => x.MTXDuyurusuMu == true);



                    var analiste = notgeneric.GetAll(predicate);

                    analiste.Reverse();
                   
                    // Reverse the list






                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.NotServisID, x.NotAdi, x.NotServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(analiste.ToList());
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }




        public ActionResult OnemliBildirimGetir()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
                    var predicate = PredicateBuilder.New<Not>();






                    predicate = predicate.And(x => x.MTXDuyurusuMu == true);
                    predicate = predicate.And(x => x.onemderecesi == "yuksek");



                    var analiste = genericService4.GetAll(predicate);

                    analiste.Reverse();

                    // Reverse the list

                    if(analiste.Count!=0)
                    {
                        return Json(analiste[0], JsonRequestBehavior.AllowGet);
                    }




                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.NotServisID, x.NotAdi, x.NotServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return Json(null);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }



        public ActionResult ToDoList(int sayfa=0,string NotID = "", string NotTuru = "")
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
                    var predicate = PredicateBuilder.New<Not>();



                    var filtrefirma = Convert.ToInt32(Session["KfirmaID"]);
                    int filtrekullanici = Convert.ToInt32(Session["KullaniciID"]);


                    predicate = predicate.And(x => x.FirmaID == filtrefirma);



                    var liste = notgeneric.GetAll().Where(x => (x.FirmaID == filtrefirma && x.DuyuruMu != false) || (x.KullaniciID == filtrekullanici) && (x.YapilacakMi == true)).Reverse();
                    var analiste = liste;
                    if (sayfa==0)
                    {
                    analiste = liste.Take(10);
                    }


                    if (sayfa == 1)
                    {
                 analiste = liste.Take(10);
                    }


                    if (sayfa == 2)
                    {
                     analiste = liste.Skip(10);
                        analiste=analiste.Take(10); 
                    }


                    if (sayfa == 3)
                    {
                     analiste = liste.Skip(20);
                        if (analiste.Count() == 0)
                        {
                           analiste = liste;
                        }
                    }


                    



                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.NotServisID, x.NotAdi, x.NotServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(analiste.ToList());
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }
        public ActionResult _istatistikler()
        {
            if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma")
            {
                return View();
            }
            else
            {
                return null;
            }



        }


        public ActionResult Chat()
        {
            if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma")
            {
                return View();
            }
            else
            {
                return null;
            }
             


        }








        public ActionResult ProcessImage()
        {
            var imgkul = Convert.ToInt32(Session["KullaniciID"]);
            var kullanicik = KullaniciService.Get(imgkul);
            if (kullanicik.ProfilResmi != null)
            {


                return new FileContentResult(kullanicik.ProfilResmi, "image/jpeg");
            }

            else
            {
                var img = Image.FromFile(Server.MapPath(@"~/Content/Images/defaultuser.JPG"));
                var ms = new MemoryStream();

                img.Save(ms, img.RawFormat);
                var foto = ms.ToArray();







                return new FileContentResult(foto, "image/jpeg");
            }
        }


        public ActionResult SifreDegisKarti(int KullaniciID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "FirmaPersonel" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma")
                {

                   




                    var kullanici = new Kullanici();

                    if (KullaniciID != 0)
                        kullanici = KullaniciService.Get(KullaniciID);

                    Session["SifreKullaniciID"] = KullaniciID;


                    kullanici.Sifre = null;



                    return View(kullanici);
                }
                else
                {
                    return RedirectToAction("Giris", "Kullanici");
                }
            }
        }
        [HttpPost]
        public JsonResult SifreDegisKarti(Kullanici stk)
        {
            if (Session["Role"] == null)
            {
                RedirectToAction("Giris", "Kullanici");

                IslemSonucModel islem;
                islem = new IslemSonucModel()
                {
                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                    Aciklama = "Kullanıcı Düzenleme Yada Ekleme Yetkiniz Bulunmuyor...",
                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()

                };
                return Json(islem, JsonRequestBehavior.AllowGet);

            }
            else
            {
                if (Session["Role"].ToString() == "FirmaPersonel" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma")
                {
                    IslemSonucModel islem;

                    var nesne = KullaniciService.Get(stk.KullaniciID);
                    nesne.KullaniciID = stk.KullaniciID;

                    try
                    {
                        #region DosyaYukleme






                        #endregion




                        #region SifreDegis




                        if (stk.Sifre != null)
                        {
                            nesne.KullaniciParola = new ToPasswordRepository().Md5(stk.Sifre);
                            nesne.Sifre = nesne.KullaniciParola;
                            var sonucUpdate = KullaniciService.Update(nesne);


                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Profiliniz başarıyla güncellendi... ",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonucUpdate.KullaniciID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Şifre güncellenemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                        }
                        else
                        {
                            islem = new IslemSonucModel()
                            {
                                IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                Aciklama = "Profil Resmi Başarıyla Güncellendi.",
                                Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),

                            };
                        }







                        #endregion

                    }
                    catch (Exception error)
                    {
                        islem = new IslemSonucModel()
                        {
                            IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Hata,
                            Aciklama = "Şifre güncellenemedi.",
                            AciklamaDetay = error.Message,
                            Baslik = IslemSonucuEnum.IslemSonucKodlari.Hata.ToString()

                        };
                        return Json(islem);
                    }

                    return Json(islem);
                }

                else
                {
                    IslemSonucModel islem;
                    islem = new IslemSonucModel()
                    {
                        IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                        Aciklama = "Kullanıcı Düzenleme Yada Ekleme Yetkiniz Bulunmuyor...",
                        Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()

                    };
                    return Json(islem);
                }
            }





        }



        [HttpPost]
        public ActionResult DosyaYukle(Kullanici stk)
        {
            if (Session["Role"] == null)
            {
                RedirectToAction("Giris", "Kullanici");


                return new HttpStatusCodeResult(204);

            }
            else
            {
                if (Session["Role"].ToString() == "FirmaPersonel" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma")
                {

                    stk.KullaniciID = Convert.ToInt32(Session["KullaniciID"]);
                    var nesne = KullaniciService.Get(stk.KullaniciID);
                    nesne.KullaniciID = stk.KullaniciID;

                    try
                    {
                        #region DosyaYukleme

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                                                                        //Use the following properties to get file's name, size and MIMEType
                            int fileSize = file.ContentLength;
                            string fileName = nesne.KullaniciAdi;
                            string mimeType = file.ContentType;
                            System.IO.Stream fileContent = file.InputStream;
                            //To save file, use SaveAs method

                            MemoryStream ms = new MemoryStream();
                            nesne.ProfilResmi = ConvertToByte(file);

                            nesne.KullaniciID = stk.KullaniciID;
                            var sonucUpdate = KullaniciService.Update(nesne);

                        }




                        #endregion



                    }
                    catch (Exception error)
                    {

                        return new HttpStatusCodeResult(204);
                    }

                    return new HttpStatusCodeResult(204);
                }

                else
                {
                    IslemSonucModel islem;
                    islem = new IslemSonucModel()
                    {
                        IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                        Aciklama = "Kullanıcı Düzenleme Yada Ekleme Yetkiniz Bulunmuyor...",
                        Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()

                    };
                    return new HttpStatusCodeResult(204);
                }
            }





        }


        public byte[] ConvertToByte(HttpPostedFileBase file)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file.InputStream);
            imageByte = rdr.ReadBytes((int)file.ContentLength);
            return imageByte;
        }
        public ActionResult _butonlar()
        {
            if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
            {
                return View();
            }
            else
            {
                return null;
            }



        }



        public ActionResult AdminLTE()


        {

            if (Session["Role"] != null)
            {


                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
                    ViewBag.ArizaTurleri = cacheFonsiyon.ArizaGetir();
                    ViewBag.Personeller = cacheFonsiyon.PersonelGetir();
                    ViewBag.TeknikDurum = cacheFonsiyon.DurumGetir();


                    

                    if (Session["Role"].ToString() == "Admin")
                    {
                        ViewBag.FirmalarBAG = cacheFonsiyon.FirmaGetir();
                        ViewBag.HizliKullaniciBAG = KullaniciService.GetAll();
                    }
                    else
                    {
                        var firmafiltrehizli = Convert.ToInt32(Session["KfirmaID"]);
                        ViewBag.HizliKullaniciBAG = kullanicigeneric.GetAll().Where(x => x.FirmaID == firmafiltrehizli);
                    }

                    return View();
                }
                else
                {
                    RedirectToAction("Giris", "Kullanici");
                    return new HttpStatusCodeResult(204);
                }

            }
            else
            {
                RedirectToAction("Giris", "Kullanici");
                return new HttpStatusCodeResult(204);
            }

        }


        public ActionResult SSS()
        {

            if (Session["Role"] != null)
            {


                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
                    return View();
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }

        }

        [HttpPost]
        public JsonResult IslemSil(int id)
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

                    #region Sil

                    islemService.Remove(id);




                    #endregion
                }
                else
                {
                    RedirectToAction("Giris", "Kullanici");
                }

                return Json("");

            }
        }



        public ActionResult NotSil(int id)
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






                    notService.Remove(id);
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



        public ActionResult StatuDegistir(int id)
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

                    #region Degistir(Checkbox)




                    var statu = notService.Get(id);

                    if (statu.YapildiMi == false)
                    {
                        statu.YapildiMi = true;
                    }


                    else
                    {
                        statu.YapildiMi = false;
                    }


                    notService.Update(statu);
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
        public ActionResult StatuDegistirGorev(int id)
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

                    #region Degistir(Checkbox)




                    var statu = gorevservice.Get(id);

                    if (statu.tamamlandimi == false)
                    {
                        statu.tamamlandimi = true;
                    }


                    else
                    {
                        statu.tamamlandimi = false;
                    }


                    gorevservice.Update(statu);
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



        public ActionResult ToDoSil(int id)
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




                    var sil = notService.Remove(id);




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
                    var predicateIS = PredicateBuilder.New<TeknikDetayView>();






                    if (TeknikServisID != 0)
                    {
                        Session["IslemAktarim"] = TeknikServisID;
                    }
                    else
                    {
                        TeknikServisID = Convert.ToInt32(Session["IslemAktarim"]);

                    }


                    predicateIS = predicateIS.And(x => x.TeknikServisID == TeknikServisID);









                    var liste = islemgeneric.GetAll(predicateIS);


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.IslemServisID, x.IslemAdi, x.IslemServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }
        public ActionResult IslemListesi(int IslemServisID = 0, int TeknikServisID = 0)

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
                        Session["IslemAktarim"] = TeknikServisID;
                    }
                    else
                    {
                        TeknikServisID = Convert.ToInt32(Session["IslemAktarim"]);

                    }


                    predicateislem = predicateislem.And(x => x.TeknikServisID == TeknikServisID);









                    var liste = islemgeneric.GetAll(predicateislem);


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.IslemServisID, x.IslemAdi, x.IslemServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }



        public ActionResult TeknikListesiDB(string TeknikServisID = "", int islemno = 0)
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {


                    var predicate = PredicateBuilder.True<TeknikView>();
                    var OnayID = durumgeneric.GetAll().Where(y => y.DurumAdi.Contains("Onay Bekliyor")).ToList()[0].DurumID;

                  
                    predicate = predicate.And(x => x.SilinmisMi != true);



            

                  
                    var filtredepartman = 0;
                    if (Session["DepartmanID"] != null)
                    {
                        filtredepartman = Convert.ToInt32(Session["DepartmanID"]);
                    }

                    var filtrefirma = Convert.ToInt32(Session["KfirmaID"]);
                    var filtrekullanici = Convert.ToInt32(Session["KullaniciID"]);

                    if (islemno == 1) {
                        predicate = predicate.And(x => x.Aktif == true);
                        Session["OnayIslemi"] = "Evet";
                        if (Session["Role"].ToString() == "Firma")
                        {

                            predicate = predicate.And(x => x.AitOlduguFirmaID==filtrefirma);
                            if (filtredepartman != 1)
                                predicate = predicate.And(x => x.DepartmanID==(filtredepartman));
                        }
                        else if (Session["Role"].ToString() == "FirmaPersonel")
                        {
                            Session["OnayIslemi"] = "Hayır";
                            predicate = predicate.And(x => x.AitOlduguKullaniciID == filtrekullanici);
                        }

                        
                        predicate = predicate.And(x => x.DurumID==OnayID);

                    }


                    if (islemno == 2)
                    {
                        Session["OnayIslemi"] = "Hayır";
                        if (Session["Role"].ToString() == "FirmaPersonel")
                        {

                            predicate = predicate.And(x => x.AitOlduguKullaniciID == filtrekullanici);



                        }

                        if (Session["Role"].ToString() == "Personel")

                            predicate = predicate.And(x => x.AtanmisPersonelID==(filtrekullanici));



                        if (Session["Role"].ToString() == "Firma")
                        {


                            predicate = predicate.And(x => x.AitOlduguFirmaID==filtrefirma);
                            var yonetim =DepartmanService.GetAll().Where(x => x.FirmaID == filtrefirma && x.DepartmanAdi == "YÖNETİM").ToList();
                            if(yonetim.Count()!=0)
                            {
                                if (filtredepartman != (yonetim[0].DepartmanID))
                                    {
                                    predicate = predicate.And(x => x.DepartmanID == (filtredepartman));
                                }
                               

                            }








                        }

                    }






                    var liste = teknikgeneric.GetAll(predicate);



                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeknikServisID, x.TeknikAdi, x.TeknikServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }



        public ActionResult TeknikKarti(int TeknikServisID = 0, string ArizaTuru = "")
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {

                    ViewBag.ArizaTurleri = cacheFonsiyon.ArizaGetir();
                    ViewBag.Personeller = cacheFonsiyon.PersonelGetir();
                    ViewBag.TeknikDurum = cacheFonsiyon.DurumGetir();

                    var teknik = new TeknikView();

                    if (TeknikServisID != 0)
                    {
                        Session["YeniKayitMi"] = "Hayır";
                        teknik = teknikgeneric.Get(TeknikServisID);
                        Session["IslemAktarim"] = TeknikServisID;
                        Session["AitOlduguFirma"] = teknik.AitOlduguFirma;
                        if (teknik.ParaBirimi == "?")
                            teknik.ParaBirimi = teknik.ParaBirimi.Replace("?", "₺");
                        teknik.OdenecekTutar = teknik.Fiyat.ToString() + teknik.ParaBirimi;
                    }
                    else
                    {

                        Session["YeniKayitMi"] = "Evet";
                    }
                    teknik.AtanmisPersonelID = kullanicigeneric.GetAll().Where(y => y.AdSoyad.Contains("Henüz Atanmamış")).ToList()[0].KullaniciID; 

                    return View(teknik);
                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult YapilacakEkleModal(int NotID)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {

                    var Not = new Not();

                    if (NotID != 0)
                        Not = notService.Get(NotID);

                    return View(Not);
                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }








        [HttpPost]
        public JsonResult TeknikKarti(TeknikView stk)
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
                        if (stk.TeknikServisID == 0)
                        {

                            #region Kaydet
                            Session["YeniKayitMi"] = "Evet";

                            if (Session["Role"].ToString() != "Admin")
                            {
                                if(Session["Role"].ToString()=="Firma")
                                {
                                    stk.DurumID = durumgeneric.GetAll().Where(y => y.DurumAdi.Contains("İşlemde")).ToList()[0].DurumID;
                                }
                                else
                                {
                                    stk.DurumID = durumgeneric.GetAll().Where(y => y.DurumAdi.Contains("Onay Bekliyor")).ToList()[0].DurumID;
                                }



                                var filtrekullaniciid = Convert.ToInt32(Session["KullaniciID"]);
                                stk.BolumID = Convert.ToInt32(Session["BolumID"]);
                                stk.AltDepartmanID = Convert.ToInt32(Session["AltDepartmanID"]);
                                stk.DepartmanID = Convert.ToInt32(Session["DepartmanID"]);
                                stk.AitOlduguKullaniciID = filtrekullaniciid;
                                stk.AitOlduguFirmaID = Convert.ToInt32(Session["KfirmaID"]);
                                if (Session["DepartmanID"] != null)
                                {
                                    stk.DepartmanID = Convert.ToInt32(Session["DepartmanID"]);
                                    stk.AltDepartmanID = Convert.ToInt32(Session["AltDepartmanID"]);
                                    stk.BolumID = Convert.ToInt32(Session["BolumID"]);
                                    stk.mailgonderilsinmi = true;
                                    var kulfiltre = PredicateBuilder.True<KullaniciView>();
                                    kulfiltre = kulfiltre.And(x => x.DepartmanID == stk.DepartmanID);
                                    kulfiltre = kulfiltre.And(x => x.Yetkiler == "Firma");
                                    kulfiltre = kulfiltre.And(x => x.FirmaID == stk.AitOlduguFirmaID);
                                  
                                    var olkullanici = kullanicigeneric.GetAll(kulfiltre);

                                    if(olkullanici == null)
                                    {
                                        var kulfiltreadmin = PredicateBuilder.True<KullaniciView>();
                                        kulfiltreadmin = kulfiltreadmin.And(x => x.Yetkiler == "Firma");
                                        kulfiltreadmin = kulfiltreadmin.And(x => x.FirmaID == stk.AitOlduguFirmaID);

                                        olkullanici = kullanicigeneric.GetAll(kulfiltreadmin);
                                    }
                                    var gonderilecekeposta = olkullanici[0].EMail;


                                    var konu = "MTX-KURUMSAL DESTEK : DESTEK TALEBİ BİLDİRİMİ";




                                    string icerik = System.IO.File.ReadAllText(Server.MapPath(@"~/Content/mailbildirim.cshtml"));

                                    icerik = icerik.Replace("mtxolusturankullanici", Session["KullaniciAdi"].ToString());
                                    icerik = icerik.Replace("mtxdestekkategorisi", stk.ArizaTuru);
                                    icerik = icerik.Replace("mtxdestekicerigi", stk.MusteriBeyani);




                                    //EPostaGonder(icerik, gonderilecekeposta, konu);





                                    //var admineposta = "team@mtxsoft.net";
                                    //        EPostaGonder(icerik, admineposta, konu);
                                 

                                


                                }
                            }

                            else
                            {

                                stk.DurumID = durumgeneric.GetAll().Where(y => y.DurumAdi.Contains("İşlemde")).ToList()[0].DurumID;
                               
                                var olkullanici = kullanicigeneric.Get(stk.AitOlduguKullaniciID);
                                stk.DepartmanID = olkullanici.DepartmanID;
                                stk.AitOlduguFirmaID = olkullanici.FirmaID;
                                stk.AltDepartmanID = olkullanici.AltDepartmanID;
                                stk.BolumID = olkullanici.BolumID;
                                var gonderilecekeposta = "halisturk@mtxsoft.net";
;                                if (stk.mailgonderilsinmi == true)
                                {
                                    var kulfiltre2 = PredicateBuilder.True<KullaniciView>();
                                    kulfiltre2 = kulfiltre2.And(x => x.DepartmanID == stk.DepartmanID);
                                    kulfiltre2 = kulfiltre2.And(x => x.Yetkiler == "Firma");
                                    kulfiltre2 = kulfiltre2.And(x => x.FirmaID == stk.AitOlduguFirmaID);
                                    var olkullaniciadmin = kullanicigeneric.GetAll(kulfiltre2);
                                    if(olkullaniciadmin.Count!=0)
                                            {
                                       gonderilecekeposta = olkullaniciadmin[0].EMail;
                                    }
                                   
                                 else
                                    {
                                        var kulfiltreadmin = PredicateBuilder.True<KullaniciView>();
                                        kulfiltreadmin = kulfiltreadmin.And(x => x.Yetkiler == "Firma");
                                        kulfiltreadmin = kulfiltreadmin.And(x => x.FirmaID == stk.AitOlduguFirmaID);
                                        gonderilecekeposta = kullanicigeneric.GetAll(kulfiltreadmin)[0].EMail ;
                                    }
                                  
                                    var konu = "MTX-KURUMSAL DESTEK : DESTEK TALEBİ BİLDİRİMİ";




                                    string icerik = System.IO.File.ReadAllText(Server.MapPath(@"~/Content/mailbildirim.cshtml"));

                                    icerik = icerik.Replace("mtxolusturankullanici", kullanicigeneric.GetAll().Where(y => y.KullaniciID==stk.AitOlduguKullaniciID).ToList()[0].AdSoyad);
                                    icerik = icerik.Replace("mtxdestekkategorisi", stk.ArizaTuru);
                                    icerik = icerik.Replace("mtxdestekicerigi", stk.MusteriBeyani);




                                    //EPostaGonder(icerik, gonderilecekeposta, konu);


                                    //var admineposta = "team@mtxsoft.net";
                                    //EPostaGonder(icerik, admineposta, konu);



                                }
                                else
                                {
                                    stk.mailgonderilsinmi = false;
                                }



                            }




                           
                            stk.SonIslemZamani = DateTime.Now;


                            if (stk.KayitZamani == null || stk.KayitZamani < DateTime.Now)
                            {
                                stk.KayitZamani = DateTime.Now;
                            }


                            stk.Aktif = true;
                            stk.TicketString = new TicketGenerator().TicketOlustur();
                            Teknik mappedstk = Mapper.Map<Teknik>(stk);
                            if(mappedstk.AtanmisPersonelID==0)
                            {
                                mappedstk.AtanmisPersonelID = kullanicigeneric.GetAll().Where(y => y.AdSoyad.Contains("Henüz Atanmamış")).ToList()[0].KullaniciID;
                            }
                           
                          
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
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            #endregion
                        }

                        else
                        {
                            #region Güncelle
                            var nesne = teknikgeneric.Get(stk.TeknikServisID);
                            if (Session["Role"].ToString() == "FirmaPersonel")
                            {
                                var filtrekullaniciid = Convert.ToInt32(Session["KullaniciID"]);
                                nesne.AitOlduguKullaniciID = filtrekullaniciid;
                            }
                            if (stk.Durumu == null)
                            {
                                stk.Durumu = "Onay Bekliyor";
                            }

                            nesne.TeknikServisID = stk.TeknikServisID;
                         
            
                            if (stk.Durumu != null)
                                nesne.Durumu = stk.Durumu;
                     
                            if (stk.CalisanYorumu != null)
                                nesne.CalisanYorumu = stk.CalisanYorumu;
                            if (stk.MusteriBeyani != null)
                                nesne.MusteriBeyani = stk.MusteriBeyani;
                      
                            if (stk.Aktif != null)
                                nesne.Aktif = stk.Aktif;
                            if (stk.Durumu == "Tamamlandı" || stk.Durumu == "Reddedildi")
                            {
                                nesne.Aktif = false;
                            }

                            nesne.MusteriBeyani = stk.MusteriBeyani;
                            if (stk.Fiyat != null)
                                nesne.Fiyat = stk.Fiyat;
                            if (stk.ParaBirimi != null)
                                nesne.ParaBirimi = stk.ParaBirimi;
                            if (stk.OdenecekTutar != null)
                                nesne.OdenecekTutar = stk.OdenecekTutar;
                            if (stk.KapanisTarihi != null)
                                nesne.KapanisTarihi = stk.KapanisTarihi;
             
                            nesne.SonIslemZamani = DateTime.Now;

                            if (stk.Durumu != null)
                            {
                                nesne.Durumu = stk.Durumu;
                            }
                            nesne.Fiyat = stk.Fiyat;
                            if (stk.ArizaTuru != null)
                                nesne.ArizaTuru = stk.ArizaTuru;

                            if (stk.AtanmisPersonelID != null)
                            {
                                nesne.AtanmisPersonelID = stk.AtanmisPersonelID;
                            }
                            if (stk.KayitZamani != null)
                                nesne.KayitZamani = stk.KayitZamani;

                            nesne.SonIslemZamani = DateTime.Now;

                            Session["IslemAktarim"] = nesne.TeknikServisID;
                            Session["YeniKayitMi"] = "Hayır";
                            Teknik mappedstk = Mapper.Map<Teknik>(stk);
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
               
                return Json("");
            }
        }


        [HttpPost]
        public JsonResult NotEkle(Not nt)
        {

            if (Session["Role"] == null)
            {
                RedirectToAction("Giris", "Kullanici");

                return Json("");

            }
            else
            {
                var islem = new IslemSonucModel();

                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {

                    try
                    {

                        #region NotKaydet
                        if (nt.NotID == 0)
                        {
                            nt.FirmaID = Convert.ToInt32(Session["KfirmaID"]);

                            nt.KullaniciID = Convert.ToInt32(Session["KullaniciID"]);
                            nt.OZamani = DateTime.Now.ToString("dd MMM yyyy HH:mm");

                            if (nt.DuyuruMu != true)
                                nt.DuyuruMu = false;






                            var sonuc = notService.Add(nt);
                        }










                        #endregion



                        else
                        {
                            #region NotGüncelle
                            var nesne = genericService4.Get(nt.NotID);


                            nesne.NotID = nt.NotID;
                            if (nt.KullaniciAdi != null)
                            {
                                nesne.KullaniciAdi = nt.KullaniciAdi;
                            }

                            if (nt.DuyuruMu != null)
                            {
                                nesne.DuyuruMu = nt.DuyuruMu;
                            }
                            if (nt.YapilacakMi != null)
                            {
                                nesne.YapilacakMi = nt.YapilacakMi;
                            }
                            if (nt.OZamani != null)
                            {
                                nesne.OZamani = nt.OZamani;
                            }
                            if (nt.YapildiMi != null)
                            {
                                nesne.YapildiMi = nt.YapildiMi;
                            }
                            if (nt.MTXDuyurusuMu != null)
                            {
                                nesne.MTXDuyurusuMu = nt.MTXDuyurusuMu;
                            }
                            if (nt.duyurubasligi != null)
                            {
                                nesne.duyurubasligi = nt.duyurubasligi;
                            }
                            if (nt.onemderecesi != null)
                            {
                                nesne.onemderecesi = nt.onemderecesi;
                            }

                            if (nt.FirmaID != 0)
                            {
                                nesne.FirmaID = nt.FirmaID;
                            }
                            if (nt.NotIcerigi != null)
                            {
                                nesne.NotIcerigi = nt.NotIcerigi;
                            }


















                            var sonucUpdate = notService.Update(nesne);



                            #endregion
                        }
                        
                    }
                    catch (Exception error)
                    {
                        islem = new IslemSonucModel()
                        {
                            IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Hata,
                            Aciklama = "Not İşlemi Sırasında Hata Oluştu.",
                            AciklamaDetay = error.Message,
                            Baslik = IslemSonucuEnum.IslemSonucKodlari.Hata.ToString()
                        };
                        return Json(islem);
                    }

                    return Json(islem);
                }

                else
                {

                    islem = new IslemSonucModel()
                    {
                        IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                        Aciklama = "Not Düzenleme Yada Ekleme Yetkiniz Bulunmuyor...",
                        Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()

                    };
                    return Json(islem);
                }
            }





        }
    




        [HttpPost]
        public JsonResult YapilacakEkle(Not nt)
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


                        #region Kaydet

                        nt.FirmaID = Convert.ToInt32(Session["KfirmaID"]);

                        nt.KullaniciAdi = Session["AdID"].ToString();

                        if (nt.DuyuruMu != true)
                            nt.DuyuruMu = false;

                        nt.OZamani = DateTime.Now.ToString("dd MMM yyyy HH:mm");
                        nt.YapilacakMi = true;
                        var sonuc = notService.Add(nt);


                        if (sonuc != null)
                        {
                            islem = new IslemSonucModel()
                            {
                                IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                Aciklama = "Destek talebi başarıyla oluşturuldu..",
                                Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                Data = sonuc.NotID
                            };
                        }
                        else
                        {
                            islem = new IslemSonucModel()
                            {
                                IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                Aciklama = "Teknik Kaydedilemedi.",
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
                            Aciklama = "Teknik İşlem Sırasında Hata Oluştu.",
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
        public JsonResult TeknikOnayla(int id)
        {
            if (Session["Role"] == null)
            {
                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin"  || Session["Role"].ToString() == "Firma")
                {
                    IslemSonucModel islem;

                    try
                    {
                        if(id!=0)

                        {

                       
                            #region Onayla
                            var nesne = teknikService.Get(id);
                            nesne.DurumID = 3;
                           

                            var sonucUpdate = teknikService.Update(nesne);

                            
                            }
                            #endregion
                        }
                 
                    catch (Exception error)
                    {
                        islem = new IslemSonucModel()
                        {
                            IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Hata,
                            Aciklama = "Onaylama işlemi Sırasında Hata Oluştu.",
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


        [HttpPost]
        public JsonResult TeknikReddet(int id)
        {
            if (Session["Role"] == null)
            {
                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma")
                {
                    IslemSonucModel islem;

                    try
                    {
                        if (id != 0)

                        {


                            #region Onayla
                            var nesne = teknikService.Get(id);
                            nesne.DurumID = 8;


                            var sonucUpdate = teknikService.Update(nesne);


                        }
                        #endregion
                    }

                    catch (Exception error)
                    {
                        islem = new IslemSonucModel()
                        {
                            IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Hata,
                            Aciklama = "Onaylama işlemi Sırasında Hata Oluştu.",
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
        public ActionResult IslemKartiAnasayfa(int IslemServisID = 0, int TeknikServisID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {
                    var islem = new Islem();

                    if (TeknikServisID!=0)
                    {
                        ViewBag.FirmalarBAG = cacheFonsiyon.FirmaGetir();
                        Session["IslemAktarim"] = TeknikServisID;
                   
                        if(Session["AitOlduguFirma"]!=null)
                        {
                        islem.AitOlduguFirma = Session["AitOlduguFirma"].ToString();
                        }

                    }





                    return View(islem);
                }

                return RedirectToAction("Giris", "Kullanici");

            }
        }

        //////public ActionResult GoreviGoruntule(int GorevID = 0)
        //////{
        //////    if (Session["Role"] == null)
        //////    {
        //////        return RedirectToAction("Giris", "Kullanici");
        //////    }
        //////    else
        //////    {
        //////        if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
        //////        {
        //////            var gorev = new GorevView();

        //////            if (GorevID != 0)
        //////            {
             

        //////                gorev = Gorevgeneric.Get(GorevID);
                     
        //////                gorev.KullaniciResim = KullaniciService.Get(gorev.AitOlanKullaniciID).ProfilResmi;

        //////            }





        //////            return View(gorev);
        //////        }

        //////        return RedirectToAction("Giris", "Kullanici");

        //////    }
        //////}


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
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" )
                {
                    IslemSonucModel islem;

                    try
                    {


                        if (stk.IslemServisID == 0)
                        {



                            #region Kaydet

                            stk.AitOlduguFirma = stk.AitOlduguFirma.Remove(stk.AitOlduguFirma.Length - 1, 1);
                            if (stk.KayitZamani == null)
                            {
                                stk.KayitZamani = DateTime.Now;
                            }
                            stk.TeknikServisID = Convert.ToInt32(Session["IslemAktarim"]);
                            stk.Aktif = true;
                            stk.KDVDahilFiyat = stk.GenelToplam + stk.KDVFiyat;
                            stk.TLFiyat = stk.DolarKuru * stk.KDVDahilFiyat;
                        
                            if (Session["KullaniciID"]!=null)
                            {
                                stk.KullaniciID = Convert.ToInt32(Session["KullaniciID"]);
                            }
                           
                            var sonuc = islemService.Add(stk);




                            #region Destek Talebi Fiyat Güncelle
                            var varteknik = teknikService.Get(stk.TeknikServisID);

                            varteknik.Fiyat = (Convert.ToDouble(varteknik.Fiyat) + Convert.ToDouble(stk.KDVDahilFiyat));
                            varteknik.ParaBirimi = stk.ParaBirimi;

                            varteknik.SonIslemZamani = DateTime.Now;
                            teknikService.Update(varteknik);

                            #endregion


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
                            var nesne = islemService.Get(stk.IslemServisID);


                            nesne.IslemServisID = stk.IslemServisID;
                    
                            nesne.ArizaTuru = stk.ArizaTuru;
                            nesne.Fiyat = stk.Fiyat;
                            nesne.IslemAdi = stk.IslemAdi;
                            nesne.IslemBirimID = stk.IslemBirimID;
                            nesne.IslemGrubuID = stk.IslemGrubuID;
                            nesne.CalisanYorumu = stk.CalisanYorumu;

                     
                            nesne.Aktif = stk.Aktif;

                            nesne.Anlasmali = stk.Anlasmali;
                            nesne.YapilanIslem = stk.YapilanIslem;

                            if (stk.YapilanIslem != null)
                            {
                                string.Join(",", stk.YapilanIslem.ToString());
                            }

                            nesne.YapilanIslem = stk.YapilanIslem;

                            var sonucUpdate = islemService.Update(nesne);

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





       









        [HttpPost]
        public JsonResult TeknikSil(int id)
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

                        #region Sil
                        var silinecek = teknikService.Get(id);
                        silinecek.SilinmisMi = true;
                        silinecek.TeknikServisID = id;
                        var sonuc = teknikService.Update(silinecek);


                        if (sonuc != null)
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
                RedirectToAction("Giris", "Kullanici");
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