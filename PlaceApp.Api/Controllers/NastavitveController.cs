// Datoteka: Controllers/NastavitveController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaceApp.Api.Data;
using PlaceApp.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NastavitveController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NastavitveController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Nastavitve
        // Pridobi zadnje veljavne nastavitve in celotno dohodninsko lestvico
        [HttpGet]
        public async Task<ActionResult<object>> GetNastavitve()
        {
            var nastavitve = await _context.NastavitveObracuna
                                         .OrderByDescending(n => n.VeljavnoOd)
                                         .FirstOrDefaultAsync();

            var lestvica = await _context.DohodninskeLestvice
                                         .OrderBy(l => l.ZaporednaSt)
                                         .ToListAsync();

            // Vrnemo enoten objekt, ki vsebuje oboje
            return Ok(new { nastavitve, lestvica });
        }

        // POST: api/Nastavitve
        // Shrani nov zapis o splošnih nastavitvah. Ustvari novega, stare ohrani za zgodovino.
        [HttpPost]
        public async Task<ActionResult> PostNastavitve([FromBody] NastavitveObracuna noveNastavitve)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Nastavimo ID na 0, da baza ve, da gre za nov vnos
            noveNastavitve.Id = 0;
            _context.NastavitveObracuna.Add(noveNastavitve);
            await _context.SaveChangesAsync();

            return Ok(noveNastavitve);
        }

        // POST: api/Nastavitve/Lestvica
        // Zamenja celotno dohodninsko lestvico z novo.
        [HttpPost("lestvica")]
        public async Task<ActionResult> PostLestvica([FromBody] List<DohodninskaLestvica> novaLestvica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Najprej pobrišemo staro lestvico
            var staraLestvica = await _context.DohodninskeLestvice.ToListAsync();
            _context.DohodninskeLestvice.RemoveRange(staraLestvica);

            // Dodamo nove vnose (in ponastavimo ID-je, če so bili poslani)
            foreach (var item in novaLestvica)
            {
                item.Id = 0;
            }
            await _context.DohodninskeLestvice.AddRangeAsync(novaLestvica);

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}