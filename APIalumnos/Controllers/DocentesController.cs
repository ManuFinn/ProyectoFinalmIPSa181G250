using APIalumnos.Models;
using APIalumnos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIalumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocentesController : ControllerBase
    {
        public tecContext Context { get; set; }

        DocenteRepository repo;

        public DocentesController(tecContext con)
        {
            Context = con;
            repo = new DocenteRepository(Context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var docentes = repo.GetAll().Where(x => x.Borrado == false);
            return Ok(docentes.Select(x => new
            {
                x.Id,
                x.NombreDocente,
                x.Usuario
            }
            ));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var docente = repo.Get(id);
            if(docente != null)
            {
                return Ok(new
                {
                    docente.Id,
                    docente.NombreDocente,
                    docente.Usuario
                }
            );
            }
            else
            {
                return NotFound();
            }
            
        }

        //[HttpPost]
        //public IActionResult Post([FromBody] Docentestable av)
        //{
        //    try
        //    {
        //        repo.Insert(av);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.InnerException != null) { return StatusCode(500, ex.InnerException.Message); }
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}
