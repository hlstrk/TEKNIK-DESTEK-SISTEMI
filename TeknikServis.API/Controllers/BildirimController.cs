
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeknikServis.Bll;
using TeknikServis.Dal.Concrete.EntityFramework.Repository;
using TeknikServis.Entittes.Models;
using TeknikServis.Interfaces;

namespace TeknikServis.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BildirimController : ControllerBase
    {
        IBildirimService bildirimService = new BildirimManager(new EfBildirimRepository());
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<BildirimController> _logger;


        public BildirimController(ILogger<BildirimController> logger)
        {
            _logger = logger;
          
        }


        [HttpGet]
        public async Task<IActionResult> BildirimGetir()
        {
            
            var predicate = PredicateBuilder.True<Bildirim>();



          
        


            var model = bildirimService.GetAll(predicate);


            if (model == null)
            {
                return NotFound();
            }

            if(model.Count==0)
            {
                return Ok("Şu Anda Hiçbir Bildirim Yok");
            }
            
            return Ok(model);
        }
    }
}
