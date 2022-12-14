using System;
using System.Collections.Generic;

#nullable disable

namespace APIalumnos.Models
{
    public partial class Docentestable
    {
        public Docentestable()
        {
            Avisostable = new HashSet<Avisostable>();
            Materiastable = new HashSet<Materiastable>();
            Mensajestable = new HashSet<Mensajestable>();
        }

        public int Id { get; set; }
        public string NombreDocente { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public bool Borrado { get; set; }

        public virtual ICollection<Avisostable> Avisostable { get; set; }
        public virtual ICollection<Materiastable> Materiastable { get; set; }
        public virtual ICollection<Mensajestable> Mensajestable { get; set; }
    }
}
