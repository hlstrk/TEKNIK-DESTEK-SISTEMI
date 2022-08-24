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
using AutoMapper;

namespace TeknikServis.MvcUI.Controllers
{
    [AllowAnonymous]

    public class IslemController : Controller
    {

        IIslemService islemService = new IslemManager(new EfIslemRepository());

        IGenericService<TeknikDetayView> islemgeneric = new GenericManager<TeknikDetayView>(new EfGenericRepository<TeknikDetayView>());
        IGenericService<Islem> genericService1 = new GenericManager<Islem>(new EfGenericRepository<Islem>());
        CacheFonsiyon cacheFonsiyon;
        IFirmaService FirmaService;


        public IslemController(IFirmaService firmaService)
        {

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

     
        public ActionResult IslemListesi(int IslemServisID = 0, int TeknikServisID = 0)
        {

            if (Session["Role"] == null)
            {
                return  RedirectToAction("Giris","Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                    var predicate = PredicateBuilder.New<TeknikDetayView>();




                    predicate = predicate.And(x => x.Aktif == true);















                    var liste = islemgeneric.GetAll(predicate);


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.IslemServisID, x.IslemAdi, x.IslemServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return  RedirectToAction("Giris","Kullanici");
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
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                    var predicate = PredicateBuilder.New<TeknikDetayView>();






                    if (TeknikServisID != 0)
                    {
                        Session["IslemAktarim"] = TeknikServisID;
                    }
                    else
                    {
                        TeknikServisID = Convert.ToInt32(Session["IslemAktarim"]);

                    }


                    predicate = predicate.And(x => x.TeknikServisID == TeknikServisID);









                    var liste = islemgeneric.GetAll(predicate);


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.IslemServisID, x.IslemAdi, x.IslemServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }



        public ActionResult IslemKarti(int IslemServisID = 0, int TeknikServisID=0)
        {
            if (Session["Role"] == null)
            {
                return  RedirectToAction("Giris","Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {
                    ViewBag.FirmalarBAG = cacheFonsiyon.FirmaGetir();

                    var islem = new TeknikDetayView();


                    if (IslemServisID != 0)
                        islem = islemgeneric.Get(TeknikServisID);

                    



                    return View(islem);
                }

                return  RedirectToAction("Giris","Kullanici");
            }
        }
        [HttpPost]
        public JsonResult IslemKarti(TeknikDetayView stk)
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
                        
                     
                        if (stk.IslemServisID == 0)
                        {
                            #region Kaydet
                            stk.AitOlduguFirma = stk.AitOlduguFirma.Remove(stk.AitOlduguFirma.Length - 1, 1);
                            if (stk.KayitZamani == null)
                            {
                                stk.KayitZamani = DateTime.Now;
                            }
                           
                            stk.TeknikServisID= Convert.ToInt32(Session["IslemAktarim"]);
                            if (Session["KullaniciAdi"] != null)
                            {
                                stk.KullaniciID = Convert.ToInt32(Session["KullaniciID"]);
                            }
                            stk.Aktif = true;

                            Islem mappedstk = Mapper.Map<Islem>(stk);

                            var sonuc = islemService.Add(mappedstk);









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
                            nesne.KDVDahilFiyat = stk.KDVDahilFiyat;

                           nesne.BirimFiyat = stk.BirimFiyat;   
                            nesne.ParaBirimi=stk.ParaBirimi;
                            nesne.FirmaID=stk.FirmaID;
                            nesne.KullaniciID=stk.KullaniciID;

                   
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
                 RedirectToAction("Giris","Kullanici");
                return Json("");
            }
        }
        [HttpPost]
        public JsonResult IslemSil(int id)
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

                    #region Sil
                 
                    islemService.Remove(id);



                   
                    #endregion
                }
                else
                {
                     RedirectToAction("Giris","Kullanici");
                }

                return Json("IslemKarti");

            }
        }

 


    }
}