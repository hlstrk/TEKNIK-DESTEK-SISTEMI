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
    public class EfIslemRepository : EfGenericRepository<Islem>, IIslemRepository
    {
        public EfIslemRepository() : base()
        {

        }

        public bool IslemGuncelle(Islem islem)
        {
            const string sql = "update Islem set IslemAdi={0},AdSoyad={1},Email={2} where IslemServisID={3}";
            return context.Database.ExecuteSqlCommand(sql, islem.IslemServisID) > 0;
        }

        public List<Islem> IslemListele(int islemservisID)
        {
            return context.Islem.Where(x => x.IslemServisID == islemservisID).ToList();
        }

        

        public List<PocoIslemListesi> IslemListesi()
        {
            string sql = @"select 
                            s.IslemServisID,s.IslemAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as IslemGrubu,
                            br.TanimAdi as Birimi
                            from Islemler
                            left outer join Tanim k on(k.TanimID = s.IslemGrubuID)
                            left outer join Tanim br on(br.TanimID= s.IslemBirimID)
                            where s.Aktif= 1
                            order by s.IslemAdi";
            return context.Database.SqlQuery<PocoIslemListesi>(sql).ToList();
        }

        public IQueryable IslemListesi(int islemGrubuId)
        {
            string sql = @"select 
                            s.IslemServisID,s.IslemAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as IslemGrubu,
                            br.TanimAdi as Birimi
                            from Islemler
                            left outer join Tanim k on(k.TanimID = s.IslemGrubuID)
                            left outer join Tanim br on(br.TanimID= s.IslemBirimID)
                            where s.Aktif= 1 and IslemGrubuID={0} 
                            order by s.IslemAdi";
            return context.Database.SqlQuery<PocoIslemListesi>(sql, islemGrubuId).AsQueryable();
        }

        public bool IslemSil(int islemId)
        {
            return context.Database.ExecuteSqlCommand("delete from Islem where IslemServisID={0}", islemId) > 0;
        }
    }
}
