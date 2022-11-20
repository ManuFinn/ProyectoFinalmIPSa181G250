using APIalumnos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIalumnos.Repositories
{
    public class AlumnosXMateriasRepository : Repository<Alumnoxmateriatable>
    {
        public AlumnosXMateriasRepository(DbContext context) : base(context) { }

        public override IEnumerable<Alumnoxmateriatable> GetAll()
        {
            return Context.Set<Alumnoxmateriatable>()
                .Include(x => x.IdAlumnoNavigation)
                .Include(x => x.IdMateriaNavigation)
                .OrderBy(x => x.Id);
        }

        public  IEnumerable<Alumnoxmateriatable> GetByAlumno(int id)
        {
            return Context.Set<Alumnoxmateriatable>()
                .Include(x => x.IdAlumnoNavigation)
                .Include(x => x.IdMateriaNavigation)
                .Where(x => id == x.IdAlumnoNavigation.Id);
        }


    }
}
