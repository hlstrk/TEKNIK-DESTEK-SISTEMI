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
    public class TeklifManager : GenericManager<Teklif>, ITeklifService
    {
        ITeklifRepository teklifRepository;

        public TeklifManager(ITeklifRepository teklifRepository) : base(teklifRepository)
        {
            this.teklifRepository = teklifRepository;
        }

        public bool TeklifGuncelle(Teklif teklif)
        {
            return teklifRepository.TeklifGuncelle(teklif);
        }

        public List<Teklif> TeklifListele(int teklifGrubuId)
        {
            return teklifRepository.TeklifListele(teklifGrubuId);
        }

        public List<PocoTeklifListesi> TeklifListele2(int teklifGrubuId)
        {
            return teklifRepository.TeklifListele2(teklifGrubuId);
        }

        public List<PocoTeklifListesi> TeklifListesi()
        {
            return teklifRepository.TeklifListesi();
        }

        public IQueryable TeklifListesi(int teklifGrubuId)
        {
            return teklifRepository.TeklifListesi(teklifGrubuId);
        }

        public bool TeklifSil(int teklifId)
        {
            return teklifRepository.TeklifSil(teklifId);


        }
    }
}
