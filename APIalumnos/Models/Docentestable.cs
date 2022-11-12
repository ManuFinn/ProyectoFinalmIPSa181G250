using System;
using System.Collections.Generic;

#nullable disable

namespace APIalumnos.Models
{
    public partial class Docentestable
    {
        public Docentestable()
        {
            Materiastable = new HashSet<Materiastable>();
        }

        public int Id { get; set; }
        public string NombreDocente { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }

        public virtual ICollection<Materiastable> Materiastable { get; set; }
    }
}
