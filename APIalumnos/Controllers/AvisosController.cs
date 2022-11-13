using APIalumnos.Models;
using APIalumnos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIalumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvisosController : ControllerBase
    {
        public tecContext Context { get; set; }

        AvisosRepository repo;

        public AvisosController(tecContext con)
        {
            Context = con;
            repo = new AvisosRepository(Context);
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
                x.IdMateriaAviso,
                x.IdDocenteAviso
            }
            ));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var aviso = repo.Get(id);
            return Ok( new
            {
                aviso.Id,
                aviso.MensajeAviso,
                aviso.Fecha,
                aviso.IdMateriaAviso,
                aviso.IdDocenteAviso
            }
            );
        }
    }
}
