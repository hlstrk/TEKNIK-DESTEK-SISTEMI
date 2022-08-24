using LinqKit;
using TeknikServis.Bll;
using TeknikServis.Dal.Concrete.EntityFramework.Repository;
using TeknikServis.Entittes.Models;
using TeknikServis.Interfaces;
using TeknikServis.MvcUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Mail;
using System.Drawing;
using AutoMapper;

namespace TeknikServis.MvcUI.Controllers
{
    [AllowAnonymous]

    public class KullaniciController : Controller
    {
        IKullaniciService KullaniciService = new KullaniciManager(new EfKullaniciRepository());

        IGenericService<KullaniciView> kullanicigeneric = new GenericManager<KullaniciView>(new EfGenericRepository<KullaniciView>());
     
        CacheFonsiyon cacheFonsiyon;
        IFirmaService FirmaService;
        
        IEnvanterService envanterService = new EnvanterManager(new EfEnvanterRepository());
              IGenericService<Departmanlar> DepartmanService = new GenericManager<Departmanlar>(new EfGenericRepository<Departmanlar>());

        IGenericService<AltDepartmanlar> AltDepartmanService = new GenericManager<AltDepartmanlar>(new EfGenericRepository<AltDepartmanlar>());

        IGenericService<Bolumler> BolumlerService = new GenericManager<Bolumler>(new EfGenericRepository<Bolumler>());
        IGenericService<Envanter> genericService1 = new GenericManager<Envanter>(new EfGenericRepository<Envanter>());
        IGenericService<EnvanterView> envantergeneric = new GenericManager<EnvanterView>(new EfGenericRepository<EnvanterView>());
        CacheFonsiyon fonksiyon = new CacheFonsiyon();


        public KullaniciController(IKullaniciService kullaniciService, IFirmaService firmaService)
        {
            KullaniciService = kullaniciService;
            FirmaService = firmaService;
            cacheFonsiyon = new CacheFonsiyon();


        }


        public ActionResult Index()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma")
                {

                    return View();


                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }



        public ActionResult KullaniciKarti(int KullaniciID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma")
                {

                    var predicate = PredicateBuilder.New<KullaniciView>();


                    ViewBag.FirmalarBAG = cacheFonsiyon.FirmaGetir();
                

                    var kullanici = new Kullanici();

                    if (KullaniciID != 0)

                    {
                        kullanici = KullaniciService.Get(KullaniciID);
                        if(kullanici.FirmaID != 0)
                        {
                            ViewBag.Departman = cacheFonsiyon.DepartmanGetir(kullanici.FirmaID);
                        }
                        else
                        {
                            ViewBag.Departman = cacheFonsiyon.DepartmanGetir(0);
                        }
       
                        if(kullanici.AltDepartmanID != 0)
                        {
                            ViewBag.AltDepartman = cacheFonsiyon.AltDepartmanGetir(kullanici.AltDepartmanID);
                        }
                        else
                        {
                            ViewBag.AltDepartman = cacheFonsiyon.AltDepartmanGetir(0);
                        }
                        if (kullanici.BolumID != 0)
                        {
                            ViewBag.Bolum = cacheFonsiyon.BolumGetir(kullanici.BolumID);
                        }
                        else
                        {
                            ViewBag.Bolum = cacheFonsiyon.BolumGetir(0);
                        }

                        ViewBag.Departmanlar = cacheFonsiyon.DepartmanGetir(kullanici.FirmaID);

                        Session["EnvanterAktarimID"] = kullanici.KullaniciID;

                       
                        if (kullanici.FirmaID != 0)
                            Session["FirmaIDKul"] = kullanici.FirmaID;
                    }

                    else
                    {
                        kullanici.FirmaID = 1;
                        ViewBag.Departmanlar = cacheFonsiyon.DepartmanGetir(0);
                        ViewBag.AltDepartman = cacheFonsiyon.AltDepartmanGetir(0);
                        ViewBag.Bolum = cacheFonsiyon.BolumGetir(0);

                    }
                    kullanici.KullaniciParola = null;
                   
                   

                    return View(kullanici);
                }
                else
                {
                    return RedirectToAction("Giris", "Kullanici");
                }
            }
        }
        [HttpPost]
        public JsonResult KullaniciKarti(Kullanici stk)
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
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma")
                {
                    IslemSonucModel islem;
                    try
                    {
                        if (stk.KullaniciID == 0)
                        {
                            #region Kaydet
                            stk.KayitZamani = DateTime.Now;
                            if (stk.KullaniciParola != null)
                            {
                                stk.KullaniciParola = new ToPasswordRepository().Md5(stk.KullaniciParola);
                                stk.Sifre = stk.KullaniciParola;
                            }



                           

                           





                    


                            stk.SilinmisMi = false;
                            var sonuc = KullaniciService.Add(stk);



                            if (sonuc != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kullanici Başarıyla Kaydedildi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.KullaniciID
                                };
                                fonksiyon.CacheTemizle();
                                fonksiyon.CacheOlustur();
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kullanici Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }

                            #endregion
                        }

                        else
                        {
                            #region Güncelle
                            var nesne = KullaniciService.Get(stk.KullaniciID);
                            if (stk.KullaniciParola != null)
                            {
                                nesne.KullaniciParola = new ToPasswordRepository().Md5(stk.KullaniciParola);
                                nesne.Sifre = nesne.KullaniciParola;
                            }
                            else
                            {
                                nesne.KullaniciParola = nesne.Sifre;
                            }

                            nesne.KullaniciID = stk.KullaniciID;

                            if(nesne.KullaniciAdi!=null)
                            nesne.KullaniciAdi = stk.KullaniciAdi;

                            if (nesne.Yetkiler != null)
                                nesne.Yetkiler = stk.Yetkiler;

                            if (nesne.AdSoyad != null)
                                nesne.AdSoyad = stk.AdSoyad;

                            if (nesne.EMail != null)
                                nesne.EMail = stk.EMail;

                            if (nesne.CepTelNo != null)
                                nesne.CepTelNo = stk.CepTelNo;

                            if (nesne.DepartmanID != 0)
                                nesne.DepartmanID = stk.DepartmanID;

                            if (nesne.FirmaID != 0)
                                nesne.FirmaID = stk.FirmaID;

                            if (nesne.BolumID != 0)
                                nesne.BolumID = stk.BolumID;

                            if (nesne.AltDepartmanID != 0)
                                nesne.AltDepartmanID = stk.AltDepartmanID;

                            nesne.Aktif = stk.Aktif;


                            if(nesne.Aktif==true)
                            {
                                nesne.SilinmisMi = false;
                            }














                            var sonucUpdate = KullaniciService.Update(nesne);

                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kullanici Başarıyla Güncellendi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonucUpdate.KullaniciID
                                };
                                fonksiyon.CacheTemizle();
                                fonksiyon.CacheOlustur();
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kullanici Güncellenemedi.",
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
                            Aciklama = "Kullanici İşlem Sırasında Hata Oluştu.",
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


        public ActionResult KullaniciGeriAl(int KullaniciID)
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




                    var kullanici = KullaniciService.Get(KullaniciID);

                    kullanici.Aktif = true;
                    kullanici.SilinmisMi = false;

                    KullaniciService.Update(kullanici);



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

        public ActionResult DepartmanGetir(int FirmaID=0)
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





                       var butundepartmanlar = DepartmanService.GetAll().Where(x => x.FirmaID == FirmaID).ToList();

                        return Json(butundepartmanlar);

                    }





                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }



        [HttpPost]

        public ActionResult AltDepartmanGetir(int DepartmanID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {




                    if (DepartmanID != 0)
                    {





                        var butundepartmanlar = AltDepartmanService.GetAll().Where(x => x.DepartmanID == DepartmanID).ToList();

                        return Json(butundepartmanlar);

                    }





                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }





        [HttpPost]

        public ActionResult BolumGetir(int AltDepartmanID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {




                    if (AltDepartmanID != 0)
                    {





                        var butundepartmanlar = BolumlerService.GetAll().Where(x => x.AltDepartmanID == AltDepartmanID).ToList();

                        return Json(butundepartmanlar);

                    }





                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }


        public ActionResult EnvanterKarti(string KullaniciID = "")
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
                    if (KullaniciID == null)
                    {
                        envanter.Kapsamici = true;
                        envanter.DonanimMi = true;

                    }
                    else
                    {


                    }



                    envanter.KullaniciID = Convert.ToInt32(Session["EnvanterAktarimID"]);
          
                    envanter.FirmaID = Convert.ToInt32(Session["FirmaIDKul"]);
       

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


    
        public ActionResult EnvanterSil(int id)
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

                    #region Sil

                   var RESULT=  envanterService.Remove(id);


                    return View(RESULT);

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

        [HttpPost]
        public JsonResult EnvanterSil(Envanter env)
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

                        #region Sil
                        var sonuc = envanterService.Remove(env.EnvanterID);

                        if (sonuc)
                        {
                            islem = new IslemSonucModel()
                            {
                                IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                Aciklama = "Envanter Başarıyla Silindi.",
                                Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString()
                            };
                        }
                        else
                        {
                            islem = new IslemSonucModel()
                            {
                                IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                Aciklama = "Envanter Silinemedi.",
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
                            Aciklama = "Envanter Silme Sırasında Hata Oluştu.",
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


         
                    var filtrekullanicienv = Convert.ToInt32(Session["EnvanterAktarimID"]);

                    predicate = predicate.And(x => x.KullaniciID == filtrekullanicienv);






                    var liste = envantergeneric.GetAll(predicate);
                  


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.EnvanterServisID, x.EnvanterAdi, x.EnvanterServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult SilinenKullaniciListesi()
        {
            if (Session["Role"] == null)
            {



                return RedirectToAction("Giris", "Kullanici");
            }

            if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma")
            {
                var predicate = PredicateBuilder.True<KullaniciView>();



                var filtrefirma = Convert.ToInt32(Session["KfirmaID"]);

                if (Session["Role"].ToString() == "Firma")
                    predicate = predicate.And(x => x.FirmaID == filtrefirma);



                predicate = predicate.And(x => x.SilinmisMi == true);
                var liste = kullanicigeneric.GetAll(predicate);
                //var liste = genericService1.GetAllSelect(predicate, x => new { x.KullaniciServisID, x.KullaniciAdi, x.KullaniciServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                return View(liste);
            }
            else
            {
                IslemSonucModel islem;
                islem = new IslemSonucModel()
                {
                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                    Aciklama = "Kullanıcı Listeleme Yetkiniz Bulunmuyor...",
                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()

                };
                return Json(islem);
            }

        }


        public ActionResult KullaniciListesi(string KullaniciID = "", string AdSoyad = "")
        {
            if (Session["Role"] == null)
            {



                return RedirectToAction("Giris", "Kullanici");
            }

            if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma")
            {
                var predicate = PredicateBuilder.True<KullaniciView>();

                predicate = predicate.And(x => x.SilinmisMi != true);

                var filtrefirma = Convert.ToInt32(Session["KfirmaID"]);

                if (Session["Role"].ToString() == "Firma")
                    predicate = predicate.And(x => x.FirmaID==filtrefirma);

                var liste = kullanicigeneric.GetAll(predicate);
                //var liste = genericService1.GetAllSelect(predicate, x => new { x.KullaniciServisID, x.KullaniciAdi, x.KullaniciServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                return View(liste);
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
        [AllowAnonymous]
        public ActionResult Giris()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Giris2()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public ActionResult Giris(ViewKullanici kullanici)

        {
            try
            {
                if (ModelState.IsValid)
                {
                    var kul = KullaniciService.KullaniciGiris(kullanici.KullaniciAdi, kullanici.Parola);



                    if (kul != null&&kul.Aktif==true)
                    {





                        
                        var liste = KullaniciService.Get(kul.KullaniciID);
                        if(liste.KullaniciParola==null)
                        {
                            liste.KullaniciParola = liste.Sifre;
                        }
                        liste.EnSonGirisZamani = DateTime.Now;
                        KullaniciService.Update(liste);
                        Session["KullaniciAdi"] = kul.AdSoyad;
                        Session["KullaniciID"] = kul.KullaniciID;
                        Session["AdID"] = kul.KullaniciAdi;
                        


                        if (kul.Yetkiler!=null)
                        {
                            Session["Role"] = kul.Yetkiler;
                        }
                        if (liste.FirmaID != 0)
                        {
                            Session["KfirmaID"] = liste.FirmaID;
                        }







                        if (liste.DepartmanID != 0)
                        {
                            Session["DepartmanID"] = liste.DepartmanID;
                        }
                        if (liste.AltDepartmanID != 0)
                        {
                            Session["AltDepartmanID"] = liste.AltDepartmanID;
                        }
                        if (liste.BolumID != 0)
                        {
                            Session["BolumID"] = liste.BolumID;
                        }
                        return RedirectToAction("Index", "Anasayfa");



                    }
                    else
                    {
                        
                            throw new Exception("Hesabınız Aktif Değil.");

                            
                       
                     

                    }
                }
                else
                {
                    
                    return View(kullanici);
                    
                }

            }
            catch (Exception error)
            {
                ModelState.AddModelError("", error.Message);

                return View(kullanici);
            }

            return View(kullanici);
        }


        public ActionResult Listele()
        {
            if (Session["Role"] == null)
            {

                return RedirectToAction("Giris", "Kullanici");


            }
            else
            {

                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma")
                {
                    return View();
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


        public ActionResult ListelePartial(string KullaniciID = "")
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");

            }
            else
            {

                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma")
                {
                    return View(kullanicigeneric.GetAll());




                    var predicate = PredicateBuilder.New<KullaniciView>();


                    var kid = Convert.ToInt32(KullaniciID == "" ? "0" : KullaniciID);

                    if (kid != 0)
                        predicate = predicate.And(x => x.KullaniciID == kid);



                    var liste = kullanicigeneric.GetAll(predicate);
                    Session["KacKullanici"] = liste.Count().ToString();


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
        public ActionResult Kaydet()
        {


            return View();

        }

        [HttpPost]
        public ActionResult Kaydet(Kullanici kullanici)
        {

            kullanici.KullaniciParola = new ToPasswordRepository().Md5(kullanici.KullaniciParola);
            return View(KullaniciService.Add(kullanici));



        }

        

       
      

        [HttpGet]

        public ActionResult Cikis(ViewKullanici kullanici)

        {
            Session.Abandon();
            var Sezon = Session["Kullanici"];
            try
            {
                if (Sezon == null)
                {

                    return RedirectToAction("Giris", "Kullanici");

                }
                else
                {
                    return RedirectToAction("Giris", "Kullanici");
                }

            }
            catch (Exception error)
            {
                ModelState.AddModelError("", error.Message);

                return View(kullanici);
            }


        }











        public ActionResult SifreSifirla(int KullaniciID = 0)
        {


           

            return View();



        }
        [HttpPost]
        public JsonResult SifreSifirla(Kullanici stk)
        {

         
           var predicate= PredicateBuilder.New<KullaniciView>();





            if (!string.IsNullOrEmpty(stk.EMail))
                predicate = predicate.And(x => x.EMail.Contains(stk.EMail));
            
          
            

            var liste = kullanicigeneric.GetAll(predicate);

            if(liste.Count!=0)

            {
                var ks = liste[0].KullaniciID.ToString();

                var epostahash= new ToPasswordRepository().HashSifrele(ks);

                stk.KullaniciID = Convert.ToInt32(ks);
                stk.KullaniciAdi = liste[0].KullaniciAdi;
                stk.Yetkiler = liste[0].Yetkiler;
                stk.AdSoyad = liste[0].AdSoyad;
                stk.FirmaID = liste[0].FirmaID;
                stk.SifreSifirlaToken = epostahash;
                stk.Aktif=liste[0].Aktif;   
                stk.CepTelNo=liste[0].CepTelNo; 
                stk.EMail=liste[0].EMail;   
                stk.DepartmanID=liste[0].DepartmanID;   
                stk.FirmaID=liste[0].FirmaID; 
                stk.ProfilResmi=liste[0].ProfilResmi;   
              
                if(stk.KullaniciParola == null)
                {
                    stk.KullaniciParola = liste[0].Sifre; 
                }
                stk.KullaniciParola = liste[0].KullaniciParola; 



                




                KullaniciService.Update(stk);

                var gonderilecekeposta = liste[0].EMail;
        

                var konu = "MTX-KURUMSAL DESTEK : ŞİFRE SIFIRLAMA TALEBİ";

          


                string icerik = System.IO.File.ReadAllText(Server.MapPath(@"~/Content/mail.cshtml"));

                icerik = icerik.Replace("mtxsoftsifresifirlamatokeni", epostahash);
             
           
                EPostaGonder2(icerik,gonderilecekeposta,konu);


            }

          
         
          
          


            return Json("");






        }


        public ActionResult SifremiUnuttum(string id="")
        {
            if(id!="")
            {
                var predicate = PredicateBuilder.New<KullaniciView>();
              
                if (!string.IsNullOrEmpty(id))
                    predicate = predicate.And(x => x.SifreSifirlaToken.Contains(id));


              

                var liste = kullanicigeneric.GetAll(predicate);

                if (liste.Count == 1)
                {

                    var kullanici = new ToPasswordRepository().HashCoz(id);

                    Session["SifirlanacakSifreID"] = kullanici;

                }
               
               



        
              
                return View();
            }


            return RedirectToAction("Giris", "Kullanici");






        }


        [HttpPost]
        public JsonResult SifremiUnuttum(Kullanici stk)
        {

            IslemSonucModel islem;
            try
            {
                if (Session["SifirlanacakSifreID"] != null)
                {
                    var SifirlanacakID = Session["SifirlanacakSifreID"].ToString();
                    var id = Convert.ToInt32(SifirlanacakID);
                    stk.KullaniciID = id;

                    var nesne = KullaniciService.Get(stk.KullaniciID);
                    nesne.KullaniciParola = new ToPasswordRepository().Md5(stk.KullaniciParola);
                    nesne.Sifre = nesne.KullaniciParola;
                    nesne.SifreSifirlaToken = null;



                    var sonucUpdate = KullaniciService.Update(nesne);
                    Session.Clear();
                    
                    if (sonucUpdate != null)
                    {
                        islem = new IslemSonucModel()
                        {
                            IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                            Aciklama = "Şifreniz başarıyla sıfırlandı. Giriş Ypabilirsiniz.",
                            Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()

                        };
                    }
                    else
                    {
                        islem = new IslemSonucModel()
                        {
                            IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                            Aciklama = "Bağlantının Süresi Dolmuş!",
                            Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()

                        };
                    }
                    return Json(islem);
                

                     

                }

                else
                {
                    islem = new IslemSonucModel()
                    {
                        IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                        Aciklama = "İsteğin Süresi dolmuş!.",
                        Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                    };
                    return Json(islem);
                }




                #region Güncelle


                #endregion

            }
            catch (Exception error)
            {
                islem = new IslemSonucModel()
                {
                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Hata,
                    Aciklama = "Kullanici İşlem Sırasında Hata Oluştu.",
                    AciklamaDetay = error.Message,
                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Hata.ToString()
                };
                return Json(islem);
            }


        }














      



        [HttpPost]
        public ActionResult EPostaGonder2(string body, string receiver,string subject)
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
        public ActionResult EPostaGonder(string receiver, string subject, string message)
        {
            try
            {

                var _host = "smtp.yandex.com";
                var _port = 587;
                var _defaultCredentials = false;
                var _enableSsl = true;
                var _emailfrom = "bilgi@mtxsoft.net";//Your yandex email adress
                var _password = "jwawuopddeieayrd";//Your yandex app password
                using (var smtpClient = new SmtpClient(_host, _port))
                {
                    smtpClient.EnableSsl = _enableSsl;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = _defaultCredentials;
               
                    if (_defaultCredentials == false)
                    {
                        smtpClient.Credentials = new NetworkCredential(_emailfrom, _password);
                    }
               
                    smtpClient.Send(_emailfrom, receiver, subject, message);
                }
                return View();
                }
            
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }




        

        public ActionResult KullaniciSil(int KullaniciID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {


                    if (KullaniciID != 0)
                    {
                        var Kullanici = new KullaniciView();


                        Kullanici = kullanicigeneric.Get(KullaniciID);


                        return View(Kullanici);

                    }





                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }
















        [HttpPost]
        public JsonResult KullaniciSil(Kullanici stk)
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
                        if (stk.KullaniciID != 0)
                        {
                            #region Güncelle
                            var nesne = KullaniciService.Get(stk.KullaniciID);




                            nesne.Aktif = false;

                            nesne.SilinmisMi = true;







                            var sonucUpdate = KullaniciService.Update(nesne);

                            if (sonucUpdate != null)
                            {
                                fonksiyon.CacheTemizle();
                                fonksiyon.CacheOlustur();
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kullanıcı başarıyla silindi..",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonucUpdate.KullaniciID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = " Kullanıcı Silinemedi!.",
                                    Data = sonucUpdate.KullaniciID,
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
                            Aciklama = " Kullanıcı Silme Sırasında Hata Oluştu.",
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



        //[HttpPost]
        //public JsonResult SifreDegis(Kullanici stk)
        //{
        //    if (Session["Role"] == null)
        //    {
        //        RedirectToAction("Giris", "Kullanici");

        //        IslemSonucModel islem;
        //        islem = new IslemSonucModel()
        //        {
        //            IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
        //            Aciklama = "Kullanıcı Düzenleme Yada Ekleme Yetkiniz Bulunmuyor...",
        //            Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()

        //        };
        //        return Json(islem, JsonRequestBehavior.AllowGet);

        //    }
        //    else
        //    {
        //        if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma")
        //        {
        //            IslemSonucModel islem;
        //            try
        //            {
        //                if (stk.KullaniciID == 0)
        //                {

        //                }

        //                else
        //                {
        //                    #region Güncelle
        //                    if (Session[""])
        //                    var nesne = KullaniciService.Get();


        //                    nesne.Parola = new ToPasswordRepository().Md5(stk.Parola);
        //                    nesne.KullaniciID = stk.KullaniciID;





        //                    var sonucUpdate = KullaniciService.Update(nesne);

        //                    if (sonucUpdate != null)
        //                    {
        //                        islem = new IslemSonucModel()
        //                        {
        //                            IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
        //                            Aciklama = "Kullanici Başarıyla Güncellendi.",
        //                            Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
        //                            Data = sonucUpdate.KullaniciID
        //                        };
        //                    }
        //                    else
        //                    {
        //                        islem = new IslemSonucModel()
        //                        {
        //                            IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
        //                            Aciklama = "Kullanici Güncellenemedi.",
        //                            Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
        //                        };
        //                    }
        //                    fonksiyon.CacheTemizle();
        //                    fonksiyon.CacheOlustur();
        //                    #endregion
        //                }
        //            }
        //            catch (Exception error)
        //            {
        //                islem = new IslemSonucModel()
        //                {
        //                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Hata,
        //                    Aciklama = "Kullanici İşlem Sırasında Hata Oluştu.",
        //                    AciklamaDetay = error.Message,
        //                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Hata.ToString()
        //                };
        //                return Json(islem);
        //            }

        //            return Json(islem);
        //        }

        //        else
        //        {
        //            IslemSonucModel islem;
        //            islem = new IslemSonucModel()
        //            {
        //                IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
        //                Aciklama = "Kullanıcı Düzenleme Yada Ekleme Yetkiniz Bulunmuyor...",
        //                Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()

        //            };
        //            return Json(islem);
        //        }
        //    }





        //}


    }
}
