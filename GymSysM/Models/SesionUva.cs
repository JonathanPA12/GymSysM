using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace GymSysM.Models
{
    public partial class SesionUva
    {
        public int IdSesionUva { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan Duracion { get; set; }
        public decimal Tarifa { get; set; }
        public int IdCliente { get; set; }
        public int IdEmpleado { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
    }
}
