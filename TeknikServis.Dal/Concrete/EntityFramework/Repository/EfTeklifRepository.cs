using TeknikServis.Dal.Abstract;
using TeknikServis.Dal.Concrete.EntityFramework.Context;
using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Dal.Concrete.EntityFramework.Repository
{
    public class EfTeklifRepository : EfGenericRepository<Teklif>, ITeklifRepository
    {
        public EfTeklifRepository() : base()
        {

        }

        public bool TeklifGuncelle(Teklif teklif)
        {
            const string sql = "update Teklif set TeklifAdi={0},AdSoyad={1},Email={2} where TeklifID={3}";
            return context.Database.ExecuteSqlCommand(sql, teklif.TeklifID, teklif.YoneticiNotu, teklif.FiyaTuru, teklif.Durumu, teklif.AitOlduguFirma) > 0;
        }

        public List<Teklif> TeklifListele(int TeklifID)
        {
            return context.Teklif.Where(x => x.TeklifID == TeklifID).ToList();
        }

        public List<PocoTeklifListesi> TeklifListele2(int TeklifID)
        {
            return context.Teklif.Where(x => x.TeklifID == TeklifID)
                .Join(context.Tanim, s => s.TeklifID, t => t.TanimID, (s, t) => new { s, t })
                .Join(context.Tanim, s1 => s1.s.TeklifID, t1 => t1.TanimID, (s1, t1) => new PocoTeklifListesi()
                {



                    TeklifGrubu = s1.t.TanimAdi,
                    TeklifID = s1.s.TeklifID
                }).OrderBy(x => x.TeklifAdi).ThenBy(c => c.Birimi).ToList();


            //context.Teklif.Include("FaturaDetay").Include("TeklifDepo")
        }

        public List<PocoTeklifListesi> TeklifListesi()
        {
            string sql = @"select 
                            s.TeklifID,s.TeklifAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as TeklifGrubu,
                            br.TanimAdi as Birimi
                            from Teklifler
                            left outer join Tanim k on(k.TanimID = s.TeklifGrubuID)
                            left outer join Tanim br on(br.TanimID= s.TeklifBirimID)
                            where s.Aktif= 1
                            order by s.TeklifAdi";
            return context.Database.SqlQuery<PocoTeklifListesi>(sql).ToList();
        }

        public IQueryable TeklifListesi(int teklifGrubuId)
        {
            string sql = @"select 
                            s.TeklifID,s.TeklifAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as TeklifGrubu,
                            br.TanimAdi as Birimi
                            from Teklifler
                            left outer join Tanim k on(k.TanimID = s.TeklifGrubuID)
                            left outer join Tanim br on(br.TanimID= s.TeklifBirimID)
                            where s.Aktif= 1 and TeklifGrubuID={0} 
                            order by s.TeklifAdi";
            return context.Database.SqlQuery<PocoTeklifListesi>(sql, teklifGrubuId).AsQueryable();
        }

        public bool TeklifSil(int teklifId)
        {
            return context.Database.ExecuteSqlCommand("delete from Teklif where TeklifID={0}", teklifId) > 0;
        }
    }
}
