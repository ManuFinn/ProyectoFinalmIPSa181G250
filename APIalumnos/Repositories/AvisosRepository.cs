using APIalumnos.Models;
using Microsoft.EntityFrameworkCore;

namespace APIalumnos.Repositories
{
    public class AvisosRepository : Repository<Avisostable>
    {
        public AvisosRepository(DbContext context) : base(context) { }

        public override IEnumerable<Avisostable> GetAll()
        {
            return base.GetAll();
        }

        public override void Insert(Avisostable entity)
        {
            entity.Fecha = DateTime.Now.ToMexicoTime();
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

            if(string.IsNullOrEmpty(entity.MensajeAviso) || entity.Fecha != fechaActual)
            {
                validationErrors.Add("Error 418. Prepara esas nalgas porque...");
            }
            return validationErrors.Count == 0;
        }
    }
}
