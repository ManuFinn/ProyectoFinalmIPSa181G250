using System;
using System.Collections.Generic;

#nullable disable

namespace APIalumnos.Models
{
    public partial class Alumnostable
    {
        public Alumnostable()
        {
            Alumnoxmateriatable = new HashSet<Alumnoxmateriatable>();
            Mensajestable = new HashSet<Mensajestable>();
        }

        public int Id { get; set; }
        public string NombreAlumno { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public bool Borrado { get; set; }

        public virtual ICollection<Alumnoxmateriatable> Alumnoxmateriatable { get; set; }
        public virtual ICollection<Mensajestable> Mensajestable { get; set; }
    }
}
