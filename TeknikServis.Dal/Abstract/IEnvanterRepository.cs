using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Dal.Abstract
{
    public interface IEnvanterRepository : IGenericRepository<Envanter>
    {
        List<Envanter> EnvanterListele(int EnvanterGrubuId);

        List<PocoEnvanterListesi> EnvanterListesi();
        IQueryable EnvanterListesi(int EnvanterGrubuId);

        bool EnvanterSil(int EnvanterId);
        bool EnvanterGuncelle(Envanter Envanter);

       
    }
}
