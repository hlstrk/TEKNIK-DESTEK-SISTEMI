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
    public class EfTeklifDetayRepository : EfGenericRepository<TeklifDetay>, ITeklifDetayRepository
    {
        public EfTeklifDetayRepository() : base()
        {

        }

        public bool TeklifDetayGuncelle(TeklifDetay teklifdetay)
        {
            const string sql = "update TeklifDetay set TeklifDetayAdi={0},AdSoyad={1},Email={2} where TeklifDetayID={3}";
            return context.Database.ExecuteSqlCommand(sql, teklifdetay.TeklifDetayID, teklifdetay.TeklifID) > 0;
        }

        public List<TeklifDetay> TeklifDetayListele(int TeklifDetayID)
        {
            return context.TeklifDetay.Where(x => x.TeklifDetayID == TeklifDetayID).ToList();
        }

        public List<PocoTeklifDetayListesi> TeklifDetayListele2(int TeklifDetayID)
        {
            return context.TeklifDetay.Where(x => x.TeklifDetayID == TeklifDetayID)
                .Join(context.Tanim, s => s.TeklifDetayID, t => t.TanimID, (s, t) => new { s, t })
                .Join(context.Tanim, s1 => s1.s.TeklifDetayID, t1 => t1.TanimID, (s1, t1) => new PocoTeklifDetayListesi()
                {



                    TeklifDetayGrubu = s1.t.TanimAdi,
                    TeklifDetayID = s1.s.TeklifDetayID
                }).OrderBy(x => x.TeklifDetayAdi).ThenBy(c => c.Birimi).ToList();


            //context.TeklifDetay.Include("FaturaDetay").Include("TeklifDetayDepo")
        }

        public List<PocoTeklifDetayListesi> TeklifDetayListesi()
        {
            string sql = @"select 
                            s.TeklifDetayID,s.TeklifDetayAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as TeklifDetayGrubu,
                            br.TanimAdi as Birimi
                            from TeklifDetayler
                            left outer join Tanim k on(k.TanimID = s.TeklifDetayGrubuID)
                            left outer join Tanim br on(br.TanimID= s.TeklifDetayBirimID)
                            where s.Aktif= 1
                            order by s.TeklifDetayAdi";
            return context.Database.SqlQuery<PocoTeklifDetayListesi>(sql).ToList();
        }

        public IQueryable TeklifDetayListesi(int teklifdetayGrubuId)
        {
            string sql = @"select 
                            s.TeklifDetayID,s.TeklifDetayAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as TeklifDetayGrubu,
                            br.TanimAdi as Birimi
                            from TeklifDetayler
                            left outer join Tanim k on(k.TanimID = s.TeklifDetayGrubuID)
                            left outer join Tanim br on(br.TanimID= s.TeklifDetayBirimID)
                            where s.Aktif= 1 and TeklifDetayGrubuID={0} 
                            order by s.TeklifDetayAdi";
            return context.Database.SqlQuery<PocoTeklifDetayListesi>(sql, teklifdetayGrubuId).AsQueryable();
        }

        public bool TeklifDetaySil(int teklifdetayId)
        {
            return context.Database.ExecuteSqlCommand("delete from TeklifDetay where TeklifDetayID={0}", teklifdetayId) > 0;
        }
    }
}
