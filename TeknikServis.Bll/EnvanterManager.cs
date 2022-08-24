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
    public class EnvanterManager : GenericManager<Envanter>, IEnvanterService
    {
        IEnvanterRepository envanterRepository;

        public EnvanterManager(IEnvanterRepository envanterRepository) : base(envanterRepository)
        {
            this.envanterRepository = envanterRepository;
        }

        public bool EnvanterGuncelle(Envanter envanter)
        {
            return envanterRepository.EnvanterGuncelle(envanter);
        }

        public List<Envanter> EnvanterListele(int envanterGrubuId)
        {
            return envanterRepository.EnvanterListele(envanterGrubuId);
        }

       

        public List<PocoEnvanterListesi> EnvanterListesi()
        {
            return envanterRepository.EnvanterListesi();
        }

        public IQueryable EnvanterListesi(int envanterGrubuId)
        {
            return envanterRepository.EnvanterListesi(envanterGrubuId);
        }

        public bool EnvanterSil(int envanterId)
        {
            return envanterRepository.EnvanterSil(envanterId);


        }
    }
}
