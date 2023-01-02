using APIalumnos.Models;
using APIalumnos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIalumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriasController : ControllerBase
    {
        public itesrcne_jeancarloContext Context { get; set; }


        MateriasRepository repo;

        public MateriasController(itesrcne_jeancarloContext con)
        {
            Context = con;
            repo = new MateriasRepository(Context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var listado = repo.GetAll();
            return Ok(listado.Select(x => new
            {
                x.Id,
                x.NombreMateria,
                x.IdProfesorMateriaNavigation.NombreDocente,
                x.IdProfesorMateria
            }
            ));
        }

        [HttpGet("GetByDocente/{id}")]
        public IActionResult Get(int id)
        {
            var listado = repo.GetByDocente(id);
            if (listado != null)
            {
                return Ok(listado.Select(x => new
                {
                    x.Id,
                    x.NombreMateria,
                    x.IdProfesorMateriaNavigation.NombreDocente,
                    x.IdProfesorMateria
                }));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
