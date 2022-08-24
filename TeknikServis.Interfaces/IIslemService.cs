using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Interfaces
{
    public interface IIslemService : IGenericService<Islem>
    {
        List<Islem> IslemListele(int islemGrubuId);

        List<PocoIslemListesi> IslemListesi();
        IQueryable IslemListesi(int islemGrubuId);

        bool IslemSil(int islemId);
        bool IslemGuncelle(Islem Islem);

      


    }
}
