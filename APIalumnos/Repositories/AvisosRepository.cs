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
    }
}
