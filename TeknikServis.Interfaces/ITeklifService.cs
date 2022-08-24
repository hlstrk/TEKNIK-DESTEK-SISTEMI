using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Interfaces
{
    public interface ITeklifService : IGenericService<Teklif>
    {
        List<Teklif> TeklifListele(int teklifGrubuId);

        List<PocoTeklifListesi> TeklifListesi();
        IQueryable TeklifListesi(int teklifGrubuId);

        bool TeklifSil(int teklifId);
        bool TeklifGuncelle(Teklif Teklif);

        List<PocoTeklifListesi> TeklifListele2(int teklifGrubuId);


    }
}
