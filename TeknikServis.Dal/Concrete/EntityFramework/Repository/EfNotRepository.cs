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
    public class EfNotRepository : EfGenericRepository<Not>, INotRepository
    {
        public EfNotRepository() : base()
        {

        }

        public bool NotGuncelle(Not not)
        {
            const string sql = "update Not set NotAdi={0},AdSoyad={1},Email={2} where NotID={3}";
            return context.Database.ExecuteSqlCommand(sql, not.NotID, not.KullaniciAdi, not.NotIcerigi, not.FirmaID, not.DuyuruMu) > 0;
        }

        public List<Not> NotListele(int notservisID)
        {
            return context.Not.Where(x => x.NotID == notservisID).ToList();
        }

       

        public List<PocoNotListesi> NotListesi()
        {
            string sql = @"select 
                            s.NotID,s.NotAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as NotGrubu,
                            br.TanimAdi as Birimi
                            from Notler
                            left outer join Tanim k on(k.TanimID = s.NotGrubuID)
                            left outer join Tanim br on(br.TanimID= s.NotBirimID)
                            where s.Aktif= 1
                            order by s.NotAdi";
            return context.Database.SqlQuery<PocoNotListesi>(sql).ToList();
        }

        public IQueryable NotListesi(int notGrubuId)
        {
            string sql = @"select 
                            s.NotID,s.NotAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as NotGrubu,
                            br.TanimAdi as Birimi
                            from Notler
                            left outer join Tanim k on(k.TanimID = s.NotGrubuID)
                            left outer join Tanim br on(br.TanimID= s.NotBirimID)
                            where s.Aktif= 1 and NotGrubuID={0} 
                            order by s.NotAdi";
            return context.Database.SqlQuery<PocoNotListesi>(sql, notGrubuId).AsQueryable();
        }

        public bool NotSil(int notId)
        {
            return context.Database.ExecuteSqlCommand("delete from Not where NotID={0}", notId) > 0;
        }
    }
}
