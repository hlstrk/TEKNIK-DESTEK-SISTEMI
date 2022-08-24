using LinqKit;

using OfficeOpenXml;
using TeknikServis.Bll;
using TeknikServis.Dal.Concrete.EntityFramework.Repository;
using TeknikServis.Entittes.Models;
using TeknikServis.Interfaces;
using TeknikServis.MvcUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Threading.Tasks;

namespace TeknikServis.MvcUI.Controllers
{
    [System.Web.Http.AllowAnonymous]


    public class MobilController : ApiController
    {

        CacheFonsiyon cacheFonsiyon;
        IBildirimService bildirimService = new BildirimManager(new EfBildirimRepository());
        IGenericService<Bildirim> genericService1 = new GenericManager<Bildirim>(new EfGenericRepository<Bildirim>());





        IKullaniciService KullaniciService = new KullaniciManager(new EfKullaniciRepository());








        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> BildirimSil([FromBody] Bildirim _bildirim)

        {
            var model = bildirimService.Remove(_bildirim.bildirimID);



            return null;
        }


        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> BildirimGuncelle([FromBody] Bildirim _bildirim)
        {
            if (_bildirim == null)
            {
                return BadRequest();
            }
            var model = bildirimService.Update(_bildirim);

            if (model == null)
            {
                return NotFound();
            }


            return null;
        }


        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> BildirimEkle([FromBody] Bildirim _bildirim)
        {
            if (_bildirim == null)
            {
                return BadRequest();
            }
            var model = bildirimService.Add(_bildirim);

            if (model == null)
            {
                return NotFound();
            }


            return Ok(model);
        }


        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> BildirimGetir()
        {

            var predicate = PredicateBuilder.True<Bildirim>();



            predicate = predicate.And(x => x.bildirimIcerigi != null);
           

            var model = bildirimService.GetAll(predicate);

            if (model == null)
            {
                return NotFound();
            }

            //await SayacAzalt(model[0].bildirimID);


            return Ok(model);
        }
    






}
 }
