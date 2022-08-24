using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Dal.Abstract
{
    public interface ITeknikRepository : IGenericRepository<Teknik>
    {
        List<Teknik> TeknikListele(int TeknikGrubuId);

        List<PocoTeknikListesi> TeknikListesi();
        IQueryable TeknikListesi(int TeknikGrubuId);

        bool TeknikSil(int TeknikId);
  

        List<PocoTeknikListesi> TeknikListele2(int TeknikGrubuId);
    }
}
