// Datoteka: Controllers/EvidencaUrController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaceApp.Api.Data;
using PlaceApp.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvidencaUrController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EvidencaUrController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EvidencaUr/5/2025/6
        // Pridobi vse vnose evidence za določenega zaposlenega za določen mesec in leto
        [HttpGet("{zaposlenId}/{leto}/{mesec}")]
        public async Task<ActionResult<IEnumerable<EvidencaUra>>> GetEvidencaZaMesec(int zaposlenId, int leto, int mesec)
        {
            var evidence = await _context.EvidenceUr
                .Where(e => e.ZaposlenId == zaposlenId && e.Datum.Year == leto && e.Datum.Month == mesec)
                .OrderBy(e => e.Datum)
                .ToListAsync();

            return Ok(evidence);
        }

        // POST: api/EvidencaUr
        // Shrani enega ali več vnosov v evidenco.
        // To omogoča shranjevanje celotne mesečne evidence naenkrat.
        [HttpPost]
        public async Task<ActionResult> PostEvidencaUr([FromBody] List<EvidencaUra> vnosi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: V prihodnosti dodajte logiko za posodabljanje obstoječih vnosov,
            // namesto da vedno dodajate nove. Zaenkrat bomo samo dodajali.

            foreach (var vnos in vnosi)
            {
                vnos.Id = 0; // Zagotovimo, da EF Core ustvari nov zapis
            }

            await _context.EvidenceUr.AddRangeAsync(vnosi);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Vnosi uspešno shranjeni." });
        }
    }
}