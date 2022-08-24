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
    public class FirmaManager : GenericManager<Firma>,IFirmaService
    {
        IFirmaRepository firmaRepository;

        public FirmaManager(IFirmaRepository firmaRepository):base(firmaRepository)
        {
            this.firmaRepository = firmaRepository;
        }

        public bool FirmaGuncelle(Firma firma)
        {
            return firmaRepository.FirmaGuncelle(firma);
        }

        public List<Firma> FirmaListele(int firmaGrubuId)
        {
            return firmaRepository.FirmaListele(firmaGrubuId);
        }

       

        public List<PocoFirmaListesi> FirmaListesi()
        {
            return firmaRepository.FirmaListesi();
        }

        public IQueryable FirmaListesi(int firmaGrubuId)
        {
            return firmaRepository.FirmaListesi(firmaGrubuId);
        }

        public bool FirmaSil(int firmaId)
        {
            return firmaRepository.FirmaSil(firmaId);
        }
    }
}
