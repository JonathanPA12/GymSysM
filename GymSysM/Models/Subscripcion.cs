using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace GymSysM.Models
{
    public partial class Subscripcion
    {
        public Subscripcion()
        {
            Cliente = new HashSet<Cliente>();
        }

        public int IdSubscripcion { get; set; }
        public string Nombre { get; set; }
        public int Meses { get; set; }
        public decimal Tarifa { get; set; }
        public string Descripcion { get; set; }
        public int CantidadSesionesUva { get; set; }

        public virtual ICollection<Cliente> Cliente { get; set; }
    }
}
