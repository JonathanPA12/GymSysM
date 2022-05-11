using System;
using System.Collections.Generic;
using System.ComponentModel;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace GymSysM.Models
{
    public partial class Planilla
    {
        public int IdPlanilla { get; set; }
        public int IdEmpleado { get; set; }
        public int HorasTrabajadas { get; set; }
        [ReadOnly(true)]
        public decimal? SalarioHora { get; set; }
        [ReadOnly(true)]
        public decimal? SalarioBruto { get; set; }
        [ReadOnly(true)]
        public decimal? SalarioNeto { get; set; }
        [ReadOnly(true)]
        public decimal? Ccss { get; set; }

        public virtual Empleado IdEmpleadoNavigation { get; set; }
    }
}
