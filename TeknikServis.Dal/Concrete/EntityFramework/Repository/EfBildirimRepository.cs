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
    public class EfBildirimRepository : EfGenericRepository<Bildirim>, IBildirimRepository
    {
        public EfBildirimRepository() : base()
        {

        }

        public bool BildirimGuncelle(Bildirim bildirim)
        {
            const string sql = "update Bildirim set BildirimAdi={0},AdSoyad={1},Email={2} where BildirimID={3}";
            return context.Database.ExecuteSqlCommand(sql, bildirim.bildirimID, bildirim.musteriBeyani, bildirim.bildirimIcerigi) > 0;
        }

        public List<Bildirim> BildirimListele(int bildirimservisID)
        {
            return context.Bildirim.Where(x => x.bildirimID == bildirimservisID).ToList();
        }

       

        public List<PocoBildirimListesi> BildirimListesi()
        {
            string sql = @"select 
                            s.BildirimID,s.BildirimAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as BildirimGrubu,
                            br.TanimAdi as Birimi
                            from Bildirimler
                            left outer join Tanim k on(k.TanimID = s.BildirimGrubuID)
                            left outer join Tanim br on(br.TanimID= s.BildirimBirimID)
                            where s.Aktif= 1
                            order by s.BildirimAdi";
            return context.Database.SqlQuery<PocoBildirimListesi>(sql).ToList();
        }

        public IQueryable BildirimListesi(int bildirimGrubuId)
        {
            string sql = @"select 
                            s.BildirimID,s.BildirimAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as BildirimGrubu,
                            br.TanimAdi as Birimi
                            from Bildirimler
                            left outer join Tanim k on(k.TanimID = s.BildirimGrubuID)
                            left outer join Tanim br on(br.TanimID= s.BildirimBirimID)
                            where s.Aktif= 1 and BildirimGrubuID={0} 
                            order by s.BildirimAdi";
            return context.Database.SqlQuery<PocoBildirimListesi>(sql, bildirimGrubuId).AsQueryable();
        }

        public bool BildirimSil(int bildirimId)
        {
            return context.Database.ExecuteSqlCommand("delete from Bildirim where BildirimID={0}", bildirimId) > 0;
        }
    }
}
