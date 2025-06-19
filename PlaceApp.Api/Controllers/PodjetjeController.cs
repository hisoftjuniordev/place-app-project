// Datoteka: PlaceApp.Api/Controllers/PodjetjeController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaceApp.Api.Data;
using PlaceApp.Api.Models;

namespace PlaceApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Pot do tega kontrolerja bo /api/Podjetje
    public class PodjetjeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Konstruktor, ki prejme DbContext, tako kot pri ZaposleniController
        public PodjetjeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Podjetje
        // Pridobi VSA podjetja
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Podjetje>>> GetPodjetja()
        {
            // Namesto _context.Zaposleni, uporabimo _context.Podjetja
            var podjetja = await _context.Podjetja.ToListAsync();
            return Ok(podjetja);
        }

        // GET: api/Podjetje/5
        // Pridobi eno podjetje po njegovem ID-ju (potrebno za POST metodo)
        [HttpGet("{id}")]
        public async Task<ActionResult<Podjetje>> GetPodjetje(int id)
        {
            var podjetje = await _context.Podjetja.FindAsync(id);

            if (podjetje == null)
            {
                return NotFound();
            }

            return podjetje;
        }


        // POST: api/Podjetje
        // Ustvari novo podjetje
        [HttpPost]
        public async Task<ActionResult<Podjetje>> PostPodjetje(Podjetje podjetje)
        {
            // Dodamo prejeto podjetje v bazo
            _context.Podjetja.Add(podjetje);
            // Shranimo spremembe
            await _context.SaveChangesAsync();

            // Vrnemo status 201 Created, skupaj z lokacijo novega vira in samim objektom
            // Fix for CS1061: Replace 'id' with 'ID' to match the property name in the Podjetje class.
            // Fix for IDE0037: Simplify member name by removing redundant quotes around "GetPodjetje".

            return CreatedAtAction(nameof(GetPodjetje), new { id = podjetje.ID }, podjetje);
            return CreatedAtAction("GetPodjetje", new { id = podjetje.ID }, podjetje);
        }
    }
}