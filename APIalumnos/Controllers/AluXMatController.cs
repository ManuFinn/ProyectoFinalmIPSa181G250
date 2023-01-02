using APIalumnos.Models;
using APIalumnos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIalumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class AluXMatController : ControllerBase 
    {
        public itesrcne_jeancarloContext Context { get; set; }

        AlumnosXMateriasRepository repo;

        public AluXMatController(itesrcne_jeancarloContext con)
        {
            Context = con;
            repo = new AlumnosXMateriasRepository(Context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var listado = repo.GetAll();
            return Ok(listado.Select(x => new
            {
                x.Id,
                x.IdMateriaNavigation.NombreMateria,
                x.IdMateriaNavigation,
                x.IdAlumnoNavigation.NombreAlumno
            }
            ));
        }

        [HttpGet("GetByAlumno/{id}")]
        public IActionResult Get(int id)
        {
            var listado = repo.GetByAlumno(id);
            if (listado != null)
            {
                return Ok(listado.Select(x => new
                {
                    x.Id,
                    x.IdMateriaNavigation.NombreMateria,
                    x.IdMateria,
                    x.IdAlumnoNavigation.NombreAlumno
                }));
            }
            else
            {
                return NotFound();
            }   
        }


    }
}
