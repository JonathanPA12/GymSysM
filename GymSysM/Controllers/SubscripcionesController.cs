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
    public class SubscripcionesController : Controller
    {
        private readonly BdGymContext _context;

        public SubscripcionesController(BdGymContext context)
        {
            _context = context;
        }

        // GET: Subscripciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Subscripcion.ToListAsync());
        }

        // GET: Subscripciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscripcion = await _context.Subscripcion
                .FirstOrDefaultAsync(m => m.IdSubscripcion == id);
            if (subscripcion == null)
            {
                return NotFound();
            }

            return View(subscripcion);
        }

        // GET: Subscripciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subscripciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSubscripcion,Nombre,Meses,Tarifa,Descripcion,CantidadSesionesUva")] Subscripcion subscripcion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subscripcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subscripcion);
        }

        // GET: Subscripciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscripcion = await _context.Subscripcion.FindAsync(id);
            if (subscripcion == null)
            {
                return NotFound();
            }
            return View(subscripcion);
        }

        // POST: Subscripciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSubscripcion,Nombre,Meses,Tarifa,Descripcion,CantidadSesionesUva")] Subscripcion subscripcion)
        {
            if (id != subscripcion.IdSubscripcion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subscripcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscripcionExists(subscripcion.IdSubscripcion))
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
            return View(subscripcion);
        }

        // GET: Subscripciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscripcion = await _context.Subscripcion
                .FirstOrDefaultAsync(m => m.IdSubscripcion == id);
            if (subscripcion == null)
            {
                return NotFound();
            }

            return View(subscripcion);
        }

        // POST: Subscripciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subscripcion = await _context.Subscripcion.FindAsync(id);
            _context.Subscripcion.Remove(subscripcion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubscripcionExists(int id)
        {
            return _context.Subscripcion.Any(e => e.IdSubscripcion == id);
        }
    }
}
