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
    public class EfFirmaRepository : EfGenericRepository<Firma>, IFirmaRepository
    {
        public EfFirmaRepository():base()
        {
            
        }

        public bool FirmaGuncelle(Firma firma)
        {
            const string sql = "update Firma set FirmaAdi={0},AdSoyad={1},Email={2} where FirmaID={3}";
            return context.Database.ExecuteSqlCommand(sql, firma.FirmaAdi, firma.AdSoyad, firma.Email, firma.FirmaID) > 0;
        }

        public List<Firma> FirmaListele(int firmaGrubuId)
        {
            return context.Firma.Where(x => x.FirmaGrubuID == firmaGrubuId).ToList();
        }

       

        public List<PocoFirmaListesi> FirmaListesi()
        {
            string sql = @"select 
                            s.FirmaID,s.FirmaAdi,s.AdSoyad,s.Email,
                           s.MusteriTuru,s.Aktif,s.CepTelefonu,s.VergiKimlikNo
                        
                            from Firmalar s
                           
                     
                            order by s.FirmaID";
            return context.Database.SqlQuery<PocoFirmaListesi>(sql).ToList();
        }

        public IQueryable FirmaListesi(int firmaGrubuId)
        {
            string sql = @"select 
                            s.FirmaID,s.FirmaAdi,s.AdSoyad,s.Email,
                            k.TanimAdi as FirmaGrubu,
                            br.TanimAdi as Birimi
                            from Firma s
                            left outer join Tanim k on(k.TanimID = s.FirmaGrubuID)
                            left outer join Tanim br on(br.TanimID= s.FirmaBirimID)
                            where s.Aktif= 1 and FirmaGrubuID={0} 
                            order by s.FirmaAdi";
            return context.Database.SqlQuery<PocoFirmaListesi>(sql,firmaGrubuId).AsQueryable();
        }

        public bool FirmaSil(int firmaId)
        {
            return context.Database.ExecuteSqlCommand("delete from Firma where FirmaID={0}", firmaId) > 0;
        }
    }
}
