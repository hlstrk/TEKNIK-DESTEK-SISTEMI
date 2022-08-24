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

    public class StokController : Controller
    {

        IStokService stokService = new StokManager(new EfStokRepository());

        IGenericService<StokView> stokgeneric = new GenericManager<StokView>(new EfGenericRepository<StokView>());
        IGenericService<StokMarkalari> stokmarkaservice = new GenericManager<StokMarkalari>(new EfGenericRepository<StokMarkalari>());
        IGenericService<StokKategorileri> stokkategoriservice = new GenericManager<StokKategorileri>(new EfGenericRepository<StokKategorileri>());
        IGenericService<StokTurleri> stokturservice = new GenericManager<StokTurleri>(new EfGenericRepository<StokTurleri>());
        CacheFonsiyon cacheFonsiyon;
        IKullaniciService KullaniciService;

        public StokController(IKullaniciService kullaniciService)
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

        public ActionResult StokListesi(string StokServisID = "", string stokAdi = "")
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
                    var predicate = PredicateBuilder.True<StokView>();

                    predicate = predicate.And(x => x.KacTane != 0);


                      var liste = stokgeneric.GetAll(predicate);


                    //var liste = stokgeneric.GetAllSelect(predicate, x => new { x.StokServisID, x.StokAdi, x.StokServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }




        



        public ActionResult StokKarti(int StokID = 0)
        {
            if (Session["Role"] == null)
            {                       
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Firma")
                {
              
                    ViewBag.EFirmaBAG = cacheFonsiyon.FirmaGetir();
                    ViewBag.EKategoriBAG = stokkategoriservice.GetAll();
                    ViewBag.EMarkalarBAG = stokmarkaservice.GetAll();
            


                    var stok = new Stok();

                    if (StokID != 0)
                    {
                        stok = stokService.Get(StokID);

                        ViewBag.ETurBAG = stokturservice.GetAll().Where(x => x.StokTuruID == stok.StokTuruID);

                    }

                    else
                    {
                        ViewBag.ETurBAG = Enumerable.Empty<SelectListItem>();
                    }
                    return View(stok);
                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }
        [HttpPost]
        public JsonResult StokKarti(Stok stk)
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
                        if (stk.StokID == 0)
                        {
                            #region Kaydet
                            stk.EkleyenKullaniciID = Convert.ToInt32(Session["KullaniciID"]);

                            stk.BarkodNo = new StokTicketGenerator().TicketOlustur();

                            var sonuc = stokService.Add(stk);


                            if (sonuc != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Stok Başarıyla Kaydedildi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.StokID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Stok Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            #endregion
                        }

                        else
                        {
                            #region Güncelle
                            var nesne = stokService.Get(stk.StokID);



                            nesne.StokID = stk.StokID;

                            nesne.StokMarkaID = stk.StokMarkaID;
                           
                            nesne.StokKategoriID = stk.StokKategoriID;
                            nesne.StokTuruID = stk.StokTuruID;
                            nesne.Aciklama = stk.Aciklama;
                            nesne.KDVOrani = stk.KDVOrani;
                            nesne.Fiyat= stk.Fiyat; 
                            nesne.AnyDesk = stk.AnyDesk;
                            nesne.DonanimMi = stk.DonanimMi;
                            nesne.IP = stk.IP;
                           nesne.Fiyat=stk.Fiyat;   
                            nesne.MAC = stk.MAC;
                        
                            nesne.IP = stk.IP;
                          
                      
                          nesne.VarsaGarantiSuresi = stk.VarsaGarantiSuresi;
                            nesne.AlisFiyati = stk.AlisFiyati;

                            nesne.OS = stk.OS;
                            nesne.TeamViewer = stk.TeamViewer;
                           





                            var sonucUpdate = stokService.Update(nesne);

                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Stok Başarıyla Güncellendi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonucUpdate.StokID
                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Stok Güncellenemedi.",

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
                            Aciklama = "Stok İşlem Sırasında Hata Oluştu.",
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



        public ActionResult StokSilKart(int StokID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {


                    if (StokID != 0)
                    {
                    


                        var stok = stokgeneric.Get(StokID);


                        return View(stok);



                    }





                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }



        [HttpPost]
        public JsonResult StokSil(StokView env)
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
                        var sonuc = stokService.Remove(env.StokID);

                        if (sonuc)
                        {
                            islem = new IslemSonucModel()
                            {
                                IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                Aciklama = "Stok Başarıyla Silindi.",
                                Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString()
                            };
                        }
                        else
                        {
                            islem = new IslemSonucModel()
                            {
                                IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                Aciklama = "Stok Silinemedi.",
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
                            Aciklama = "Stok Silme Sırasında Hata Oluştu.",
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
