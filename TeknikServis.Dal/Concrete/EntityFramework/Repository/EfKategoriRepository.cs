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
    public class EfTanimRepository : EfGenericRepository<Tanim>, ITanimRepository
    {
        public EfTanimRepository() : base()
        {

        }

        public bool TanimGuncelle(Tanim tanim)
        {
            const string sql = "update Tanim set TanimAdi={0},AdSoyad={1},Email={2} where TanimID={3}";
            return context.Database.ExecuteSqlCommand(sql, tanim.TanimID, tanim.TanimGrupID, tanim.TanimAdi, tanim.TanimGrup, tanim.Kodu) > 0;
        }

        public List<Tanim> TanimListele(int tanimID)
        {
            return context.Tanim.Where(x => x.TanimID == tanimID).ToList();
        }

       

        public List<PocoTanimListesi> TanimListesi()
        {
            string sql = @"select 
                            s.TanimID,s.TanimAdi,s.TanimGrup,s.TanimGrupID,
                            k.TanimAdi as TanimGrubu,
                            br.TanimAdi as Birimi
                            from Tanim
                            left outer join Tanim k on(k.TanimID = s.TanimGrubuID)
                            left outer join Tanim br on(br.TanimID= s.TanimBirimID)
                            where s.Aktif= 1
                            order by s.TanimAdi";
            return context.Database.SqlQuery<PocoTanimListesi>(sql).ToList();
        }

        public IQueryable TanimListesi(int tanimGrubuId)
        {
            string sql = @"select 
                            s.TanimID,s.TanimAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as TanimGrubu,
                            br.TanimAdi as Birimi
                            from Tanimler
                            left outer join Tanim k on(k.TanimID = s.TanimGrubuID)
                            left outer join Tanim br on(br.TanimID= s.TanimBirimID)
                            where s.Aktif= 1 and TanimGrubuID={0} 
                            order by s.TanimAdi";
            return context.Database.SqlQuery<PocoTanimListesi>(sql, tanimGrubuId).AsQueryable();
        }

        public bool TanimSil(int tanimId)
        {
            return context.Database.ExecuteSqlCommand("delete from Tanim where TanimID={0}", tanimId) > 0;
        }
    }
}
