using APIalumnos.Models;
using APIalumnos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIalumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        public tecContext Context { get; set; }

        AlumnoRepository repo;

        public AlumnosController(tecContext con)
        {
            Context = con;
            repo = new AlumnoRepository(Context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alumnos = repo.GetAll().Where(x => x.Borrado == false);
            return Ok(alumnos.Select(x => new
            {
                x.Id,
                x.NombreAlumno,
                x.Usuario
            }
            ));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var docente = repo.Get(id);
            if (docente != null)
            {
                return Ok(new
                {
                    docente.Id,
                    docente.NombreAlumno,
                    docente.Usuario
                }
            );
            }
            else
            {
                return NotFound();
            }
        }

    }
}
