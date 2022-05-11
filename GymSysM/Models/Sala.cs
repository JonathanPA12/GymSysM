using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace GymSysM.Models
{
    public partial class Sala
    {
        public Sala()
        {
            Clase = new HashSet<Clase>();
        }

        public int IdSala { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Capacidad { get; set; }

        public virtual ICollection<Clase> Clase { get; set; }
    }
}
