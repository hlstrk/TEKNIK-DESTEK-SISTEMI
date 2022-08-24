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

namespace TeknikServis.MvcUI.Controllers
{
    [AllowAnonymous]

    public class EnvanterController : Controller
    {

        IEnvanterService envanterService = new EnvanterManager(new EfEnvanterRepository());

        IGenericService<EnvanterView> genericService1 = new GenericManager<EnvanterView>(new EfGenericRepository<EnvanterView>());
        CacheFonsiyon cacheFonsiyon;
        IKullaniciService KullaniciService;

        public EnvanterController(IKullaniciService kullaniciService)
        {

            KullaniciService = kullaniciService;
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


                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                    ViewBag.Kategoriler = cacheFonsiyon.KategoriGetir();


                    return View();
                }


                return RedirectToAction("Giris", "Kullanici");
            }
        }



            public ActionResult  KullaniciEnvanterListesi(string EnvanterServisID = "", string envanterAdi = "")
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {






                    var liste = genericService1.GetAll().Where(x=> x.FirmaEnvanterimi==false);


             

                    return View(liste.ToList());
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult FirmaEnvanterListesi(string EnvanterServisID = "", string envanterAdi = "")
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {






                    var liste = genericService1.GetAll().Where(x => x.FirmaEnvanterimi == true);


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.EnvanterServisID, x.EnvanterAdi, x.EnvanterServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste.ToList());
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }


        public ActionResult EnvanterListesi(string EnvanterServisID = "", string envanterAdi = "")
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
            





                    var liste = genericService1.GetAll();


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.EnvanterServisID, x.EnvanterAdi, x.EnvanterServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult EnvanterKarti(int EnvanterID = 0,bool FirmaEnvanterimi=false)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                   ViewBag.EKullaniciBAG= cacheFonsiyon.KullaniciGetir();
                    ViewBag.EFirmaBAG = cacheFonsiyon.FirmaGetir();
                    ViewBag.EKategoriBAG = cacheFonsiyon.EkategoriGetir();
                    ViewBag.EMarkalarBAG = cacheFonsiyon.MarkaGetir();
           

                    

                    var envanter = new Envanter();
                    envanter.FirmaEnvanterimi = FirmaEnvanterimi;
                    

                    if (EnvanterID != 0)
                    {
                        envanter = envanterService.Get(EnvanterID);
                       
                    }

                    return View(envanter);
                }

                return RedirectToAction("Giris", "Kullanici");
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
                            if(stk.FirmaID==0)
                            {
                                var envanterkullanici =KullaniciService.Get(stk.KullaniciID);
                                if(envanterkullanici != null)
                                {
                                    stk.FirmaID = envanterkullanici.FirmaID;
                                    stk.BarkodNo = envanterkullanici.KullaniciID+"-"+new TicketGenerator().EnvanterTicketOlustur();
                                }




                            }
                            else
                            {
                                stk.BarkodNo = stk.FirmaID + "-" + new TicketGenerator().EnvanterTicketOlustur();
                            }
                           
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
                            nesne.Kapsamici = stk.Kapsamici;
                            nesne.MAC = stk.MAC;
                            nesne.MAC2= stk.MAC2;   
                            nesne.WLANMAC = stk.WLANMAC;    
                            nesne.IP= stk.IP;   
                            nesne.IP2= stk.IP2; 
                            nesne.Marka= stk.Marka; 
                            nesne.WLANIP= stk.WLANIP;
                     
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
