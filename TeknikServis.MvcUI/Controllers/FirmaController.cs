using LinqKit;
using TeknikServis.Bll;
using TeknikServis.Dal.Concrete.EntityFramework.Repository;
using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using TeknikServis.Interfaces;
using TeknikServis.MvcUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeknikServis.MvcUI.Controllers
{
    [AllowAnonymous]
    



    public class FirmaController : Controller
    {

        IFirmaService firmaService = new FirmaManager(new EfFirmaRepository());
        
        IGenericService<Firma> genericService1 = new GenericManager<Firma>(new EfGenericRepository<Firma>());
        IGenericService<EnvanterView> envantergeneric = new GenericManager<EnvanterView>(new EfGenericRepository<EnvanterView>());
        IGenericService<PocoFirmaListesi> genericService5 = new GenericManager<PocoFirmaListesi>(new EfGenericRepository<PocoFirmaListesi>());
        IEnvanterService envanterService = new EnvanterManager(new EfEnvanterRepository());
        IGenericService<TeknikView> teknikgeneric = new GenericManager<TeknikView>(new EfGenericRepository<TeknikView>());
        IGenericService<FirmaView> firmageneric = new GenericManager<FirmaView>(new EfGenericRepository<FirmaView>());
        IGenericService<Envanter> genericService2 = new GenericManager<Envanter>(new EfGenericRepository<Envanter>());
        CacheFonsiyon cacheFonsiyon;
        public FirmaController()
        {
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
                if (Session["Role"].ToString() == "Admin")
                {
                    ViewBag.Kategoriler = cacheFonsiyon.KategoriGetir();
                    return View();
                }
                return  RedirectToAction("Giris","Kullanici");
            }
        }
        
        public ActionResult EnvanterKarti(string FirmaID = "")
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {

                    ViewBag.EMarkalarBAG = cacheFonsiyon.MarkaGetir();
                    ViewBag.EKullaniciBAG = cacheFonsiyon.KullaniciGetir();
                    ViewBag.EFirmaBAG = cacheFonsiyon.FirmaGetir();
                    ViewBag.EKategoriBAG = cacheFonsiyon.EkategoriGetir();

                    var envanter = new Envanter();
                    if (FirmaID == null)
                    {
                        envanter.Kapsamici = true;
                        envanter.DonanimMi = true;

                    }
                    else
                    {


                    }



           
                    envanter.FirmaID = Convert.ToInt32(Session["EnvanterAktarimFirmaID"]);

                    return View(envanter);
                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }



        public ActionResult EnvanterSilKart(int EnvanterID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {


                    if (EnvanterID != 0)
                    {
                        var envanter = new Envanter();


                        envanter = envanterService.Get(EnvanterID);


                        return View(envanter);

                    }





                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }


        [HttpPost]
        public JsonResult EnvanterSil(int id)
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

                    envanterService.Remove(id);




                    #endregion
                }
                else
                {
                    RedirectToAction("Giris", "Kullanici");
                }

                return Json("");

            }
        }





        [HttpPost]
        public JsonResult EnvanterKarti(Envanter stk)
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
                        if (stk.EnvanterID == 0)
                        {
                            #region Kaydet

                            stk.EkleyenKullaniciID = Convert.ToInt32(Session["KullaniciID"]);
                            stk.FirmaID = Convert.ToInt32(Session["EnvanterAktarimFirmaID"]);
                            var sonuc = envanterService.Add(stk);


                            if (sonuc != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Envanter Başarıyla Kaydedildi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.EnvanterID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Envanter Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            #endregion
                        }

                        else
                        {
                            #region Güncelle
                            var nesne = envanterService.Get(stk.EnvanterID);



                            nesne.EnvanterID = stk.EnvanterID;

                
                            nesne.Aciklama = stk.Aciklama;
               
                            nesne.AnyDesk = stk.AnyDesk;
                            nesne.DonanimMi = stk.DonanimMi;
                            nesne.IP = stk.IP;
                            nesne.MAC = stk.MAC;
                            nesne.IP2 = stk.IP2;
                            nesne.MAC2 = stk.MAC2;
                            nesne.WLANIP = stk.WLANIP;
                            nesne.WLANMAC = stk.WLANMAC;
                            nesne.Marka = stk.Marka;

                            nesne.FirmaID = stk.FirmaID;

                            nesne.Kapsamici = stk.Kapsamici;
                            nesne.OS = stk.OS;
                            nesne.TeamViewer = stk.TeamViewer;
                            nesne.KullaniciID = stk.KullaniciID;





                            var sonucUpdate = envanterService.Update(nesne);

                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Envanter Başarıyla Güncellendi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonucUpdate.EnvanterID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Envanter Güncellenemedi.",

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
                            Aciklama = "Envanter İşlem Sırasında Hata Oluştu.",
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



        public ActionResult EnvanterListesiKart()
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else


            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                    var predicate = PredicateBuilder.True<EnvanterView>();



                    var filtrekullanicienv = Convert.ToInt32(Session["EnvanterAktarimFirmaID"]);

                    predicate = predicate.And(x => x.FirmaID == filtrekullanicienv);






                    var liste = envantergeneric.GetAll(predicate);



                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.EnvanterServisID, x.EnvanterAdi, x.EnvanterServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult FirmaListesi(string FirmaID = "", string firmaAdi = "")
        {
            if (Session["Role"] == null)
            {
                return  RedirectToAction("Giris","Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {
           







                    var liste = firmageneric.GetAll();
                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.FirmaID, x.FirmaAdi, x.FirmaID, x.MusteriTuru, x.AdSoyad, x.CepTelefonu, x.Email });

                    return View(liste.ToList());
                }
                return  RedirectToAction("Giris","Kullanici");
            }
            }


        public ActionResult TedarikciFirmaListesi(string FirmaID = "", string firmaAdi = "")
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {








                    var liste = firmageneric.GetAll().Where(x => x.TedarikciFirmaMi == true);
                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.FirmaID, x.FirmaAdi, x.FirmaID, x.MusteriTuru, x.AdSoyad, x.CepTelefonu, x.Email });

                    return View(liste.ToList());
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }


        
        public ActionResult FirmaKarti(int FirmaID=0)






        {

            if (Session["Role"] == null)
            {
                return  RedirectToAction("Giris","Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    ViewBag.illerBAG = cacheFonsiyon.ilGetir();
                    ViewBag.ilcelerBAG = cacheFonsiyon.ilceGetir();
                    ViewBag.Kategoriler = cacheFonsiyon.KategoriGetir();
                   

                    var firma = new Firma();

                    if (FirmaID != 0)
                    {
                        firma = firmaService.Get(FirmaID);
                        Session["EnvanterAktarimFirmaAdi"] = firma.FirmaAdi;
                        Session["EnvanterAktarimFirmaID"] = firma.FirmaID;
                    }
                       

                    return View(firma);
                }
                return  RedirectToAction("Giris","Kullanici");
            }
        }



        public ActionResult TedarikciFirmaKarti(int FirmaID = 0)


        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    ViewBag.illerBAG = cacheFonsiyon.ilGetir();
                    ViewBag.ilcelerBAG = cacheFonsiyon.ilceGetir();
                    ViewBag.Kategoriler = cacheFonsiyon.KategoriGetir();


                    var firma = new Firma();

                    if (FirmaID != 0)
                    {
                        firma = firmaService.Get(FirmaID);
                        Session["EnvanterAktarimFirmaAdi"] = firma.FirmaAdi;
                        Session["EnvanterAktarimFirmaID"] = firma.FirmaID;
                    }

                    firma.TedarikciFirmaMi = true;
                    return View(firma);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }
        [HttpPost]
        public JsonResult FirmaKarti(Firma stk)
        {

            if (Session["Role"] == null)
            {
                
                 RedirectToAction("Giris","Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    IslemSonucModel islem;
                    try
                    {
                        if (stk.FirmaID == 0)
                        {
                            #region Kaydet
                            stk.KayitZamani = DateTime.Now;
                            stk.BarkodNo = new TicketGenerator().FirmaTicketOlustur();
                            var sonuc = firmaService.Add(stk);

                            if (sonuc != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Firma Başarıyla Kaydedildi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.FirmaID
                                };
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Firma Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            #endregion
                        }
                        else
                        {
                            #region Güncelle
                            var nesne = firmaService.Get(stk.FirmaID);
                            if(nesne.BarkodNo==null)
                            {
                                nesne.BarkodNo = new TicketGenerator().FirmaTicketOlustur();
                            }
                            nesne.FirmaID = stk.FirmaID;
                            nesne.MusteriTuru = stk.MusteriTuru;
                            nesne.FirmaAdi = stk.FirmaAdi;
                            nesne.FirmaBirimID = stk.FirmaBirimID;
                            nesne.FirmaGrubuID = stk.FirmaGrubuID;
                            nesne.AdSoyad = stk.AdSoyad;
                            nesne.Email = stk.Email;
                            nesne.Notlar = stk.Notlar;
                            nesne.Aktif = stk.Aktif;
                            nesne.Adres = stk.Adres;
                            nesne.CepTelefonu = stk.CepTelefonu;
                            nesne.VergiKimlikNo = stk.VergiKimlikNo;
                            nesne.ilID = stk.ilID;
                            nesne.ilceID = stk.ilceID;
                            nesne.TedarikciFirmaMi = stk.TedarikciFirmaMi;

                            nesne.vergidairesi = stk.vergidairesi;
                            var sonucUpdate = firmaService.Update(nesne);


                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Firma Başarıyla Güncellendi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonucUpdate.FirmaID

                                };
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Firma Güncellenemedi.",
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
                            Aciklama = "Firma İşlem Sırasında Hata Oluştu.",
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

        public ActionResult FirmaSil(int FirmaID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {


                    if (FirmaID != 0)
                    {
                        var firma = new Firma();


                        firma = firmaService.Get(FirmaID);

                        return View(firma);

                    }





                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }

        [HttpGet]
        public ActionResult ilcegetir(int ilID=0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {

   
                    var butunilceler = new List<ilce>();

                    if (ilID != 0)
                    {



                       butunilceler = (List<ilce>)cacheFonsiyon.ilceGetir();
                       
                            
                            var filtreilce = butunilceler.Where(x => x.IlID == ilID);
                        
                        return Json(filtreilce.ToList(),JsonRequestBehavior.AllowGet);

                    }





                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }







        public ActionResult DestekListesi(int FirmaID)
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                    var predicate = PredicateBuilder.New<TeknikView>();

                    predicate = predicate.And(x => x.SilinmisMi != true);


                    predicate = predicate.And(x => x.AitOlduguFirmaID == FirmaID);



                    var liste = teknikgeneric.GetAll(predicate);

                   

                    Session["FirmaAdi"] = firmaService.Get(FirmaID).FirmaAdi;

                   

                    return View(liste.ToList());
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }





        [HttpPost]
        public JsonResult FirmaSil(Firma stk)
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
                        if (stk.FirmaID != 0)
                        {
                            #region Güncelle
                            var nesne = firmaService.Get(stk.FirmaID);




                            nesne.Aktif = false;







                            var sonucUpdate = firmaService.Update(nesne);

                            if (sonucUpdate != null)
                            {
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Firma başarıyla silindi..",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonucUpdate.FirmaID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Firma Silinemedi!.",
                                    Data = sonucUpdate.FirmaID,
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




        
    }
}