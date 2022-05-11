using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace GymSysM.Models
{
    public partial class Matricula
    {
        public int IdMatricula { get; set; }
        public int IdCliente { get; set; }
        public int IdClase { get; set; }
        public DateTime FechaHora { get; set; }

        public virtual Clase IdClaseNavigation { get; set; }
        public virtual Cliente IdClienteNavigation { get; set; }
    }
}
