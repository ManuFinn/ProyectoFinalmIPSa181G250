using APIalumnos.Models;
using Microsoft.EntityFrameworkCore;

namespace APIalumnos.Repositories
{
    public class MensajeRepository : Repository<Mensajestable>
    {
        public MensajeRepository(DbContext context) : base(context) { }

        public override IEnumerable<Mensajestable> GetAll()
        {
            return Context.Set<Mensajestable>()
                .Include(x => x.IdDocenteNavigation)
                .Include(x => x.IdAlumnoNavigation)
                .OrderBy(x => x.Id);
        }

        public IEnumerable<Mensajestable> GetByDocente(int idDocente)
        {
            return Context
                 .Set<Mensajestable>()
                 .Include(x => x.IdDocenteNavigation)
                 .Include(x => x.IdAlumnoNavigation)
                 .Where(x => x.IdDocente == idDocente);
        }

        public IEnumerable<Mensajestable> GetByAlumno(int idAlumno)
        {
            return Context
                .Set<Mensajestable>()
                .Include(x => x.IdDocenteNavigation)
                .Include(x => x.IdAlumnoNavigation)
                .Where(x => x.IdAlumno == idAlumno);
        }

        public IEnumerable<Mensajestable> DocenteByAlumno(int idAlumno, int idDocente)
        {
            return Context
                .Set<Mensajestable>()
                .Include(x => x.IdDocenteNavigation)
                .Include(x => x.IdAlumnoNavigation)
                .Where(x => x.IdAlumno == idAlumno && x.IdDocente == idDocente);
        }

        public override void Insert(Mensajestable entity)
        {
            entity.Fecha = DateTime.Now.ToMexicoTime().AddHours(-1);
            entity.Id = 0;
            base.Insert(entity);
        }

        public override void Delete(Mensajestable entity)
        {
            base.Delete(entity);
        }

        public override bool IsValid(Mensajestable entity, out List<string> validationErrors)
        {
            validationErrors = new List<string>();

            DateTime fechaActual = DateTime.Now;

            if (string.IsNullOrEmpty(entity.Mensaje))
            {
                validationErrors.Add("Error 418...");
            }
            return validationErrors.Count == 0;
        }

    }
}
