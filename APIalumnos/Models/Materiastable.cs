using System;
using System.Collections.Generic;

#nullable disable

namespace APIalumnos.Models
{
    public partial class Materiastable
    {
        public Materiastable()
        {
            Alumnoxmateriatable = new HashSet<Alumnoxmateriatable>();
            Avisostable = new HashSet<Avisostable>();
        }

        public int Id { get; set; }
        public string NombreMateria { get; set; }
        public int IdProfesorMateria { get; set; }

        public virtual Docentestable IdProfesorMateriaNavigation { get; set; }
        public virtual ICollection<Alumnoxmateriatable> Alumnoxmateriatable { get; set; }
        public virtual ICollection<Avisostable> Avisostable { get; set; }
    }
}
