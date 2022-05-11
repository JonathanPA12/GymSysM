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
    public class CategoriaEmpleadosController : Controller
    {
        private readonly BdGymContext _context;

        public CategoriaEmpleadosController(BdGymContext context)
        {
            _context = context;
        }

        // GET: CategoriaEmpleadoes
        public async Task<IActionResult> Index()
        {
            ConsultasSalBase();
            return View(await _context.CategoriaEmpleado.ToListAsync());
        }

        // GET: CategoriaEmpleadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEmpleado = await _context.CategoriaEmpleado
                .FirstOrDefaultAsync(m => m.IdCategoriaEmpleado == id);
            if (categoriaEmpleado == null)
            {
                return NotFound();
            }

            return View(categoriaEmpleado);
        }

        // GET: CategoriaEmpleadoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriaEmpleadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCategoriaEmpleado,Nombre,Descripcion,SalarioHora,SalarioBase")] CategoriaEmpleado categoriaEmpleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaEmpleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaEmpleado);
        }

        // GET: CategoriaEmpleadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEmpleado = await _context.CategoriaEmpleado.FindAsync(id);
            if (categoriaEmpleado == null)
            {
                return NotFound();
            }
            return View(categoriaEmpleado);
        }

        // POST: CategoriaEmpleadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategoriaEmpleado,Nombre,Descripcion,SalarioHora,SalarioBase")] CategoriaEmpleado categoriaEmpleado)
        {
            if (id != categoriaEmpleado.IdCategoriaEmpleado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaEmpleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaEmpleadoExists(categoriaEmpleado.IdCategoriaEmpleado))
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
            return View(categoriaEmpleado);
        }

        // GET: CategoriaEmpleadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEmpleado = await _context.CategoriaEmpleado
                .FirstOrDefaultAsync(m => m.IdCategoriaEmpleado == id);
            if (categoriaEmpleado == null)
            {
                
                return NotFound();
            }


            if (categoriaEmpleado.Empleado.Any())
            {
                return BadRequest();
            }

            return View(categoriaEmpleado);
        }

        // POST: CategoriaEmpleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriaEmpleado = await _context.CategoriaEmpleado.FindAsync(id);
            _context.CategoriaEmpleado.Remove(categoriaEmpleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaEmpleadoExists(int id)
        {
            return _context.CategoriaEmpleado.Any(e => e.IdCategoriaEmpleado == id);
        }

        public void ConsultasSalBase()
        {
            var categEmp = from catEmp in _context.CategoriaEmpleado
                           select catEmp;

            foreach (var item in categEmp)
            {
                item.SalarioBase = item.SalarioHora * 48;
                _context.CategoriaEmpleado.Update(item);
            }
            _context.SaveChanges();
        }
    }
}
