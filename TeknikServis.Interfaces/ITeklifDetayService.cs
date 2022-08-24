using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Interfaces
{
    public interface ITeklifDetayService : IGenericService<TeklifDetay>
    {
        List<TeklifDetay> TeklifDetayListele(int teklifdetayGrubuId);

        List<PocoTeklifDetayListesi> TeklifDetayListesi();
        IQueryable TeklifDetayListesi(int teklifdetayGrubuId);

        bool TeklifDetaySil(int teklifdetayId);
        bool TeklifDetayGuncelle(TeklifDetay TeklifDetay);

        List<PocoTeklifDetayListesi> TeklifDetayListele2(int teklifdetayGrubuId);


    }
}
