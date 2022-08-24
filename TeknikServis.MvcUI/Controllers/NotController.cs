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

    public class NotController : Controller
    {


        INotService notService = new NotManager(new EfNotRepository());
        IGenericService<Not> genericService1 = new GenericManager<Not>(new EfGenericRepository<Not>());
        IGenericService<NotView> notgeneric = new GenericManager<NotView>(new EfGenericRepository<NotView>());

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
                    var predicate = PredicateBuilder.New<NotView>();





                   
                    
                      
                    var filtrefirma = Convert.ToInt32(Session["KfirmaID"]);
                    var filtrekullanici = Convert.ToInt32(Session["KullaniciID"]);

                   
                    predicate = predicate.And(x => x.FirmaID==filtrefirma);



                    var analiste = genericService1.GetAll().Where(x=>(x.FirmaID==filtrefirma && x.DuyuruMu==true)||(x.KullaniciID==filtrekullanici));




                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.NotServisID, x.NotAdi, x.NotServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(analiste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }





       
        [HttpPost]
        public JsonResult NotKarti(Not stk)
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
                        if (stk.NotID == 0)
                        {
                            #region Kaydet


                            var sonuc = notService.Add(stk);


                            if (sonuc != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Not Başarıyla Kaydedildi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.NotID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Not Kaydedilemedi.",
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
                                Aciklama = "Not Kaydedilemedi.",
                                Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                            };
                        }
                    
                    }
                    catch (Exception error)
                    {
                        islem = new IslemSonucModel()
                        {
                            IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Hata,
                            Aciklama = "Not İşlem Sırasında Hata Oluştu.",
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
        public JsonResult NotSil(int id)
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
                        var sonuc = notService.Remove(id);

                        if (sonuc)
                        {
                            islem = new IslemSonucModel()
                            {
                                IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                Aciklama = "Not Başarıyla Silindi.",
                                Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString()
                            };
                        }
                        else
                        {
                            islem = new IslemSonucModel()
                            {
                                IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                Aciklama = "Not Silinemedi.",
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
                            Aciklama = "Not Silme Sırasında Hata Oluştu.",
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


        public ActionResult BildirimListesi()
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



                    var analiste = notService.GetAll(predicate);




                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.NotServisID, x.NotAdi, x.NotServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(analiste.ToList());
                }
                return RedirectToAction("Giris", "Kullanici");
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
