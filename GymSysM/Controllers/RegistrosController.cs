using GymSysM.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSysM.Controllers
{
    public class RegistrosController : Controller
    {
        private readonly BdGymContext _context;

        public RegistrosController(BdGymContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ClasesR()
        {
            return View();
        }
        public IActionResult ClientesR()
        {
            return View();
        }
        public IActionResult EmpleadosR()
        {
            return View();
        }
        public IEnumerable<Object> ConsultaClientes()
        {
            var client = from cl in _context.Cliente
                         orderby cl.Apellidos ascending
                         select cl;
            

            return client.ToList();
        }

        public IEnumerable<Object> ConsultaClientesSubs()
        {
            var clientSubs = from cl in _context.Cliente
                             join sub in _context.Subscripcion
                             on cl.IdSubscripcion equals sub.IdSubscripcion
                             orderby sub.Nombre ascending
                             select new
                             {
                                 sub.Nombre,
                                 cl.Dni,
                                 cl.Apellidos,
                                 nombreClnt = cl.Nombre,
                                 cl.Telefono,
                                 cl.Correo
                             };
                    
            return clientSubs;
        }

        public IEnumerable<Object> ConsultaClientesMats()
        {
            var clientMat = from cl in _context.Cliente
                            join mat in _context.Matricula
                            on cl.IdCliente equals mat.IdCliente
                            orderby mat.FechaHora ascending
                            select new
                            {
                                cl.Dni,
                                cl.Nombre,
                                cl.Apellidos,
                                cl.Telefono,
                                cl.Correo,
                                mat.FechaHora,
                                clase = mat.IdClaseNavigation.IdActividadNavigation.Nombre
                            };


            return clientMat.ToList();
        }

        public IEnumerable<Object> ConsultaEmpleados()
        {
            var emp = from em in _context.Empleado
                         orderby em.Apellidos ascending
                         select em;


            return emp.ToList();
        }

        public IEnumerable<Object> ConsultaEmpleadosCat()
        {
            var emp = from em in _context.Empleado
                      join cat in _context.CategoriaEmpleado
                      on em.IdCategoriaEmpleado equals cat.IdCategoriaEmpleado
                      orderby cat.Nombre ascending
                      select new
                      {
                          categoria = cat.Nombre,
                          em.Dni,
                          em.Nombre,
                          em.Apellidos,
                          em.Telefono,
                          em.Correo
                      };

            return emp.ToList();
        }

        public IEnumerable<Object> ConsultaEmpleadosClase()
        {
            var emp = from em in _context.Empleado
                      join cls in _context.Clase
                      on em.IdEmpleado equals cls.IdEmpleado
                      orderby cls.IdActividadNavigation.Nombre ascending
                      select new
                      {
                          clase = cls.IdActividadNavigation.Nombre,
                          em.Dni,
                          em.Nombre,
                          em.Apellidos,
                          em.Telefono,
                          em.Correo
                      };

            return emp.ToList();
        }

        public IEnumerable<Object> ConsultaEmpleadosPlani()
        {
            var emp = from em in _context.Empleado
                      join plni in _context.Planilla
                      on em.IdEmpleado equals plni.IdEmpleado
                      orderby em.Nombre ascending
                      select new
                      {
                          em.Nombre,
                          em.Apellidos,
                          em.Dni,
                          em.CuentaIban,
                          plni.SalarioHora,
                          plni.SalarioBruto,
                          plni.SalarioNeto
                      };

            return emp.ToList();
        }

        public IEnumerable<Object> ConsultaClases()
        {
            var classes = from clss in _context.Clase
                          orderby clss.IdActividadNavigation.Nombre ascending
                          select clss;


            return classes.ToList();
        }

        public IEnumerable<Object> ConsultaClasesAct()
        {
            var clasAct = from clss in _context.Clase
                          join act in _context.Actividad
                          on clss.IdActividad equals act.IdActividad
                          orderby clss.IdActividadNavigation.Nombre ascending
                          select new
                          {
                              actividad = act.Nombre,
                              sala = clss.IdSalaNavigation.Nombre,
                              instructor = clss.IdEmpleadoNavigation.Nombre,
                              clss.HoraInicio,
                              clss.HoraFin
                          };

            return clasAct.ToList();
        }

        public IEnumerable<Object> ConsultaClasesMat()
        {
            var clasMat = from clss in _context.Clase
                          join mat in _context.Matricula
                          on clss.IdClase equals mat.IdClase
                          orderby clss.IdActividadNavigation.Nombre ascending
                          select new
                          {
                              actividad = clss.IdActividadNavigation.Nombre,
                              sala = clss.IdSalaNavigation.Nombre,
                              instructor = clss.IdEmpleadoNavigation.Nombre,
                              DNIcliente = mat.IdClienteNavigation.Dni,
                              mat.FechaHora
                          };


            return clasMat.ToList();
        }
    }
}
