using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Dal.Abstract
{
    public interface ITeklifRepository : IGenericRepository<Teklif>
    {
        List<Teklif> TeklifListele(int TeklifGrubuId);

        List<PocoTeklifListesi> TeklifListesi();
        IQueryable TeklifListesi(int TeklifGrubuId);

        bool TeklifSil(int TeklifId);
        bool TeklifGuncelle(Teklif Teklif);

        List<PocoTeklifListesi> TeklifListele2(int TeklifGrubuId);
    }
}
