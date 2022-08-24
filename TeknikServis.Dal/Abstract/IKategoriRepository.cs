using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Dal.Abstract
{
    public interface ITanimRepository : IGenericRepository<Tanim>
    {
        List<Tanim> TanimListele(int TanimGrubuId);

        List<PocoTanimListesi> TanimListesi();
        IQueryable TanimListesi(int TanimGrubuId);

        bool TanimSil(int TanimId);
        bool TanimGuncelle(Tanim Tanim);

      
    }
}
