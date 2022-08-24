using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Dal.Abstract
{
    public interface IStokRepository : IGenericRepository<Stok>
    {
        List<Stok> StokListele(int StokGrubuId);

        List<PocoStokListesi> StokListesi();
        IQueryable StokListesi(int StokGrubuId);

        bool StokSil(int StokId);
        bool StokGuncelle(Stok Stok);

        List<PocoStokListesi> StokListele2(int StokGrubuId);
    }
}
