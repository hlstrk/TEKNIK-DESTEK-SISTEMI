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
    public class EfTeknikRepository : EfGenericRepository<Teknik>, ITeknikRepository
    {
        public EfTeknikRepository() : base()
        {

        }

        

        public List<Teknik> TeknikListele(int teknikservisID)
        {
            return context.Teknik.Where(x => x.TeknikServisID == teknikservisID).ToList();
        }

        public List<PocoTeknikListesi> TeknikListele2(int teknikservisID)
        {
            return context.Teknik.Where(x => x.TeknikServisID == teknikservisID)
                .Join(context.Tanim, s => s.TeknikServisID, t => t.TanimID, (s, t) => new { s, t })
                .Join(context.Tanim, s1 => s1.s.TeknikServisID, t1 => t1.TanimID, (s1, t1) => new PocoTeknikListesi()
                {
                   
                   
                   
                    TeknikGrubu = s1.t.TanimAdi,
                    TeknikServisID = s1.s.TeknikServisID
                }).OrderBy(x => x.TeknikAdi).ThenBy(c => c.Birimi).ToList();


            //context.Teknik.Include("FaturaDetay").Include("TeknikDepo")
        }

        public List<PocoTeknikListesi> TeknikListesi()
        {
            string sql = @"select 
                            s.TeknikServisID,s.TeknikAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as TeknikGrubu,
                            br.TanimAdi as Birimi
                            from Teknikler
                            left outer join Tanim k on(k.TanimID = s.TeknikGrubuID)
                            left outer join Tanim br on(br.TanimID= s.TeknikBirimID)
                            where s.Aktif= 1
                            order by s.TeknikAdi";
            return context.Database.SqlQuery<PocoTeknikListesi>(sql).ToList();
        }

        public IQueryable TeknikListesi(int teknikGrubuId)
        {
            string sql = @"select 
                            s.TeknikServisID,s.TeknikAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as TeknikGrubu,
                            br.TanimAdi as Birimi
                            from Teknikler
                            left outer join Tanim k on(k.TanimID = s.TeknikGrubuID)
                            left outer join Tanim br on(br.TanimID= s.TeknikBirimID)
                            where s.Aktif= 1 and TeknikGrubuID={0} 
                            order by s.TeknikAdi";
            return context.Database.SqlQuery<PocoTeknikListesi>(sql, teknikGrubuId).AsQueryable();
        }

        public bool TeknikSil(int teknikId)
        {
            return context.Database.ExecuteSqlCommand("delete from Teknik where TeknikServisID={0}", teknikId) > 0;
        }
    }
}
