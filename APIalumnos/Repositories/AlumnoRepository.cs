using APIalumnos.Models;
using Microsoft.EntityFrameworkCore;

namespace APIalumnos.Repositories
{
    public class AlumnoRepository : Repository<Alumnostable>
    {
        public AlumnoRepository(DbContext context) : base(context) { }

        public override IEnumerable<Alumnostable> GetAll()
        {
            return base.GetAll();
        }
    }
}
