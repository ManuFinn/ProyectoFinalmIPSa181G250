using APIalumnos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIalumnos.Repositories
{
    public class AvisosRepository : Repository<Avisostable>
    {
        public AvisosRepository(DbContext context) : base(context) { }

        //Context.Table.Include(x => x.NavigationProperty).LinqMethod(...);

        public override IEnumerable<Avisostable> GetAll()
        {
            return Context.Set<Avisostable>()
                .Include(x => x.IdDocenteAvisoNavigation)
                .Include(x => x.IdMateriaAvisoNavigation)
                .OrderBy(x=> x.Id);
        }

        public IEnumerable<Avisostable> GetbyId(int id)
        {
            return Context
                .Set<Avisostable>()
                .Include(x => x.IdDocenteAvisoNavigation)
                .Include(x => x.IdMateriaAvisoNavigation)
                .Where(x => x.Id == id);
        }

        public IEnumerable<Avisostable> GetByMateria(string materia)
        {
            return Context
                .Set<Avisostable>()
                .Include(x => x.IdDocenteAvisoNavigation)
                .Include(x => x.IdMateriaAvisoNavigation)
                .Where(x => x.IdMateriaAvisoNavigation.NombreMateria == materia);
        }

        public override void Insert(Avisostable entity)
        {
            entity.Fecha = DateTime.Now.ToMexicoTime().AddHours(-1);
            entity.Id = 0;
            entity.FechaUltAct = null;
            base.Insert(entity);
        }

        public override void Update(Avisostable entity)
        {
            entity.FechaUltAct = DateTime.Now.ToMexicoTime().AddHours(-1);
            base.Update(entity);
        }

        public override void Delete(Avisostable entity)
        {
            base.Delete(entity);
        }

        public override bool IsValid(Avisostable entity, out List<string> validationErrors)
        {
            validationErrors = new List<string>();

            DateTime fechaActual = DateTime.Now;

            if (string.IsNullOrEmpty(entity.MensajeAviso))
            {
                validationErrors.Add("Error 418. Prepara esas nalgas porque...");
            }
            return validationErrors.Count == 0;
        }
    }
}
