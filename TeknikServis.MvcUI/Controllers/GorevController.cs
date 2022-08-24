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

    public class GorevController : Controller
    {


        IGenericService<Gorev> gorevservice = new GenericManager<Gorev>(new EfGenericRepository<Gorev>());

        IGenericService<GorevView> Gorevgeneric = new GenericManager<GorevView>(new EfGenericRepository<GorevView>());

        IKullaniciService KullaniciService;

       

        public ActionResult Index()
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

        public ActionResult AitGorevler()
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {

                    var filtrefirma = Convert.ToInt32(Session["KfirmaID"]);
                    var filtrekullanici = Convert.ToInt32(Session["KullaniciID"]);

                    var analiste = Gorevgeneric.GetAll().Where(x=>(x.FirmaID==filtrefirma && x.AitOlanKullaniciID==filtrekullanici)&&x.silinmismi!=true||(x.toplugorevmi==true)&&x.silinmismi!=true);
                    for (int i = analiste.Count(); i== 0;i++)
                    {
                     analiste.ToList()[i].KullaniciResim=KullaniciService.Get(analiste.ToList()[i].OlusturanKullaniciID).ProfilResmi;
                    };

                    return View(analiste.ToList());
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult OlusturulanGorevler()
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
              




                    var filtrefirma = Convert.ToInt32(Session["KfirmaID"]);
                    var filtrekullanici = Convert.ToInt32(Session["KullaniciID"]);


                    var analiste = Gorevgeneric.GetAll().Where(x => (x.FirmaID == filtrefirma && x.OlusturanKullaniciID== filtrekullanici)&&x.silinmismi!=true);

                    for (int i = analiste.Count(); i == 0; i++)
                    {
                        analiste.ToList()[i].KullaniciResim = KullaniciService.Get(analiste.ToList()[i].AitOlanKullaniciID).ProfilResmi;
                    };



                    return View(analiste.ToList());
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }



        public ActionResult GorevListesiMain(string GorevID = "", string GorevTuru = "")
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
             


                    var analiste = Gorevgeneric.GetAll().Where(x=>x.silinmismi!=true&&x.tamamlandimi!=true);



                    return View(analiste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }


        public ActionResult GorevListesiSilinen()
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
                    var predicate = PredicateBuilder.New<GorevView>();

                    var analiste = Gorevgeneric.GetAll().Where(x => x.silinmismi != false);

                    return View(analiste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }


        public ActionResult GorevListesiTamamlanan()
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {
                 

                    var analiste = Gorevgeneric.GetAll().Where(x => x.silinmismi != true && x.tamamlandimi == true);

                    return View(analiste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }




        [HttpPost]
        public JsonResult GorevKarti(Gorev stk)
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
                   var kaydetgorev = new Gorev();

                    try
                    {
                        if (stk.GorevID == 0)
                        {
                            #region Kaydet

                            stk.AtamaTarihi = DateTime.Now;
                           stk.FirmaID = Convert.ToInt32(Session["KfirmaID"]);
                           stk.OlusturanKullaniciID = Convert.ToInt32(Session["KullaniciID"]);
                            stk.silinmismi = false;
                            stk.tamamlandimi = false;
                      

                            var sonuc = gorevservice.Add(stk);


                            if (sonuc != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Gorev Başarıyla Kaydedildi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.GorevID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Gorev Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            #endregion
                        }
                        else
                        {
                            islem = new IslemSonucModel()
                            {
                                IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                Aciklama = "Gorev Kaydedilemedi.",
                                Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                            };
                        }
                    
                    }
                    catch (Exception error)
                    {
                        islem = new IslemSonucModel()
                        {
                            IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Hata,
                            Aciklama = "Gorev İşlem Sırasında Hata Oluştu.",
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
        public JsonResult GorevSil(int id)
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
                        var sonuc = Gorevgeneric.Remove(id);

                        if (sonuc)
                        {
                            islem = new IslemSonucModel()
                            {
                                IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                Aciklama = "Gorev Başarıyla Silindi.",
                                Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString()
                            };
                        }
                        else
                        {
                            islem = new IslemSonucModel()
                            {
                                IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                Aciklama = "Gorev Silinemedi.",
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
                            Aciklama = "Gorev Silme Sırasında Hata Oluştu.",
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


}
