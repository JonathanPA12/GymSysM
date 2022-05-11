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
    public class SesionesUvaController : Controller
    {
        private readonly BdGymContext _context;

        public SesionesUvaController(BdGymContext context)
        {
            _context = context;
        }

        // GET: SesionesUva
        public async Task<IActionResult> Index()
        {
            var bdGymContext = _context.SesionUva.Include(s => s.IdClienteNavigation);
            return View(await bdGymContext.ToListAsync());
        }

        // GET: SesionesUva/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesionUva = await _context.SesionUva
                .Include(s => s.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdSesionUva == id);
            if (sesionUva == null)
            {
                return NotFound();
            }

            return View(sesionUva);
        }

        // GET: SesionesUva/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Apellidos");
            return View();
        }

        // POST: SesionesUva/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSesionUva,Fecha,HoraInicio,Duracion,Tarifa,IdCliente,IdEmpleado")] SesionUva sesionUva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sesionUva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Apellidos", sesionUva.IdCliente);
            return View(sesionUva);
        }

        // GET: SesionesUva/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesionUva = await _context.SesionUva.FindAsync(id);
            if (sesionUva == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Dni", sesionUva.IdCliente);
            return View(sesionUva);
        }

        // POST: SesionesUva/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSesionUva,Fecha,HoraInicio,Duracion,Tarifa,IdCliente,IdEmpleado")] SesionUva sesionUva)
        {
            if (id != sesionUva.IdSesionUva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sesionUva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SesionUvaExists(sesionUva.IdSesionUva))
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
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Dni", sesionUva.IdCliente);
            return View(sesionUva);
        }

        // GET: SesionesUva/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesionUva = await _context.SesionUva
                .Include(s => s.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdSesionUva == id);
            if (sesionUva == null)
            {
                return NotFound();
            }

            return View(sesionUva);
        }

        // POST: SesionesUva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sesionUva = await _context.SesionUva.FindAsync(id);
            _context.SesionUva.Remove(sesionUva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SesionUvaExists(int id)
        {
            return _context.SesionUva.Any(e => e.IdSesionUva == id);
        }
    }
}
