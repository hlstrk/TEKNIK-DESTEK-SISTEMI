using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Dal.Abstract
{
    public interface INotRepository : IGenericRepository<Not>
    {
        List<Not> NotListele(int NotGrubuId);

        List<PocoNotListesi> NotListesi();
        IQueryable NotListesi(int NotGrubuId);

        bool NotSil(int NotId);
        bool NotGuncelle(Not Not);

       
    }
}
