using TeknikServis.Cache;
using TeknikServis.Dal.Concrete.EntityFramework.Repository;
using TeknikServis.Entittes.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using TeknikServis.Interfaces;

namespace TeknikServis.Bll
{
    public class CacheFonsiyon
    {
        DefaultCacheProvider provider = new DefaultCacheProvider();
        public void CacheTemizle()
        {
            provider.Remove(Enums.CacheKey.Kategoriler.ToString());
          
        }



       
        public void CacheOlustur()
        {
            DefaultCacheProvider provider = new DefaultCacheProvider();
            #region Kategori
            object kategoriCache = null;
            object kategoriCache2 = null;
            try
            {
                GenericManager<Tanim> genericManager = new GenericManager<Tanim>(new EfGenericRepository<Tanim>());
                GenericManager<TanimGrup> genericManager2 = new GenericManager<TanimGrup>(new EfGenericRepository<TanimGrup>());

                var kategori = genericManager.GetAll(x => x.TanimGrupID == (int)Enums.TanimGrup.FirmaGrubu);
                var kategoriler = genericManager2.GetAll();

                if (kategori != null)
                    kategoriCache = kategori;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
                if (kategoriler != null)
                    kategoriCache2 = kategoriler;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }

            provider.Set(Enums.CacheKey.Kategoriler.ToString(), kategoriCache);
           
            #endregion
            

            #region Ariza
            object arizaCache = null;
            try
            {
                GenericManager<ArizaTurleri> genericManager = new GenericManager<ArizaTurleri>(new EfGenericRepository<ArizaTurleri>());

                var ariza = genericManager.GetAll();

                if (ariza != null)
                    arizaCache = ariza;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }


            provider.Set(Enums.CacheKey.Arizalar.ToString(), arizaCache);



            #endregion


            #region Marka
            object markaCache = null;
            try
            {

                GenericManager<EnvanterMarkalari> genericManager = new GenericManager<EnvanterMarkalari>(new EfGenericRepository<EnvanterMarkalari>());

                var markacache = genericManager.GetAll();








                if (markacache != null)
                    markaCache = markacache;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }


            provider.Set(Enums.CacheKey.Markalar.ToString(), markaCache);



            #endregion



            #region EnvanterKategori
            object ekategoriCache = null;
            try
            {
                GenericManager<EnvanterTurleri> genericManager = new GenericManager<EnvanterTurleri>(new EfGenericRepository<EnvanterTurleri>());

                var ekategori = genericManager.GetAll();

                if (ekategori != null)
                    ekategoriCache = ekategori;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }


            provider.Set(Enums.CacheKey.Ekategori.ToString(), ekategoriCache);



            #endregion

            #region Departman
            object departmanCache = null;
            try
            {
                GenericManager<Departmanlar> genericManager = new GenericManager<Departmanlar>(new EfGenericRepository<Departmanlar>());

                var departman = genericManager.GetAll();

                if (departman != null)
                    departmanCache = departman;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }


            provider.Set(Enums.CacheKey.Departmanlar.ToString(), departmanCache);



            #endregion
            #region AltDepartman
            object altdepartmanCache = null;
            try
            {
                GenericManager<AltDepartmanlar> genericManager = new GenericManager<AltDepartmanlar>(new EfGenericRepository<AltDepartmanlar>());

                var altdepartman = genericManager.GetAll();

                if (altdepartman != null)
                    altdepartmanCache = altdepartman;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }


            provider.Set(Enums.CacheKey.AltDepartmanlar.ToString(), altdepartmanCache);



            #endregion
            #region Bolum
            object bolumCache = null;
            try
            {
                GenericManager<Bolumler> genericManager = new GenericManager<Bolumler>(new EfGenericRepository<Bolumler>());

                var bolum = genericManager.GetAll();

                if (bolum != null)
                   bolumCache = bolum;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }


            provider.Set(Enums.CacheKey.Bolumler.ToString(), bolumCache);



            #endregion
            #region Durum
            object durumCache = null;
            try
            {
                GenericManager<Durumlar> genericManager = new GenericManager<Durumlar>(new EfGenericRepository<Durumlar>());

                var durum = genericManager.GetAll();

                if (durum != null)
                    durumCache = durum;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }

            provider.Set(Enums.CacheKey.Durumlar.ToString(), durumCache);
            #endregion

            #region Iller
            object ilCache = null;
            try
            {
                IGenericService<il> genericService1 = new GenericManager<il>(new EfGenericRepository<il>());
                var il = genericService1.GetAll();

                if (il != null)
                    ilCache = il;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }

            provider.Set(Enums.CacheKey.il.ToString(), ilCache);
            #endregion



            #region Ilce
            object ilceCache = null;
            try
            {
                IGenericService<ilce> genericService1 = new GenericManager<ilce>(new EfGenericRepository<ilce>());

                var ilce = genericService1.GetAll();

                if (ilce != null)
                    ilceCache = ilce;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }

            provider.Set(Enums.CacheKey.ilce.ToString(), ilceCache);
            #endregion



            #region kategorigrup
            object kategorigrupCache = null;
            try
            {
                GenericManager<TanimGrup> genericManager2 = new GenericManager<TanimGrup>(new EfGenericRepository<TanimGrup>());

                var kategorigrup = genericManager2.GetAll();

                if (kategorigrup != null)
                    kategorigrupCache = kategorigrup;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }

            provider.Set(Enums.CacheKey.KategoriGrup.ToString(), kategorigrupCache);
            #endregion

            #region Personeller
            object PersonellerCache = null;
            try
            {
                GenericManager<KullaniciView> genericManager2 = new GenericManager<KullaniciView>(new EfGenericRepository<KullaniciView>());
                var predicate = PredicateBuilder.True<KullaniciView>();
                predicate = predicate.And(x => x.CalistigiFirma.Contains("MTX"));
                var personeller = genericManager2.GetAll(predicate);

                if (personeller != null)
                    PersonellerCache = personeller;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }

            provider.Set(Enums.CacheKey.Personeller.ToString(), PersonellerCache);
            #endregion

            #region Kullanicilar
            object kullanicilarCache = null;
            try
            {
                GenericManager<KullaniciView> genericManager2 = new GenericManager<KullaniciView>(new EfGenericRepository<KullaniciView>());
                var predicate = PredicateBuilder.True<KullaniciView>();
               
                var kullanicilar = genericManager2.GetAll(predicate);

                if (kullanicilar != null)
                    kullanicilarCache = kullanicilar;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }

            provider.Set(Enums.CacheKey.Kullanicilar.ToString(), kullanicilarCache);
            #endregion

            #region Firmalar
            object firmalarCache = null;
            try
            {
                GenericManager<Firma> genericManager2 = new GenericManager<Firma>(new EfGenericRepository<Firma>());
                var predicate = PredicateBuilder.True<Firma>();

                var firmalar = genericManager2.GetAll(predicate);

                if (firmalar != null)
                    firmalarCache = firmalar;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }

            provider.Set(Enums.CacheKey.Firmalar.ToString(), firmalarCache);
            #endregion







        }

        public object KategoriGetir()
        {
            object value = null;
            try
            {
                var kategori = (List<Tanim>)(provider.Get(Enums.CacheKey.Kategoriler.ToString()));

                if (kategori != null)
                    value = kategori;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }


        public object MarkaGetir()
        {
            object value = null;
            try
            {
                var marka = (List<EnvanterMarkalari>)(provider.Get(Enums.CacheKey.Markalar.ToString()));

                if (marka != null)
                    value = marka;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }


        public object ilGetir()
        {
            object value = null;
            try
            {
                var il = (List<il>)(provider.Get(Enums.CacheKey.il.ToString()));

                if (il != null)
                    value = il;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }

        public object ilceGetir()
        {
            object value = null;
            try
            {
                var ilce = (List<ilce>)(provider.Get(Enums.CacheKey.ilce.ToString()));

                if (ilce != null)
                    value = ilce;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);



            }

            return value;
        }
        public object KategoriGrupGetir()
        {
            object value = null;
            try
            {
                var kategorigrup = (List<TanimGrup>)(provider.Get(Enums.CacheKey.KategoriGrup.ToString()));


                if (kategorigrup != null)
                    value = kategorigrup;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }

        public object PersonelGetir()
        {
            object value = null;
            try
            {
                var personeller = (List<KullaniciView>)(provider.Get(Enums.CacheKey.Personeller.ToString()));


                if (personeller != null)
                    value = personeller;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }

        public object EkategoriGetir()
        {
            object value = null;
            try
            {
                var ekategori = (List<EnvanterTurleri>)(provider.Get(Enums.CacheKey.Ekategori.ToString()));


                if (ekategori != null)
                    value = ekategori;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }


        public object KullaniciGetir()
        {
            object value = null;
            try
            {
                var kullanicilar = (List<KullaniciView>)(provider.Get(Enums.CacheKey.Kullanicilar.ToString()));


                if (kullanicilar != null)
                    value = kullanicilar;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }


        public object FirmaGetir()
        {
            object value = null;
            try
            {
                var firmalar = (List<Firma>)(provider.Get(Enums.CacheKey.Firmalar.ToString()));


                if (firmalar != null)
                    value = firmalar;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }


        public object DepartmanGetir(int? FirmaID)
        {
            object value = null;
            try
            {
                var departman = (List<Departmanlar>)(provider.Get(Enums.CacheKey.Departmanlar.ToString()));
                if (FirmaID != 0)
                {
                    departman = departman.Where(x => x.FirmaID == FirmaID).ToList();
                }

                if (departman != null)
                    value = departman;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }
        public object AltDepartmanGetir(int? AltDepartmanID)
        {
            object value = null;
            try
            {
                var altdepartman = (List<AltDepartmanlar>)(provider.Get(Enums.CacheKey.AltDepartmanlar.ToString()));
                if (AltDepartmanID != 0)
                {
                    altdepartman = altdepartman.Where(x => x.AltDepartmanID == AltDepartmanID).ToList();
                }

                if (altdepartman != null)
                    value = altdepartman;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }

        public object BolumGetir(int? AltDeparmtanID)
        {
            object value = null;
            try
            {
                var bolum = (List<Bolumler>)(provider.Get(Enums.CacheKey.Bolumler.ToString()));
                if (AltDeparmtanID!= 0)
                {
                    bolum = bolum.Where(x => x.BolumID == AltDeparmtanID).ToList();
                }

             



                if (bolum != null)
                    value = bolum;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }



        public object ArizaGetir()
        {
            object value = null;
            try
            {
                var ariza = (List<ArizaTurleri>)(provider.Get(Enums.CacheKey.Arizalar.ToString()));

                if (ariza != null)
                    value = ariza;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }



        public object DurumGetir()
        {
            object value = null;
            try
            {
                var durum = (List<Durumlar>)(provider.Get(Enums.CacheKey.Durumlar.ToString()));

                if (durum != null)
                    value = durum;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }




        public object CacheTanimGetir(string key)
        {
            object value = null;
            try
            {
                var tanim = (List<Tanim>)(provider.Get(key));

                if (tanim != null)
                    value = tanim;
            }
            catch (Exception error)
            {
                value = null;
                Trace.WriteLine(key + " Cache'ten Okunamadı." + error.Message);
                throw new Exception(key + " Cache'ten Okunamadı.", error);
            }

            return value;
        }
    }
}
