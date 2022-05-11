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
    public class MatriculasController : Controller
    {
        private readonly BdGymContext _context;

        public MatriculasController(BdGymContext context)
        {
            _context = context;
        }

        // GET: Matriculas
        public async Task<IActionResult> Index()
        {
            var bdGymContext = _context.Matricula.Include(m => m.IdClaseNavigation).Include(m => m.IdClienteNavigation);
            return View(await bdGymContext.ToListAsync());
        }

        // GET: Matriculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matricula
                .Include(m => m.IdClaseNavigation)
                .Include(m => m.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdMatricula == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // GET: Matriculas/Create
        public IActionResult Create()
        {
            //ViewData["IdClase"] = new SelectList(_context.Clase.Where(g => g.Capacidad > 0).Include(c => c.IdActividadNavigation), "IdClase", "Nombre");
            ViewData["IdClase"] = new SelectList(_context.Clase.Where(g => g.Capacidad > 0), "IdClase", "Dia");
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Dni");
            return View();
        }

        // POST: Matriculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMatricula,IdCliente,IdClase,FechaHora")] Matricula matricula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matricula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var clase = _context.Clase.Include(c => c.IdActividadNavigation);
            clase.ToList();

            //ViewData["IdClase"] = new SelectList(clase, "IdActividadNavigation", "Nombre", matricula.IdClase);
            ViewData["IdClase"] = new SelectList(_context.Clase, "IdClase", "Dia", matricula.IdClase);
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Dni", matricula.IdCliente);
            
            return View(matricula);
        }

        // GET: Matriculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matricula.FindAsync(id);
            if (matricula == null)
            {
                return NotFound();
            }
            ViewData["IdClase"] = new SelectList(_context.Clase, "IdClase", "Dia", matricula.IdClase);
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Dni", matricula.IdCliente);
            return View(matricula);
        }

        // POST: Matriculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMatricula,IdCliente,IdClase,FechaHora")] Matricula matricula)
        {
            if (id != matricula.IdMatricula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matricula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatriculaExists(matricula.IdMatricula))
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
            ViewData["IdClase"] = new SelectList(_context.Clase, "IdClase", "Dia", matricula.IdClase);
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Dni", matricula.IdCliente);
            return View(matricula);
        }

        // GET: Matriculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matricula
                .Include(m => m.IdClaseNavigation)
                .Include(m => m.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.IdMatricula == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // POST: Matriculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matricula = await _context.Matricula.FindAsync(id);
            _context.Matricula.Remove(matricula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatriculaExists(int id)
        {
            return _context.Matricula.Any(e => e.IdMatricula == id);
        }

        
        private void DisminuirCupos(int id)
        {
            var claseM = from mt in _context.Matricula
                         where(mt.IdMatricula == id)
                         join clase in _context.Clase
                         on mt.IdClase equals clase.IdClase
                         select clase;

            foreach (var item in claseM)
            {
                item.Capacidad--;
                _context.Clase.Update(item);
            }
            _context.SaveChanges();
        }
         
    }
}
