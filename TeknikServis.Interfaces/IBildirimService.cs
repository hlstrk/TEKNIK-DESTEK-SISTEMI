using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeknikServis.Interfaces;

namespace TeknikServis.Interfaces
{
    public interface IBildirimService : IGenericService<Bildirim>
    {
        List<Bildirim> BildirimListele(int bildirimGrubuId);

        List<PocoBildirimListesi> BildirimListesi();
        IQueryable BildirimListesi(int bildirimGrubuId);

        bool BildirimSil(int bildirimId);
        bool BildirimGuncelle(Bildirim Bildirim);

       

      
    }
}
