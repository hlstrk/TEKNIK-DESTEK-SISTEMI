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
    public class TeknikManager : GenericManager<Teknik>, ITeknikService
    {
        ITeknikRepository teknikRepository;

        public TeknikManager(ITeknikRepository teknikRepository) : base(teknikRepository)
        {
            this.teknikRepository = teknikRepository;
        }

       

        public List<Teknik> TeknikListele(int teknikGrubuId)
        {
            return teknikRepository.TeknikListele(teknikGrubuId);
        }

        public List<PocoTeknikListesi> TeknikListele2(int teknikGrubuId)
        {
            return teknikRepository.TeknikListele2(teknikGrubuId);
        }

        public List<PocoTeknikListesi> TeknikListesi()
        {
            return teknikRepository.TeknikListesi();
        }

        public IQueryable TeknikListesi(int teknikGrubuId)
        {
            return teknikRepository.TeknikListesi(teknikGrubuId);
        }

        public bool TeknikSil(int teknikId)
        {
            return teknikRepository.TeknikSil(teknikId);

            
        }
    }
}
