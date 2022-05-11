using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace GymSysM.Models
{
    public partial class Clase
    {
        public Clase()
        {
            Matricula = new HashSet<Matricula>();
        }

        public int IdClase { get; set; }
        public int IdActividad { get; set; }
        public int IdSala { get; set; }
        public int IdEmpleado { get; set; }
        public string Dia { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int Capacidad { get; set; }

        public virtual Actividad IdActividadNavigation { get; set; }
        public virtual Empleado IdEmpleadoNavigation { get; set; }
        public virtual Sala IdSalaNavigation { get; set; }
        public virtual ICollection<Matricula> Matricula { get; set; }
    }
}
