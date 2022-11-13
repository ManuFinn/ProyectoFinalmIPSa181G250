using System;
using System.Collections.Generic;

#nullable disable

namespace APIalumnos.Models
{
    public partial class Avisostable
    {
        public int Id { get; set; }
        public string MensajeAviso { get; set; }
        public DateTime Fecha { get; set; }
        public int IdMateriaAviso { get; set; }
        public int IdDocenteAviso { get; set; }

        public virtual Docentestable IdDocenteAvisoNavigation { get; set; }
        public virtual Materiastable IdMateriaAvisoNavigation { get; set; }
    }
}
