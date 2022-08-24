using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Interfaces
{
    public interface INotService : IGenericService<Not>
    {
        List<Not> NotListele(int notGrubuId);

        List<PocoNotListesi> NotListesi();
        IQueryable NotListesi(int notGrubuId);

        bool NotSil(int notId);
        bool NotGuncelle(Not Not);

       


    }
}
