// Datoteka: PlaceApp.Api/Controllers/ZaposleniController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // <<< DODAJTE TO VRSTICO!
using PlaceApp.Api.Data;
using PlaceApp.Api.Models;

namespace PlaceApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZaposleniController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ZaposleniController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Zaposleni
        // Pridobi VSE zaposlene
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zaposlen>>> GetZaposleni()
        {
            // --- SPREMEMBA JE TUKAJ ---
            // Z metodo .Include() povemo Entity Frameworku, naj za vsakega zaposlenega
            // naloži tudi povezane podatke iz tabele Podjetje.
            var zaposleni = await _context.Zaposleni
                                          .Include(z => z.Podjetje)
                                          .ToListAsync();

            return Ok(zaposleni);
        }

        // ... preostanek datoteke ostane nespremenjen ...

        // GET: api/Zaposleni/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Zaposlen>> GetZaposlen(int id)
        {
            // Tudi tukaj je dobro dodati .Include(), če želite dobiti podjetje pri klicu posameznega zaposlenega
            var zaposlen = await _context.Zaposleni
                                         .Include(z => z.Podjetje)
                                         .FirstOrDefaultAsync(z => z.ID == id);

            if (zaposlen == null)
            {
                return NotFound();
            }

            return zaposlen;
        }

        // POST: api/Zaposleni
        [HttpPost]
        public async Task<ActionResult<Zaposlen>> PostZaposlen(Zaposlen zaposlen)
        {
            _context.Zaposleni.Add(zaposlen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZaposlen", new { id = zaposlen.ID }, zaposlen);
        }
    }
}