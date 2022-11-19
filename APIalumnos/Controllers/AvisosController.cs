using APIalumnos.Models;
using APIalumnos.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
//using static System.Runtime.InteropServices.JavaScript.JSType;

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
                x.FechaUltAct,
                x.IdDocenteAvisoNavigation.NombreDocente,
                x.IdMateriaAvisoNavigation.NombreMateria
            }
            ));
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var aviso = repo.GetbyId(id);
            if (aviso != null)
            {
                return Ok(aviso.Select(x => new
                {
                    x.Id,
                    x.MensajeAviso,
                    x.Fecha,
                    x.FechaUltAct,
                    x.IdDocenteAvisoNavigation.NombreDocente,
                    x.IdMateriaAvisoNavigation.NombreMateria
                }));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetByMateria/{materia}")]
        public IActionResult GetByMateria(string materia)
        {
            var aviso = repo.GetByMateria(materia);
            if (aviso != null)
            {
                return Ok(aviso.Select(x => new
                {
                    x.Id,
                    x.MensajeAviso,
                    x.Fecha,
                    x.FechaUltAct,
                    x.IdDocenteAvisoNavigation.NombreDocente,
                    x.IdMateriaAvisoNavigation.NombreMateria
                }));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Avisostable av)
        {
            try
            {
                if (repo.IsValid(av, out List<string> errores))
                {
                    repo.Insert(av);
                    return Ok(new
                    {
                        av.Id,
                        av.Fecha,
                        av.MensajeAviso,
                        av.IdDocenteAviso,
                        av.IdMateriaAviso
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

        [HttpPut]
        public IActionResult Put([FromBody] Avisostable av)
        {
            try
            {
                var aviso = repo.GetPut(av.Id);
                if (aviso != null)
                {
                    if (repo.IsValid(av, out List<string> errores))
                    {
                        aviso.MensajeAviso = av.MensajeAviso;
                        repo.Update(aviso);
                        return Ok();
                    }
                    else { return BadRequest(errores); }
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

        [HttpDelete]
        public IActionResult Delete([FromBody] Avisostable av)
        {
            try
            {
                var aviso = repo.GetPut(av.Id);
                if (aviso != null)
                {
                        repo.Delete(aviso);
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
