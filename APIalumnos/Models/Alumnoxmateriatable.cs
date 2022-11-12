using System;
using System.Collections.Generic;

#nullable disable

namespace APIalumnos.Models
{
    public partial class Alumnoxmateriatable
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdMateria { get; set; }

        public virtual Alumnostable IdAlumnoNavigation { get; set; }
        public virtual Materiastable IdMateriaNavigation { get; set; }
    }
}
