using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Dal.Abstract
{
    public interface IIslemRepository : IGenericRepository<Islem>
    {
        List<Islem> IslemListele(int IslemGrubuId);

        List<PocoIslemListesi> IslemListesi();
        IQueryable IslemListesi(int IslemGrubuId);

        bool IslemSil(int IslemId);
        bool IslemGuncelle(Islem Islem);

       
    }
}
