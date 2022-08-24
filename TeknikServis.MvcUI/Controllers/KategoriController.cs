using LinqKit;
using TeknikServis.Bll;
using TeknikServis.Dal.Concrete.EntityFramework.Repository;
using TeknikServis.Entittes.Models;
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




    public class KategoriController : Controller
    {
        IGenericService<Markalar> genericService4 = new GenericManager<Markalar>(new EfGenericRepository<Markalar>());
        IGenericService<Tanim> genericService3 = new GenericManager<Tanim>(new EfGenericRepository<Tanim>());

        IGenericService<StokTurleriView> stokturview = new GenericManager<StokTurleriView>(new EfGenericRepository<StokTurleriView>());
        IGenericService<StokTurleri> stokturugeneric = new GenericManager<StokTurleri>(new EfGenericRepository<StokTurleri>());
        IGenericService<StokMarkalari> stokmarkageneric = new GenericManager<StokMarkalari>(new EfGenericRepository<StokMarkalari>());
        IGenericService<StokKategorileri> stokkategorigeneric = new GenericManager<StokKategorileri>(new EfGenericRepository<StokKategorileri>());


        IGenericService<Departmanlar> departmangeneric = new GenericManager<Departmanlar>(new EfGenericRepository<Departmanlar>());

        IGenericService<EnvanterMarkalari> envantermarkageneric = new GenericManager<EnvanterMarkalari>(new EfGenericRepository<EnvanterMarkalari>());
        IGenericService<EnvanterTurleri> envanterkategorigeneric = new GenericManager<EnvanterTurleri>(new EfGenericRepository<EnvanterTurleri>());
        IGenericService<AltDepartmanlar> altdepartmangeneric = new GenericManager<AltDepartmanlar>(new EfGenericRepository<AltDepartmanlar>());

        IGenericService<Bolumler> bolumgeneric = new GenericManager<Bolumler>(new EfGenericRepository<Bolumler>());

        IGenericService<ArizaTurleri> arizageneric = new GenericManager<ArizaTurleri>(new EfGenericRepository<ArizaTurleri>());
        IGenericService<Durumlar> durumgeneric = new GenericManager<Durumlar>(new EfGenericRepository<Durumlar>());

        IFirmaService firmaService = new FirmaManager(new EfFirmaRepository());


        CacheFonsiyon cacheFonsiyon;
        public KategoriController()
        {
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
                if (Session["Role"].ToString() == "Admin")
                {
                
                    return View();
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }
        public ActionResult StokTanimIndex()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {

                    return View();
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }


        public ActionResult EnvanterTanimIndex()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {

                    return View();
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }





        public ActionResult ArizaTuruSil(int ArizaTuruID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {


                    if (ArizaTuruID != 0)
                    {
                        var tanim = new ArizaTurleri();


                        tanim = arizageneric.Get(ArizaTuruID);

                        return View(tanim);

                    }





                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }

        [HttpPost]
        public JsonResult ArizaTuruSil(ArizaTurleri stk)
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
                        if (stk.ArizaTuruID != 0)
                        {
                            #region Sil












                            var sonucUpdate = arizageneric.Remove(stk.ArizaTuruID);
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





        [HttpPost]
        public JsonResult DurumSil(Durumlar stk)
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
                        if (stk.DurumID != 0)
                        {
                            #region Sil












                            var sonucUpdate = durumgeneric.Remove(stk.DurumID);
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




        [HttpPost]
        public JsonResult StokTuruSil(int StokTuruID)
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
                        if (StokTuruID != 0)
                        {
                            #region Sil












                            var sonucRemove = stokturugeneric.Remove(StokTuruID);
                            
                            if (sonucRemove != null)
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


        [HttpPost]
        public JsonResult StokKategoriSil(int StokKategoriID)
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

                    try
                    {
                        if (StokKategoriID != 0)
                        {
                            #region Sil





                            var turler = stokturugeneric.GetAll().Where(x => x.StokKategoriID==StokKategoriID);

                            if(turler.Count()==0)
                            {
                                var sonucRemove = stokkategorigeneric.Remove(StokKategoriID);

                                if (sonucRemove != null)
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

                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Hata,
                                    Aciklama = "Bu üst kategoriye ait türler var. Lütfen önce bağlantılı türleri silin",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Hata.ToString(),

                                };
                            }

                            return Json(islem);







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
        public JsonResult StokTuruGetir(int StokKategoriID)
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
                    var sonuc = new List<StokTurleri>();
                    try
                    {

                        #region Getir
                   sonuc = stokturugeneric.GetAll().Where(x=>x.StokKategoriID== StokKategoriID).ToList();

                       
                        #endregion
                    }
                    catch (Exception error)
                    {
                        return Json(error);
                    }

                    return Json(sonuc);
                }
                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
        }











        [HttpPost]
        public JsonResult StokMarkaSil(int StokMarkaID)
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

                    try
                    {
                        if (StokMarkaID != 0)
                        {
                            #region Sil












                            var sonucRemove = stokmarkageneric.Remove(StokMarkaID);

                            if (sonucRemove != null)
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


        public ActionResult DurumSil(int DurumID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Firma" || Session["Role"].ToString() == "FirmaPersonel")
                {


                    if (DurumID != 0)
                    {
                        var tanim = new Durumlar();


                        tanim = durumgeneric.Get(DurumID);

                        return View(tanim);

                    }





                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult EnvanterKategoriListesi(string TanimID = "", string firmaAdi = "")
        {
           
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" )
                {
















                    var liste = cacheFonsiyon.EkategoriGetir();


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeknikServisID, x.TeknikAdi, x.TeknikServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        








        public ActionResult StokTurListesi()
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {
















                    var liste = stokturview.GetAll();


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeknikServisID, x.TeknikAdi, x.TeknikServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }


        public ActionResult EnvanterMarkaListesi()
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {



                    var liste = cacheFonsiyon.MarkaGetir();


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeknikServisID, x.TeknikAdi, x.TeknikServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }



        public ActionResult EnvanterKategoriListesiTanim()
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {



                    var liste = cacheFonsiyon.EkategoriGetir();


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeknikServisID, x.TeknikAdi, x.TeknikServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }


        public ActionResult StokKategoriListesi()
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {


                    
                    var liste = stokkategorigeneric.GetAll();


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeknikServisID, x.TeknikAdi, x.TeknikServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }
        public ActionResult StokMarkaListesi()
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {
















                    var liste = stokmarkageneric.GetAll();


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeknikServisID, x.TeknikAdi, x.TeknikServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult MarkaListesi()
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {
                var liste = cacheFonsiyon.MarkaGetir();


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeknikServisID, x.TeknikAdi, x.TeknikServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }


        public ActionResult DurumKategoriListesi(string TanimID = "", string firmaAdi = "")
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {
             















                    var liste = cacheFonsiyon.DurumGetir();


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeknikServisID, x.TeknikAdi, x.TeknikServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }


        public ActionResult ArizaKategoriListesi(string TanimID = "", string firmaAdi = "")
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {
              















                    var liste = cacheFonsiyon.ArizaGetir();


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeknikServisID, x.TeknikAdi, x.TeknikServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }


        public ActionResult FirmaKategoriListesi(string TanimID = "", string firmaAdi = "")
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel")
                {
















                    var liste = cacheFonsiyon.KategoriGetir();


                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.TeknikServisID, x.TeknikAdi, x.TeknikServisID, x.MusteriTuru, x.CalisanYorumu, x.CepTelefonu, x.MusteriBeyani });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }


        public ActionResult FirmaListesi(string FirmaID = "", string firmaAdi = "")
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {


                    var predicate = PredicateBuilder.True<Firma>();



                    var liste = firmaService.GetAll(predicate);
                    //var liste = genericService1.GetAllSelect(predicate, x => new { x.FirmaID, x.FirmaAdi, x.FirmaID, x.MusteriTuru, x.AdSoyad, x.CepTelefonu, x.Email });

                    return View(liste);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult FirmaDepartmanKarti(int FirmaID = 0)


        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {
                   


                  

                 
                    
                       var firma = firmaService.Get(FirmaID);
                       
                    


                    return View(firma/*.Departmanlar*/);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }
      

        public ActionResult KategoriKarti(int TanimID = 0)


        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {
                  
                    ViewBag.Kategoriler = cacheFonsiyon.KategoriGetir();


                    var firma = new Tanim();

                   

                    return View(firma);
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult DurumEkleKarti(int DurumID)


        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {

                 
                 
                        var durum = new Durumlar();
                        if (DurumID != 0)
                        {
                            durum = durumgeneric.Get(DurumID);

                        }
                        return View(durum);
                    


               



                    return View();
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult ArizaTuruEkleKarti(int ArizaTuruID)


        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {

                  
                        var ariza = new ArizaTurleri();
                        if (ArizaTuruID != 0)
                        {
                            ariza = arizageneric.Get(ArizaTuruID);

                        }
                        return View(ariza);

                   






             
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult StokMarkaEkleKarti(int StokMarkaID)




        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {


                    var ariza = new StokMarkalari();
                    if (StokMarkaID != 0)
                    {
                        ariza = stokmarkageneric.Get(StokMarkaID);

                    }
                    return View(ariza);









                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult StokTuruEkleKarti(int StokTuruID)


        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    ViewBag.StokKategorileri = stokkategorigeneric.GetAll();

                    var ariza = new StokTurleri();
                    if (StokTuruID != 0)
                    {
                        ariza = stokturugeneric.Get(StokTuruID);

                    }
                    return View(ariza);









                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }


        public ActionResult StokKategoriEkleKarti(int StokKategoriID)


        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {


                    var ariza = new StokKategorileri();
                    if (StokKategoriID != 0)
                    {
                        ariza = stokkategorigeneric.Get(StokKategoriID);

                    }
                    return View(ariza);









                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        [HttpPost]

        public ActionResult DepartmanGetir(int FirmaID = 0)
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





                        var butundepartmanlar = departmangeneric.GetAll().Where(x => x.FirmaID == FirmaID).ToList();

                        return Json(butundepartmanlar);

                    }





                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }
        [HttpPost]
        public ActionResult DepEkle(int FirmaID=0,int DepartmanID=0,int AltDepartmanID=0,int BolumID=0,string DepartmanAdi="",string AltDepartmanAdi="",string BolumAdi="")
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" )

                {



                    if (FirmaID != 0)
                    {


                       






                        if (DepartmanID !=0)
                        {






                            if (AltDepartmanID != 0)
                            {





                                if (BolumAdi != "")
                                {

                                    var eklenecekbolum = new Bolumler();
                                    eklenecekbolum.BolumAdi = BolumAdi; 
                                    eklenecekbolum.AltDepartmanID = AltDepartmanID;

                                    bolumgeneric.Add(eklenecekbolum);



                                }



                            }
                            else
                            {
                                if (AltDepartmanAdi != "")
                                {

                                    var eklenecekaltdepartman = new AltDepartmanlar();
                                    eklenecekaltdepartman.DepartmanID = DepartmanID;
                                    eklenecekaltdepartman.AltDepartmanAdi = AltDepartmanAdi;

                                   var sonuc= altdepartmangeneric.Add(eklenecekaltdepartman);
                                    if (BolumAdi != "")
                                    {
                                        var eklenecekbolum = new Bolumler();
                                        eklenecekbolum.AltDepartmanID = sonuc.AltDepartmanID;
                                        eklenecekbolum.BolumAdi= BolumAdi;
                                    }

                                }

                              
                            }





                        }


                        else
                        {
                            if (DepartmanAdi != "")
                            {

                                var eklencekdepartman = new Departmanlar();
                                eklencekdepartman.DepartmanAdi = DepartmanAdi;
                                eklencekdepartman.FirmaID = FirmaID;

                                var sonucdep = departmangeneric.Add(eklencekdepartman);
                                if(AltDepartmanAdi!="")
                                {
                                    var eklencekaltdep = new AltDepartmanlar();
                                    eklencekaltdep.AltDepartmanAdi = AltDepartmanAdi;
                                    eklencekaltdep.DepartmanID = sonucdep.DepartmanID;

                                    var sonucaltdep = altdepartmangeneric.Add(eklencekaltdep);
                                    if (BolumAdi != "")
                                    {
                                        var eklenecekbolum = new Bolumler();
                                        eklenecekbolum.AltDepartmanID = sonucaltdep.AltDepartmanID;
                                        eklenecekbolum.BolumAdi = BolumAdi;
                                        var sonucbolum = bolumgeneric.Add(eklenecekbolum);
                                    }
                                }
                               

                            }


                        }





                    }










                  

                    return View();
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult DepartmanKarti(int FirmaID = 0)


        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {

           

                    if (FirmaID != 0)
                    {

                        Session["DepartmanFirmaID"] = FirmaID;
                        var liste = new Departmanlar();
                        liste.FirmaID = FirmaID;

                        return View(liste);
                    }





                    return View();
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }





        public ActionResult EnvanterMarkaEkleKarti(int EnvanterMarkaID = 0)


        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {

                    var liste = new EnvanterMarkalari();

                    if (EnvanterMarkaID != 0)
                    {

                        liste=envantermarkageneric.Get(EnvanterMarkaID);



                 
                    }


                    return View(liste);


               
                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }




        public ActionResult EnvanterKategoriEkleKarti(int EnvanterTurID = 0)


        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {

                    var liste = new EnvanterTurleri();

                    if (EnvanterTurID != 0)
                    {

                        liste = envanterkategorigeneric.Get(EnvanterTurID);




                    }


                    return View(liste);



                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        public ActionResult DepartmanListesi(int FirmaID = 0)


        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")


                {

                    
                        var liste = new List<Departmanlar>();

                        if (FirmaID != 0)
                        {
                            var predicate = PredicateBuilder.New<Departmanlar>();

                            predicate = predicate.And(x => x.FirmaID == FirmaID);

                            liste = departmangeneric.GetAll(predicate);

                            return View(liste);



                        }

                    
                   




                    return View(liste);

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


                        var butundepartmanlar = bolumgeneric.GetAll().Where(x => x.AltDepartmanID == AltDepartmanID).ToList();

                        return Json(butundepartmanlar);

                    }





                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }



        public ActionResult AltDepartmanListesi(int FirmaID = 0)


        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")


                {



                    var liste = new List<AltDepartmanlar>();

                    if (FirmaID != 0)
                    {
                        var predicate = PredicateBuilder.New<AltDepartmanlar>();

                        predicate = predicate.And(x => x.DepartmanID == FirmaID);

                        liste = altdepartmangeneric.GetAll(predicate);
                        return View(liste);
                    }







                    return View(liste);

                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }


     
        public ActionResult BolumListesi(int FirmaID = 0)


        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")


                {

                   
              
              

                            var liste = new List<Bolumler>();

                            if (FirmaID != 0)
                            {
                                var predicate = PredicateBuilder.New<Bolumler>();

                                predicate = predicate.And(x => x.AltDepartmanID == FirmaID);

                                liste = bolumgeneric.GetAll(predicate);

                            }

                        return View(liste);

             





             

                }
                return RedirectToAction("Giris", "Kullanici");
            }
        }

        [HttpPost]
        public JsonResult KategoriKarti(Tanim stk)
        {

            if (Session["Role"] == null)
            {

                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    IslemSonucModel islem;
                    try
                    {
                        if (stk.TanimID == 0)
                        {
                            #region Kaydet
                            stk.KayitZamani = DateTime.Now;

                            var sonuc = genericService3.Add(stk);

                            if (sonuc != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kategori Başarıyla Kaydedildi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.TanimID
                                };
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kategori Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            #endregion
                        }
                        else
                        {
                            #region Güncelle


                          var  sonucUpdate = 22;

                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kategori Başarıyla Güncellendi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                              

                                };
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kategori Güncellenemedi.",
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
                            Aciklama = "Kategori İşlem Sırasında Hata Oluştu.",
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
        public JsonResult  StokMarkaEkleKarti(StokMarkalari stk)
        {

            if (Session["Role"] == null)
            {

                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    IslemSonucModel islem;
                    try
                    {
                        if (stk.StokMarkaID == 0)
                        {
                            #region Kaydet
                            stk.EkleyenKullaniciID = Convert.ToInt32(Session["KullaniciID"]);
                            stk.BarkodNo = new StokTicketGenerator().TicketOlustur();
                            var sonuc = stokmarkageneric.Add(stk);

                            if (sonuc != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kategori Başarıyla Kaydedildi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.StokMarkaID
                                };
                              
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kategori Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            #endregion
                        }
                        else
                        {
                            #region Güncelle


                            var sonucUpdate = 22;

                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kategori Başarıyla Güncellendi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),


                                };
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kategori Güncellenemedi.",
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
                            Aciklama = "Kategori İşlem Sırasında Hata Oluştu.",
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
        public JsonResult EnvanterKategoriEkleKarti(EnvanterTurleri stk)
        {

            if (Session["Role"] == null)
            {

                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    IslemSonucModel islem;
                    try
                    {
                        if (stk.EnvanterTurID == 0)
                        {
                            #region Kaydet
                            stk.KullaniciID = Convert.ToInt32(Session["KullaniciID"]);
                            stk.BarkodNo = new StokTicketGenerator().TicketOlustur();
                            var sonuc = envanterkategorigeneric.Add(stk);

                            if (sonuc != null)
                            {
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kategori Başarıyla Kaydedildi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.EnvanterTurID
                                };

                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kategori Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            #endregion
                        }
                        else
                        {
                            #region Güncelle


                            var sonucUpdate = 22;

                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kategori Başarıyla Güncellendi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),


                                };
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kategori Güncellenemedi.",
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
                            Aciklama = "Kategori İşlem Sırasında Hata Oluştu.",
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
        public JsonResult EnvanterMarkaEkleKarti(EnvanterMarkalari stk)
        {

            if (Session["Role"] == null)
            {

                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    IslemSonucModel islem;
                    try
                    {
                        if (stk.EnvanterMarkaID == 0)
                        {
                            #region Kaydet
                            stk.KullaniciID = Convert.ToInt32(Session["KullaniciID"]);
                            stk.BarkodNo = new StokTicketGenerator().TicketOlustur();
                            var sonuc = envantermarkageneric.Add(stk);

                            if (sonuc != null)
                            {
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kategori Başarıyla Kaydedildi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.EnvanterMarkaID
                                };

                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kategori Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            #endregion
                        }
                        else
                        {
                            #region Güncelle


                            var sonucUpdate = 22;

                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kategori Başarıyla Güncellendi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),


                                };
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kategori Güncellenemedi.",
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
                            Aciklama = "Kategori İşlem Sırasında Hata Oluştu.",
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
        public JsonResult StokTuruEkleKarti(StokTurleri stk)
        {

            if (Session["Role"] == null)
            {

                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    IslemSonucModel islem;
                    try
                    {
                        if (stk.StokTuruID == 0)
                        {
                            #region Kaydet
                            stk.EkleyenKullaniciID = Convert.ToInt32(Session["KullaniciID"]);
                            stk.BarkodNo = new StokTicketGenerator().TicketOlustur();
                            var sonuc = stokturugeneric.Add(stk);

                            if (sonuc != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Stok Türü Başarıyla Kaydedildi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.StokTuruID
                                };

                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Stok Türü Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            #endregion
                        }
                        else
                        {
                            #region Güncelle


                            var sonucUpdate = 22;

                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kategori Başarıyla Güncellendi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),


                                };
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kategori Güncellenemedi.",
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
                            Aciklama = "Kategori İşlem Sırasında Hata Oluştu.",
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
        public JsonResult StokKategoriEkleKarti(StokKategorileri stk)
        {

            if (Session["Role"] == null)
            {

                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    IslemSonucModel islem;
                    try
                    {
                        if (stk.StokKategoriID == 0)
                        {
                            #region Kaydet
                            stk.EkleyenKullaniciID = Convert.ToInt32(Session["KullaniciID"]);
                            stk.BarkodNo = new StokTicketGenerator().TicketOlustur();
                            var sonuc = stokkategorigeneric.Add(stk);

                            if (sonuc != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kategori Başarıyla Kaydedildi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.StokKategoriID
                                };

                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kategori Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            #endregion
                        }
                        else
                        {
                            #region Güncelle


                            var sonucUpdate = 22;

                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kategori Başarıyla Güncellendi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),


                                };
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kategori Güncellenemedi.",
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
                            Aciklama = "Kategori İşlem Sırasında Hata Oluştu.",
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
        public JsonResult DurumKategoriEkleFK(Durumlar stk)
        {

            if (Session["Role"] == null)
            {

                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    IslemSonucModel islem;
                    try
                    {
                        if (stk.DurumID == 0)
                        {
                            #region Kaydet


                            var sonuc = durumgeneric.Add(stk);

                            if (sonuc != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kategori Başarıyla Kaydedildi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.DurumID
                                };
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kategori Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            #endregion
                        }
                        else
                        {
                            #region Güncelle


                            var sonucUpdate = 22;

                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kategori Başarıyla Güncellendi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),


                                };
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kategori Güncellenemedi.",
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
                            Aciklama = "Kategori İşlem Sırasında Hata Oluştu.",
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
        public JsonResult ArizaKategoriEkleFK(ArizaTurleri stk)
        {

            if (Session["Role"] == null)
            {

                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    IslemSonucModel islem;
                    try
                    {
                        if (stk.ArizaTuruID == 0)
                        {
                            #region Kaydet
                          

                            var sonuc = arizageneric.Add(stk);

                            if (sonuc != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kategori Başarıyla Kaydedildi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),
                                    Data = sonuc.ArizaTuruID
                                };
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kategori Kaydedilemedi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Uyari.ToString()
                                };
                            }
                            #endregion
                        }
                        else
                        {
                            #region Güncelle


                            var sonucUpdate = 22;

                            if (sonucUpdate != null)
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Kategori Başarıyla Güncellendi.",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),


                                };
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Kategori Güncellenemedi.",
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
                            Aciklama = "Kategori İşlem Sırasında Hata Oluştu.",
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
        public ActionResult KategoriSil(int TanimID = 0)
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Giris", "Kullanici");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Kategori" || Session["Role"].ToString() == "KategoriPersonel")
                {


                    if (TanimID != 0)
                    {
                        var firma = new Tanim();


                        firma = genericService3.Get(TanimID);

                        return View(firma);

                    }





                }

                return RedirectToAction("Giris", "Kullanici");
            }
        }









        [HttpPost]
        public JsonResult EnvanterMarkaSil(int EnvanterMarkaID=0)
        {
            if (Session["Role"] == null)
            {
                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Kategori" || Session["Role"].ToString() == "KategoriPersonel")
                {
                    IslemSonucModel islem;

                    try
                    {
                        if (EnvanterMarkaID != 0)
                        {
                            #region Güncelle
                            var sonucUpdate = envantermarkageneric.Remove(EnvanterMarkaID);





                            if (sonucUpdate != null)
                            {
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Envanter markası başarıyla silindi..",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),

                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Envanter markası Silinemedi!.",

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
        public JsonResult EnvanterKategoriSil(int EnvanterKategoriID = 0)
        {
            if (Session["Role"] == null)
            {
                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Kategori" || Session["Role"].ToString() == "KategoriPersonel")
                {
                    IslemSonucModel islem;

                    try
                    {
                        if (EnvanterKategoriID != 0)
                        {
                            #region Güncelle
                            var sonucUpdate = envanterkategorigeneric.Remove(EnvanterKategoriID);





                            if (sonucUpdate != null)
                            {
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Envanter kategorisi başarıyla silindi..",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),

                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Envanter Kategorisi Silinemedi!.",

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
        public JsonResult EnvanterSil(int EnvanterMarkaID = 0)
        {
            if (Session["Role"] == null)
            {
                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Kategori" || Session["Role"].ToString() == "KategoriPersonel")
                {
                    IslemSonucModel islem;

                    try
                    {
                        if (EnvanterMarkaID != 0)
                        {
                            #region Güncelle
                            var sonucUpdate = envantermarkageneric.Remove(EnvanterMarkaID);





                            if (sonucUpdate != null)
                            {
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Basarili,
                                    Aciklama = "Envanter kategorisi başarıyla silindi..",
                                    Baslik = IslemSonucuEnum.IslemSonucKodlari.Basarili.ToString(),

                                };
                            }
                            else
                            {
                                islem = new IslemSonucModel()
                                {
                                    IslemKodu = (int)IslemSonucuEnum.IslemSonucKodlari.Uyari,
                                    Aciklama = "Envanter Kategorisi Silinemedi!.",

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
        public JsonResult KategoriSil(Tanim stk)
        {
            if (Session["Role"] == null)
            {
                RedirectToAction("Giris", "Kullanici");
                return Json("");
            }
            else
            {
                if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Personel" || Session["Role"].ToString() == "Kategori" || Session["Role"].ToString() == "KategoriPersonel")
                {
                    IslemSonucModel islem;

                    try
                    {
                        if (stk.TanimID != 0)
                        {
                            #region Güncelle
                            var nesne = genericService3.Get(stk.TanimID);




                            nesne.Aktif = false;







                            var sonucUpdate = genericService3.Update(nesne);

                            if (sonucUpdate != null)
                            {
                                cacheFonsiyon.CacheTemizle();
                                cacheFonsiyon.CacheOlustur();
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





    }
}