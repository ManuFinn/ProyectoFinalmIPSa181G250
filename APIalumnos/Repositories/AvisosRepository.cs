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
            return Context.Set<Avisostable>().Include(x => x.IdDocenteAvisoNavigation).Include(x => x.IdMateriaAvisoNavigation);
        }

        public override void Insert(Avisostable entity)
        {
            entity.Fecha = DateTime.Now.ToMexicoTime().AddHours(-1);
            entity.Id = 0;
            base.Insert(entity);
        }

        public override void Update(Avisostable entity)
        {
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

            //if(string.IsNullOrEmpty(entity.MensajeAviso) || entity.Fecha != fechaActual)
            //{
            //    validationErrors.Add("Error 418. Prepara esas nalgas porque...");
            //}
            return validationErrors.Count == 0;
        }
    }
}
