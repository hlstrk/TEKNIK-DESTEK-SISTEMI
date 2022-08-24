using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Dal.Abstract
{
    public interface IBildirimRepository : IGenericRepository<Bildirim>
    {
        List<Bildirim> BildirimListele(int BildirimGrubuId);

        List<PocoBildirimListesi> BildirimListesi();
        IQueryable BildirimListesi(int BildirimGrubuId);

        bool BildirimSil(int BildirimId);
        bool BildirimGuncelle(Bildirim Bildirim);

       
    }
}
