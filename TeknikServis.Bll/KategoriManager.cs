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
    public class TanimManager : GenericManager<Tanim>, ITanimService
    {
        ITanimRepository tanimRepository;

        public TanimManager(ITanimRepository tanimRepository) : base(tanimRepository)
        {
            this.tanimRepository = tanimRepository;
        }

        public bool TanimGuncelle(Tanim tanim)
        {
            return tanimRepository.TanimGuncelle(tanim);
        }

        public List<Tanim> TanimListele(int tanimGrubuId)
        {
            return tanimRepository.TanimListele(tanimGrubuId);
        }

        public List<PocoTanimListesi> TanimListele2(int tanimGrubuId)
        {
            throw new NotImplementedException();
        }

        public List<PocoTanimListesi> TanimListesi()
        {
            return tanimRepository.TanimListesi();
        }

        public IQueryable TanimListesi(int tanimGrubuId)
        {
            return tanimRepository.TanimListesi(tanimGrubuId);
        }

        public bool TanimSil(int tanimId)
        {
            return tanimRepository.TanimSil(tanimId);


        }
    }
}
