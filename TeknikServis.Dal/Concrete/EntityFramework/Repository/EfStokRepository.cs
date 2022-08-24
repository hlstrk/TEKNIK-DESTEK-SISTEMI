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
    public class EfStokRepository : EfGenericRepository<Stok>, IStokRepository
    {
        public EfStokRepository() : base()
        {

        }

        public bool StokGuncelle(Stok stok)
        {
            const string sql = "update Stok set StokAdi={0},AdSoyad={1},Email={2} where StokServisID={3}";
            return context.Database.ExecuteSqlCommand(sql, stok.StokID, stok.Aciklama, stok.StokKategoriID) > 0;
        }

        public List<Stok> StokListele(int stokservisID)
        {
            return context.Stok.Where(x => x.StokID == stokservisID).ToList();
        }

        public List<PocoStokListesi> StokListele2(int stokservisID)
        {
            return context.Stok.Where(x => x.StokID == stokservisID)
                .Join(context.Tanim, s => s.StokID, t => t.TanimID, (s, t) => new { s, t })
                .Join(context.Tanim, s1 => s1.s.StokID, t1 => t1.TanimID, (s1, t1) => new PocoStokListesi()
                {



                    StokGrubu = s1.t.TanimAdi,
                    StokID = s1.s.StokID
                }).OrderBy(x => x.StokAdi).ThenBy(c => c.Birimi).ToList();


            //context.Stok.Include("FaturaDetay").Include("StokDepo")
        }

        public List<PocoStokListesi> StokListesi()
        {
            string sql = @"select 
                            s.StokServisID,s.StokAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as StokGrubu,
                            br.TanimAdi as Birimi
                            from Stokler
                            left outer join Tanim k on(k.TanimID = s.StokGrubuID)
                            left outer join Tanim br on(br.TanimID= s.StokBirimID)
                            where s.Aktif= 1
                            order by s.StokAdi";
            return context.Database.SqlQuery<PocoStokListesi>(sql).ToList();
        }

        public IQueryable StokListesi(int stokGrubuId)
        {
            string sql = @"select 
                            s.StokServisID,s.StokAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as StokGrubu,
                            br.TanimAdi as Birimi
                            from Stokler
                            left outer join Tanim k on(k.TanimID = s.StokGrubuID)
                            left outer join Tanim br on(br.TanimID= s.StokBirimID)
                            where s.Aktif= 1 and StokGrubuID={0} 
                            order by s.StokAdi";
            return context.Database.SqlQuery<PocoStokListesi>(sql, stokGrubuId).AsQueryable();
        }

        public bool StokSil(int stokId)
        {
            return context.Database.ExecuteSqlCommand("delete from Stok where StokServisID={0}", stokId) > 0;
        }
    }
}
