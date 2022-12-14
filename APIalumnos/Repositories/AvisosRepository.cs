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


        public override IEnumerable<Avisostable> GetAll()
        {
            return Context.Set<Avisostable>()
                .Include(x => x.IdDocenteAvisoNavigation)
                .Include(x => x.IdMateriaAvisoNavigation)
                .OrderBy(x=> x.Id);
        }



        public Avisostable? GetbyId(int id)
        {
            return Context
                .Set<Avisostable>()
                .Include(x => x.IdDocenteAvisoNavigation)
                .Include(x => x.IdMateriaAvisoNavigation)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Avisostable> GetByMateria(string materia)
        {
            return Context
                .Set<Avisostable>()
                .Include(x => x.IdDocenteAvisoNavigation)
                .Include(x => x.IdMateriaAvisoNavigation)
                .Where(x => x.IdMateriaAvisoNavigation.NombreMateria == materia);
        }

        public IEnumerable<Avisostable> GetByMaterias(int[] materias)
        {
            return Context
                .Set<Avisostable>()
                .Include(x => x.IdDocenteAvisoNavigation)
                .Include(x => x.IdMateriaAvisoNavigation)
                .Where(x => materias.Contains(x.IdMateriaAvisoNavigation.Id));
        }

        public IEnumerable<Avisostable> GetByDocente(int id)
        {
            return Context
                .Set<Avisostable>()
                .Include(x => x.IdDocenteAvisoNavigation)
                .Include(x => x.IdMateriaAvisoNavigation)
                .Where(x => x.IdDocenteAviso == id);
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
                validationErrors.Add("Error 418...");
            }
            return validationErrors.Count == 0;
        }
    }
}
