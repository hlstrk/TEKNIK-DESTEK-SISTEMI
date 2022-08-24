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
    public class EfEnvanterRepository : EfGenericRepository<Envanter>, IEnvanterRepository
    {
        public EfEnvanterRepository() : base()
        {

        }

        public bool EnvanterGuncelle(Envanter envanter)
        {
            const string sql = "update Envanter set EnvanterAdi={0},AdSoyad={1},Email={2} where EnvanterServisID={3}";
            return context.Database.ExecuteSqlCommand(sql, envanter.EnvanterID, envanter.KullaniciID) > 0;
        }

        public List<Envanter> EnvanterListele(int envanterservisID)
        {
            return context.Envanter.Where(x => x.EnvanterID == envanterservisID).ToList();
        }

        

        public List<PocoEnvanterListesi> EnvanterListesi()
        {
            string sql = @"select 
                            s.EnvanterServisID,s.EnvanterAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as EnvanterGrubu,
                            br.TanimAdi as Birimi
                            from Envanterler
                            left outer join Tanim k on(k.TanimID = s.EnvanterGrubuID)
                            left outer join Tanim br on(br.TanimID= s.EnvanterBirimID)
                            where s.Aktif= 1
                            order by s.EnvanterAdi";
            return context.Database.SqlQuery<PocoEnvanterListesi>(sql).ToList();
        }

        public IQueryable EnvanterListesi(int envanterGrubuId)
        {
            string sql = @"select 
                            s.EnvanterServisID,s.EnvanterAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as EnvanterGrubu,
                            br.TanimAdi as Birimi
                            from Envanterler
                            left outer join Tanim k on(k.TanimID = s.EnvanterGrubuID)
                            left outer join Tanim br on(br.TanimID= s.EnvanterBirimID)
                            where s.Aktif= 1 and EnvanterGrubuID={0} 
                            order by s.EnvanterAdi";
            return context.Database.SqlQuery<PocoEnvanterListesi>(sql, envanterGrubuId).AsQueryable();
        }

        public bool EnvanterSil(int envanterId)
        {
            return context.Database.ExecuteSqlCommand("delete from Envanter where EnvanterServisID={0}", envanterId) > 0;
        }
    }
}
