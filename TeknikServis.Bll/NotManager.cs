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
    public class BildirimManager : GenericManager<Bildirim>, IBildirimService
    {
        IBildirimRepository bildirimRepository;

        public BildirimManager(IBildirimRepository bildirimRepository) : base(bildirimRepository)
        {
            this.bildirimRepository = bildirimRepository;
        }

        public bool BildirimGuncelle(Bildirim bildirim)
        {
            return bildirimRepository.BildirimGuncelle(bildirim);
        }

        public List<Bildirim> BildirimListele(int bildirimGrubuId)
        {
            return bildirimRepository.BildirimListele(bildirimGrubuId);
        }

       

        public List<PocoBildirimListesi> BildirimListesi()
        {
            return bildirimRepository.BildirimListesi();
        }

        public IQueryable BildirimListesi(int bildirimGrubuId)
        {
            return bildirimRepository.BildirimListesi(bildirimGrubuId);
        }

        public bool BildirimSil(int bildirimId)
        {
            return bildirimRepository.BildirimSil(bildirimId);


        }
    }
}
