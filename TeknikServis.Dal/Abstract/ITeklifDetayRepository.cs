using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Dal.Abstract
{
    public interface ITeklifDetayRepository : IGenericRepository<TeklifDetay>
    {
        List<TeklifDetay> TeklifDetayListele(int TeklifDetayGrubuId);

        List<PocoTeklifDetayListesi> TeklifDetayListesi();
        IQueryable TeklifDetayListesi(int TeklifDetayGrubuId);

        bool TeklifDetaySil(int TeklifDetayId);
        bool TeklifDetayGuncelle(TeklifDetay TeklifDetay);

        List<PocoTeklifDetayListesi> TeklifDetayListele2(int TeklifDetayGrubuId);
    }
}
