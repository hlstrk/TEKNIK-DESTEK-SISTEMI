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
    public class IslemManager : GenericManager<Islem>, IIslemService
    {
        IIslemRepository islemRepository;

        public IslemManager(IIslemRepository islemRepository) : base(islemRepository)
        {
            this.islemRepository = islemRepository;
        }

        public bool IslemGuncelle(Islem islem)
        {
            return islemRepository.IslemGuncelle(islem);
        }

        public List<Islem> IslemListele(int islemGrubuId)
        {
            return islemRepository.IslemListele(islemGrubuId);
        }

      

        public List<PocoIslemListesi> IslemListesi()
        {
            return islemRepository.IslemListesi();
        }

        public IQueryable IslemListesi(int islemGrubuId)
        {
            return islemRepository.IslemListesi(islemGrubuId);
        }

        public bool IslemSil(int islemId)
        {
            return islemRepository.IslemSil(islemId);


        }
    }
}
