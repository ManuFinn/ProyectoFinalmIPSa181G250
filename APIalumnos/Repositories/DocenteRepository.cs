using APIalumnos.Models;
using Microsoft.EntityFrameworkCore;

namespace APIalumnos.Repositories
{
    public class DocenteRepository : Repository<Docentestable>
    {
        public DocenteRepository(DbContext context): base(context) { }

        public override IEnumerable<Docentestable> GetAll()
        {
            return base.GetAll();
        }

        public override void Insert(Docentestable entity)
        {
            //entity.Fecha = DateTime.Now.ToMexicoTime();
            entity.Id = 0;
            base.Insert(entity);
        }
    }
}
