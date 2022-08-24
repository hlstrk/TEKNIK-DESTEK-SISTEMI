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
    public class NotManager : GenericManager<Not>, INotService
    {
        INotRepository notRepository;

        public NotManager(INotRepository notRepository) : base(notRepository)
        {
            this.notRepository = notRepository;
        }

        public bool NotGuncelle(Not not)
        {
            return notRepository.NotGuncelle(not);
        }

        public List<Not> NotListele(int notGrubuId)
        {
            return notRepository.NotListele(notGrubuId);
        }

       

        public List<PocoNotListesi> NotListesi()
        {
            return notRepository.NotListesi();
        }

        public IQueryable NotListesi(int notGrubuId)
        {
            return notRepository.NotListesi(notGrubuId);
        }

        public bool NotSil(int notId)
        {
            return notRepository.NotSil(notId);


        }
    }
}
