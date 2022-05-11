using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace GymSysM.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            Clase = new HashSet<Clase>();
            Planilla = new HashSet<Planilla>();
        }

        public int IdEmpleado { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
        public int IdCategoriaEmpleado { get; set; }
        public string NSeguroSocial { get; set; }
        public string CuentaIban { get; set; }

        public virtual CategoriaEmpleado IdCategoriaEmpleadoNavigation { get; set; }
        public virtual ICollection<Clase> Clase { get; set; }
        public virtual ICollection<Planilla> Planilla { get; set; }
    }
}
