using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Interfaces
{
    public interface ITeknikService : IGenericService<Teknik>
    {
        List<Teknik> TeknikListele(int teknikGrubuId);

        List<PocoTeknikListesi> TeknikListesi();
        IQueryable TeknikListesi(int teknikGrubuId);

        bool TeknikSil(int teknikId);
     

        List<PocoTeknikListesi> TeknikListele2(int teknikGrubuId);

      
    }
}
