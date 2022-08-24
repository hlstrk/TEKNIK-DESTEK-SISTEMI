using TeknikServis.Dal.Abstract;
using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using TeknikServis.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Bll
{
    public class StokManager : GenericManager<Stok>, IStokService
    {
        IStokRepository stokRepository;

        public StokManager(IStokRepository stokRepository) : base(stokRepository)
        {
            this.stokRepository = stokRepository;
        }

        public bool StokGuncelle(Stok stok)
        {
            return stokRepository.StokGuncelle(stok);
        }

        public List<Stok> StokListele(int stokGrubuId)
        {
            return stokRepository.StokListele(stokGrubuId);
        }

        public List<PocoStokListesi> StokListele2(int stokGrubuId)
        {
            return stokRepository.StokListele2(stokGrubuId);
        }

        public List<PocoStokListesi> StokListesi()
        {
            return stokRepository.StokListesi();
        }

        public IQueryable StokListesi(int stokGrubuId)
        {
            return stokRepository.StokListesi(stokGrubuId);
        }

        public bool StokSil(int stokId)
        {
            return stokRepository.StokSil(stokId);


        }
    }
}
