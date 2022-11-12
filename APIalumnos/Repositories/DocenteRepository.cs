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
    }
}
