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
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetUsuarios()
        {
            if (_context?.usuario == null)
                return NotFound("No se encontró la entidad 'usuario'.");

            var usuarios = await _context.usuario
                .Include(u => u.Rol)
                .Select(u => new
                {
                    u.idusuario,
                    u.nombre,
                    u.login,
                    u.idrol
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        // POST: api/Usuario/login
        [HttpPost("login")]
        public async Task<ActionResult<object>> Login([FromBody] LoginModel model)
        {
            var usuario = await _context.usuario
                .FirstOrDefaultAsync(u => u.login == model.Usuario && u.clave == model.Clave);

            if (usuario == null)
                return Unauthorized("Usuario o clave incorrectos");

            return Ok(new { usuario.idusuario, usuario.nombre, usuario.login, usuario.idrol });
        }

        // POST: api/Usuario/registrar
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] Usuario dto)
        {
            if (dto.idrol != 2 && dto.idrol != 3)
                return BadRequest("Rol inválido. Solo Estudiante o Profesor.");

            var usuario = new Usuario
            {
                nombre = dto.nombre,
                login = dto.login,
                clave = dto.clave,
                idrol = dto.idrol
            };

            _context.usuario.Add(usuario);
            await _context.SaveChangesAsync();

            await _context.Entry(usuario).Reference(u => u.Rol).LoadAsync();

            return Ok(new
            {
                usuario.idusuario,
                usuario.nombre,
                usuario.login,
                usuario.idrol
            });
        }

        public class LoginModel
        {
            public string Usuario { get; set; }
            public string Clave { get; set; }
        }
    }
}
