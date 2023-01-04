using APIalumnos.Models;
using APIalumnos.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace APIalumnos.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class MensajesController : ControllerBase
    {
        public itesrcne_jeancarloContext Context { get; set; }

        MensajeRepository repo;

        public MensajesController(itesrcne_jeancarloContext con)
        {
            Context = con;
            repo = new MensajeRepository(Context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var mensaje = repo.GetAll();
            return Ok(mensaje.Select(x => new
            {
                x.Id,
                x.Mensaje,
                x.Fecha,
                x.IdDocente,
                x.IdDocenteNavigation.NombreDocente,
                x.IdAlumno,
                x.IdAlumnoNavigation.NombreAlumno,
            }
            ));
        }

        [HttpGet("GetByDocente/{idDocente}")]
        public IActionResult GetByDocente(int idDocente)
        {
            var aviso = repo.GetByDocente(idDocente);
            if (aviso != null)
            {
                return Ok(aviso.Select(x => new
                {
                    x.Id,
                    x.Mensaje,
                    x.Fecha,
                    x.IdDocenteNavigation.NombreDocente,
                    x.IdAlumno,
                    x.IdAlumnoNavigation.NombreAlumno,
                }));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetByAlumno/{idAlumno}")]
        public IActionResult GetByAlumno(int idAlumno)
        {
            var aviso = repo.GetByAlumno(idAlumno);
            if (aviso != null)
            {
                return Ok(aviso.Select(x => new
                {
                    x.Id,
                    x.Mensaje,
                    x.Fecha,
                    x.IdDocente,
                    x.IdDocenteNavigation.NombreDocente,
                    x.IdAlumno,
                    x.IdAlumnoNavigation.NombreAlumno,
                }));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("DocenteByAlumno/{idDocente}-{idAlumno}")]
        public IActionResult DocenteByAlumno(int idAlumno, int idDocente)
        {
            var aviso = repo.DocenteByAlumno(idAlumno, idDocente);
            if (aviso != null)
            {
                return Ok(aviso.Select(x => new
                {
                    x.Id,
                    x.Mensaje,
                    x.Fecha,
                    x.IdDocente,
                    x.IdDocenteNavigation.NombreDocente,
                    x.IdAlumno,
                    x.IdAlumnoNavigation.NombreAlumno,
                }));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("AgregarMensaje")]
        public IActionResult Post([FromBody] Mensajestable mv)
        {
            try
            {
                if (repo.IsValid(mv, out List<string> errores))
                {
                    repo.Insert(mv);
                    return Ok(new
                    {
                        mv.Id,
                        mv.Fecha,
                        mv.Mensaje,
                        mv.IdDocente,
                        mv.IdAlumno,
                    });
                }
                else { return Ok(); }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) { return StatusCode(500, ex.InnerException.Message); }
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("EliminarMensaje")]
        public IActionResult Delete([FromBody] Mensajestable mv)
        {
            try
            {
                var mensaje = repo.GetPut(mv.Id);
                if (mensaje != null)
                {
                    repo.Delete(mensaje);
                    return Ok();
                }
                else { NotFound(); }

                return BadRequest();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) { return StatusCode(500, ex.InnerException.Message); }
                return StatusCode(500, ex.Message);
            }
        }
    }
}
