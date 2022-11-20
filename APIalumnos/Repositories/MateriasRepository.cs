using APIalumnos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIalumnos.Repositories
{
    public class MateriasRepository : Repository<Materiastable>
    {
        public MateriasRepository(DbContext context) : base(context) { }

        public override IEnumerable<Materiastable> GetAll()
        {
            return Context.Set<Materiastable>()
                .Include(x => x.IdProfesorMateriaNavigation)
                .OrderBy(x => x.Id);
        }

        public IEnumerable<Materiastable> GetByDocente(int id)
        {
            return Context.Set<Materiastable>()
                .Include(x => x.IdProfesorMateriaNavigation)
                .Where(x => id == x.IdProfesorMateriaNavigation.Id);
        }

    }
}
