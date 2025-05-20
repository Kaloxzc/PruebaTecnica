using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Models;

namespace Prueba_Tecnica.Controllers
{
    public class CamasController : Controller
    {
        private readonly FondoXyzContext _context;

        public CamasController(FondoXyzContext context)
        {
            _context = context;
        }

        // GET: Camas
        public async Task<IActionResult> Index()
        {
            var fondoXyzContext = _context.Camas.Include(c => c.IdHabitacionNavigation);
            return View(await fondoXyzContext.ToListAsync());
        }

        // GET: Camas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cama = await _context.Camas
                .Include(c => c.IdHabitacionNavigation)
                .FirstOrDefaultAsync(m => m.IdCama == id);
            if (cama == null)
            {
                return NotFound();
            }

            return View(cama);
        }

        // GET: Camas/Create
        public IActionResult Create()
        {
            ViewData["IdHabitacion"] = new SelectList(_context.Habitacions, "IdHabitacion", "IdHabitacion");
            return View();
        }

        // POST: Camas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCama,IdHabitacion,Tipo,Cantidad")] Cama cama)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cama);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHabitacion"] = new SelectList(_context.Habitacions, "IdHabitacion", "IdHabitacion", cama.IdHabitacion);
            return View(cama);
        }

        // GET: Camas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cama = await _context.Camas.FindAsync(id);
            if (cama == null)
            {
                return NotFound();
            }
            ViewData["IdHabitacion"] = new SelectList(_context.Habitacions, "IdHabitacion", "IdHabitacion", cama.IdHabitacion);
            return View(cama);
        }

        // POST: Camas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCama,IdHabitacion,Tipo,Cantidad")] Cama cama)
        {
            if (id != cama.IdCama)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cama);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CamaExists(cama.IdCama))
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
            ViewData["IdHabitacion"] = new SelectList(_context.Habitacions, "IdHabitacion", "IdHabitacion", cama.IdHabitacion);
            return View(cama);
        }

        // GET: Camas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cama = await _context.Camas
                .Include(c => c.IdHabitacionNavigation)
                .FirstOrDefaultAsync(m => m.IdCama == id);
            if (cama == null)
            {
                return NotFound();
            }

            return View(cama);
        }

        // POST: Camas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cama = await _context.Camas.FindAsync(id);
            if (cama != null)
            {
                _context.Camas.Remove(cama);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CamaExists(int id)
        {
            return _context.Camas.Any(e => e.IdCama == id);
        }
    }
}
