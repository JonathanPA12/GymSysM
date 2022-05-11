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
    public class PlanillasController : Controller
    {
        private readonly BdGymContext _context;

        public PlanillasController(BdGymContext context)
        {
            _context = context;
        }

        // GET: Planillas
        public async Task<IActionResult> Index()
        {
            var bdGymContext = _context.Planilla.Include(p => p.IdEmpleadoNavigation);
            AccionesPlanilla();
            return View(await bdGymContext.ToListAsync());
        }

        // GET: Planillas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planilla = await _context.Planilla
                .Include(p => p.IdEmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.IdPlanilla == id);
            if (planilla == null)
            {
                return NotFound();
            }

            return View(planilla);
        }

        // GET: Planillas/Create
        public IActionResult Create()
        {
            ViewData["IdEmpleado"] = new SelectList(_context.Empleado, "IdEmpleado", "Apellidos");
            return View();
        }

        // POST: Planillas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPlanilla,IdEmpleado,FechaInicio,HorasTrabajadas,SalarioHora,SalarioBruto,SalarioNeto,Ccss")] Planilla planilla)
        {
            if (ModelState.IsValid)
            {
                var empl = from mp in _context.Planilla
                           select mp.IdEmpleado;
                if (empl.Contains(planilla.IdEmpleado))
                {
                    return BadRequest();
                }
                _context.Add(planilla);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

         


                ViewData["IdEmpleado"] = new SelectList(_context.Empleado, "IdEmpleado", "Apellidos", planilla.IdEmpleado);
            Empleado emp = _context.Empleado.Where(g => g.IdEmpleado == planilla.IdEmpleado).First();
            int cat = emp.IdCategoriaEmpleado;
            CategoriaEmpleado catEmp = _context.CategoriaEmpleado.Where(g => g.IdCategoriaEmpleado == cat).First();
            decimal sal = catEmp.SalarioHora;
            ViewData["SalarioHora"] = sal;
            return View(planilla);


        }

        // GET: Planillas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planilla = await _context.Planilla.FindAsync(id);
            if (planilla == null)
            {
                return NotFound();
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Empleado, "IdEmpleado", "Apellidos", planilla.IdEmpleado);
            return View(planilla);
        }

        // POST: Planillas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPlanilla,IdEmpleado,HorasTrabajadas,SalarioHora,SalarioBruto,SalarioNeto,Ccss")] Planilla planilla)
        {
            if (id != planilla.IdPlanilla)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planilla);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanillaExists(planilla.IdPlanilla))
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
            ViewData["IdEmpleado"] = new SelectList(_context.Empleado, "IdEmpleado", "Apellidos", planilla.IdEmpleado);
            return View(planilla);
        }

        // GET: Planillas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planilla = await _context.Planilla
                .Include(p => p.IdEmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.IdPlanilla == id);
            if (planilla == null)
            {
                return NotFound();
            }

            return View(planilla);
        }

        // POST: Planillas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planilla = await _context.Planilla.FindAsync(id);
            _context.Planilla.Remove(planilla);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanillaExists(int id)
        {
            return _context.Planilla.Any(e => e.IdPlanilla == id);
        }

        public void AccionesPlanilla()
        {
            var plan = from pln in _context.Planilla
                       select pln;

            var salarioHora = from empl in _context.Empleado
                              join cat in _context.CategoriaEmpleado
                              on empl.IdCategoriaEmpleado equals cat.IdCategoriaEmpleado
                              select cat.SalarioHora;
            
            int x = 0;
            foreach (var item in plan)
            {
               
                item.SalarioHora = salarioHora.ToArray().ElementAt(x);
                item.SalarioBruto = item.SalarioHora * item.HorasTrabajadas;
                item.Ccss = (item.SalarioBruto * 5) / 100;
                item.SalarioNeto = item.SalarioBruto - item.Ccss;
                _context.Planilla.Update(item);
                x++;
            }
            
            _context.SaveChanges();
        }
    }
}
