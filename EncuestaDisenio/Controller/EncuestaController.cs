using EncuestaDisenio.DATA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace EncuestaDisenio.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncuestaController : ControllerBase
    {
        private readonly ApplicationDbContextEncuesta _context;

        public EncuestaController(ApplicationDbContextEncuesta context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Respuesta>>> GetEncuestas()
        {
            var encuestas = await _context.respuestas
                .Include(r => r.Sexo)
                .Include(r => r.Departamento)
                .Include(r => r.Ciudad)
                .Include(r => r.Facultad)
                .Include(r => r.Carrera)
                .Include(r => r.Matricula)
                .Include(r => r.Becado)
                .Include(r => r.Xii)
                .Include(r => r.Xiii)
                .Include(r => r.Xiv)
                .Include(r => r.Xv)
                .Include(r => r.Xvi)
                .Include(r => r.Xvii)
                .ToListAsync();

            return Ok(encuestas);
        }

        // POST: api/Encuesta
        [HttpPost]
        public async Task<ActionResult<Respuesta>> PostEncuesta(Respuesta respuesta)
        {
            if (respuesta == null)
                return BadRequest("No se recibió ninguna respuesta.");

            try
            {
                _context.respuestas.Add(respuesta);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEncuestas), new { id = respuesta.Numero }, respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error guardando la encuesta: " + ex.Message);
            }
        }


    }
}

