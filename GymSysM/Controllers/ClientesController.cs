using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymSysM.Models;

namespace GymSysM.Controllers
{
    public class ClientesController : Controller
    {
        private readonly BdGymContext _context;

        public ClientesController(BdGymContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var bdGymContext = _context.Cliente.Include(c => c.IdSubscripcionNavigation);
            ConsultFchRenovacion();
            ConsultUVAdisp();
            return View(await bdGymContext.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.IdSubscripcionNavigation)
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["IdSubscripcion"] = new SelectList(_context.Subscripcion, "IdSubscripcion", "Nombre");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,Dni,Nombre,Apellidos,FechaNacimiento,Telefono,Direccion,Correo,Estado,IdSubscripcion,FechaSubscripcion,FechaRenovacion,CantSesionesUva,SesionesUVAdisp")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSubscripcion"] = new SelectList(_context.Subscripcion, "IdSubscripcion", "Nombre", cliente.IdSubscripcion);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["IdSubscripcion"] = new SelectList(_context.Subscripcion, "IdSubscripcion", "Nombre", cliente.IdSubscripcion);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,Dni,Nombre,Apellidos,FechaNacimiento,Telefono,Direccion,Correo,Estado,IdSubscripcion,FechaSubscripcion,FechaRenovacion,CantSesionesUva,SesionesUVAdisp")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.IdCliente))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSubscripcion"] = new SelectList(_context.Subscripcion, "IdSubscripcion", "Nombre", cliente.IdSubscripcion);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.IdSubscripcionNavigation)
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            if (cliente.Estado == true)
            {
                return BadRequest();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.IdCliente == id);
        }

        public void ConsultFchRenovacion()
        {

            var client = from cl in _context.Cliente
                         select cl;

            var subMeses = from cl in _context.Cliente
                           join subs in _context.Subscripcion
                           on cl.IdSubscripcion equals subs.IdSubscripcion
                           select subs.Meses;
            int x = 0;
            foreach (var item in client)
            {
                item.FechaRenovacion = (item.FechaSubscripcion.AddMonths(subMeses.ToArray().ElementAt(x)));
                _context.Cliente.Update(item);
                x++;
            }
            _context.SaveChanges();
        }

        public void ConsultUVAdisp()
        {

            var client = from cl in _context.Cliente
                         select cl;

            var UVAsubscrip = from cl in _context.Cliente
                              join subs in _context.Subscripcion
                              on cl.IdSubscripcion equals subs.IdSubscripcion
                              select subs.CantidadSesionesUva;

            int x = 0;
            foreach (var item in client)
            {
                item.SesionesUVAdisp = UVAsubscrip.ToArray().ElementAt(x) - item.CantSesionesUva; 
                _context.Cliente.Update(item);
                x++;
            }
            _context.SaveChanges();
        }
    }
}
