using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Interfaces
{
    public interface IEnvanterService : IGenericService<Envanter>
    {
        List<Envanter> EnvanterListele(int envanterGrubuId);

        List<PocoEnvanterListesi> EnvanterListesi();
        IQueryable EnvanterListesi(int envanterGrubuId);

        bool EnvanterSil(int envanterId);
        bool EnvanterGuncelle(Envanter Envanter);

       


    }
}
