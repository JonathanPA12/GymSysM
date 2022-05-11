using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace GymSysM.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Matricula = new HashSet<Matricula>();
            SesionUva = new HashSet<SesionUva>();
        }

        public int IdCliente { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        [Display(Name = "Fecha Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public bool? Estado { get; set; }
        public int IdSubscripcion { get; set; }
        [Display(Name = "Fecha Subscripcion")]
        [DataType(DataType.Date)]
        public DateTime FechaSubscripcion { get; set; }
        [Display(Name = "Fecha Renovacion")]
        [DataType(DataType.Date)]
        public DateTime? FechaRenovacion { get; set; }
        public int CantSesionesUva { get; set; }
        public int? SesionesUVAdisp { get; set; }

        public virtual Subscripcion IdSubscripcionNavigation { get; set; }
        public virtual ICollection<Matricula> Matricula { get; set; }
        public virtual ICollection<SesionUva> SesionUva { get; set; }
    }
}
