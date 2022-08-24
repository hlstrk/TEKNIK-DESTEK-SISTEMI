using TeknikServis.Bll;
using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using TeknikServis.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeknikServis.Dal.Abstract;

namespace TeknikServis.Bll
{
    public class TeklifDetayManager : GenericManager<TeklifDetay>, ITeklifDetayService
    {
        ITeklifDetayRepository teklifdetayRepository;

        public TeklifDetayManager(ITeklifDetayRepository teklifdetayRepository) : base(teklifdetayRepository)
        {
            this.teklifdetayRepository = teklifdetayRepository;
        }

        public bool TeklifDetayGuncelle(TeklifDetay teklifdetay)
        {
            return teklifdetayRepository.TeklifDetayGuncelle(teklifdetay);
        }

        public List<TeklifDetay> TeklifDetayListele(int teklifdetayGrubuId)
        {
            return teklifdetayRepository.TeklifDetayListele(teklifdetayGrubuId);
        }

        public List<PocoTeklifDetayListesi> TeklifDetayListele2(int teklifdetayGrubuId)
        {
            return teklifdetayRepository.TeklifDetayListele2(teklifdetayGrubuId);
        }

        public List<PocoTeklifDetayListesi> TeklifDetayListesi()
        {
            return teklifdetayRepository.TeklifDetayListesi();
        }

        public IQueryable TeklifDetayListesi(int teklifdetayGrubuId)
        {
            return teklifdetayRepository.TeklifDetayListesi(teklifdetayGrubuId);
        }

        public bool TeklifDetaySil(int teklifdetayId)
        {
            return teklifdetayRepository.TeklifDetaySil(teklifdetayId);


        }
    }
}
