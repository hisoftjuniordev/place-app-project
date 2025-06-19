// Datoteka: Controllers/PlacaController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaceApp.Api.Data;
using PlaceApp.Api.Models;

namespace PlaceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlacaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Zaposleni/5/Place  (To je primer, kako bi lahko dobili plače za zaposlenega)
        [HttpGet("/api/Zaposleni/{zaposlenId}/Place")]
        public async Task<ActionResult<IEnumerable<Placa>>> GetPlaceZaZaposlenega(int zaposlenId)
        {
            return await _context.Place.Where(p => p.ZaposlenId == zaposlenId).ToListAsync();
        }

        // POST: api/Placa
        [HttpPost]
        public async Task<ActionResult<Placa>> PostPlaca(Placa placa)
        {
            _context.Place.Add(placa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlaca", new { id = placa.Id }, placa);
        }

        // Pomožna GET metoda za CreatedAtAction
        [HttpGet("{id}")]
        public async Task<ActionResult<Placa>> GetPlaca(int id)
        {
            var placa = await _context.Place.FindAsync(id);
            if (placa == null) return NotFound();
            return placa;
        }
    }
}