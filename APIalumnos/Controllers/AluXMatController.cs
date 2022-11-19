using APIalumnos.Models;
using APIalumnos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIalumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AluXMatController : ControllerBase
    {
        public tecContext Context { get; set; }

        AlumnosXMateriasRepository repo;

        public AluXMatController(tecContext con)
        {
            Context = con;
            repo = new AlumnosXMateriasRepository(Context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var avisos = repo.GetAll();
            return Ok(avisos.Select(x => new
            {
                x.Id,
                x.MensajeAviso,
                x.Fecha,
                x.FechaUltAct,
                x.IdDocenteAvisoNavigation.NombreDocente,
                x.IdMateriaAvisoNavigation.NombreMateria
            }
            ));
        }
    }
}
