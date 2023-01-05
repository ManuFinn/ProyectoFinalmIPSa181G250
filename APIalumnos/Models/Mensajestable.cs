using System;
using System.Collections.Generic;

#nullable disable

namespace APIalumnos.Models
{
    public partial class Mensajestable
    {
        public int Id { get; set; }
        public string Mensaje { get; set; }
        public DateTime? Fecha { get; set; }
        public int IdDocente { get; set; }
        public int IdAlumno { get; set; }

        public virtual Alumnostable IdAlumnoNavigation { get; set; }
        public virtual Docentestable IdDocenteNavigation { get; set; }
    }
}
