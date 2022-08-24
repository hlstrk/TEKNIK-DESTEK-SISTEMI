using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Dal.Abstract
{
    public interface IFirmaRepository : IGenericRepository<Firma>
    {
        List<Firma> FirmaListele(int firmaGrubuId);

        List<PocoFirmaListesi> FirmaListesi();
        IQueryable FirmaListesi(int firmaGrubuId);

        bool FirmaSil(int firmaId);
        bool FirmaGuncelle(Firma firma);


    }
}
