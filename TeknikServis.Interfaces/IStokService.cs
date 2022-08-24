using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Interfaces
{
    public interface IStokService : IGenericService<Stok>
    {
        List<Stok> StokListele(int stokGrubuId);

        List<PocoStokListesi> StokListesi();
        IQueryable StokListesi(int stokGrubuId);

        bool StokSil(int stokId);
        bool StokGuncelle(Stok Stok);

        List<PocoStokListesi> StokListele2(int stokGrubuId);


    }
}
