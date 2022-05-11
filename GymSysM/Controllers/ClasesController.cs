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
    public class ClasesController : Controller
    {
        private readonly BdGymContext _context;

        public ClasesController(BdGymContext context)
        {
            _context = context;
        }

        // GET: Clases
        public async Task<IActionResult> Index()
        {
            var bdGymContext = _context.Clase.Include(c => c.IdActividadNavigation).Include(c => c.IdEmpleadoNavigation).Include(c => c.IdSalaNavigation);
            return View(await bdGymContext.ToListAsync());
        }

        // GET: Clases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clase = await _context.Clase
                .Include(c => c.IdActividadNavigation)
                .Include(c => c.IdEmpleadoNavigation)
                .Include(c => c.IdSalaNavigation)
                .FirstOrDefaultAsync(m => m.IdClase == id);
            if (clase == null)
            {
                return NotFound();
            }

            return View(clase);
        }

        // GET: Clases/Create
        public IActionResult Create()
        {
            ViewData["IdActividad"] = new SelectList(_context.Actividad, "IdActividad", "Nombre");
            ViewData["IdEmpleado"] = new SelectList(_context.Empleado, "IdEmpleado", "Apellidos");
            ViewData["IdSala"] = new SelectList(_context.Sala, "IdSala", "Nombre");
            return View();
        }

        // POST: Clases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdClase,IdActividad,IdSala,IdEmpleado,Dia,HoraInicio,HoraFin,Capacidad")] Clase clase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdActividad"] = new SelectList(_context.Actividad, "IdActividad", "Nombre", clase.IdActividad);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleado, "IdEmpleado", "Apellidos", clase.IdEmpleado);
            ViewData["IdSala"] = new SelectList(_context.Sala, "IdSala", "Nombre", clase.IdSala);
            return View(clase);
        }

        // GET: Clases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clase = await _context.Clase.FindAsync(id);
            if (clase == null)
            {
                return NotFound();
            }
            ViewData["IdActividad"] = new SelectList(_context.Actividad, "IdActividad", "Nombre", clase.IdActividad);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleado, "IdEmpleado", "Apellidos", clase.IdEmpleado);
            ViewData["IdSala"] = new SelectList(_context.Sala, "IdSala", "Nombre", clase.IdSala);
            return View(clase);
        }

        // POST: Clases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdClase,IdActividad,IdSala,IdEmpleado,Dia,HoraInicio,HoraFin,Capacidad")] Clase clase)
        {
            if (id != clase.IdClase)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaseExists(clase.IdClase))
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
            ViewData["IdActividad"] = new SelectList(_context.Actividad, "IdActividad", "Nombre", clase.IdActividad);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleado, "IdEmpleado", "Apellidos", clase.IdEmpleado);
            ViewData["IdSala"] = new SelectList(_context.Sala, "IdSala", "Nombre", clase.IdSala);
            return View(clase);
        }

        // GET: Clases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clase = await _context.Clase
                .Include(c => c.IdActividadNavigation)
                .Include(c => c.IdEmpleadoNavigation)
                .Include(c => c.IdSalaNavigation)
                .FirstOrDefaultAsync(m => m.IdClase == id);
            if (clase == null)
            {
                return NotFound();
            }

            if (!clase.Matricula.Any())
            {
                return BadRequest();
            }

            return View(clase);
        }

        // POST: Clases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clase = await _context.Clase.FindAsync(id);
            _context.Clase.Remove(clase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaseExists(int id)
        {
            return _context.Clase.Any(e => e.IdClase == id);
        }
    }
}
