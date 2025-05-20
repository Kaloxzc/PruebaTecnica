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
    public class TarifaAlojamientoesController : Controller
    {
        private readonly FondoXyzContext _context;

        public TarifaAlojamientoesController(FondoXyzContext context)
        {
            _context = context;
        }

        // GET: TarifaAlojamientoes
        public async Task<IActionResult> Index()
        {
            var tarifas = await _context.TarifaAlojamientos
            .Include(t => t.IdAlojamientoNavigation)
            .Include(x => x.IdTemporadaNavigation)
            .ToListAsync();

            return View(tarifas);
        }

        // GET: TarifaAlojamientoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarifaAlojamiento = await _context.TarifaAlojamientos
                .Include(t => t.IdAlojamientoNavigation)
                .Include(t => t.IdTemporadaNavigation)
                .FirstOrDefaultAsync(m => m.IdTarifa == id);
            if (tarifaAlojamiento == null)
            {
                return NotFound();
            }

            return View(tarifaAlojamiento);
        }

        // GET: TarifaAlojamientoes/Create
        public IActionResult Create()
        {
            ViewData["IdAlojamiento"] = new SelectList(_context.Alojamientos, "IdAlojamiento", "IdAlojamiento");
            ViewData["IdTemporada"] = new SelectList(_context.Temporada, "IdTemporada", "IdTemporada");
            return View();
        }

        // POST: TarifaAlojamientoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTarifa,IdAlojamiento,IdTemporada,NumeroHabitacion,Precio,PersonasIncluidas")] TarifaAlojamiento tarifaAlojamiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarifaAlojamiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlojamiento"] = new SelectList(_context.Alojamientos, "IdAlojamiento", "IdAlojamiento", tarifaAlojamiento.IdAlojamiento);
            ViewData["IdTemporada"] = new SelectList(_context.Temporada, "IdTemporada", "IdTemporada", tarifaAlojamiento.IdTemporada);
            return View(tarifaAlojamiento);
        }

        // GET: TarifaAlojamientoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarifaAlojamiento = await _context.TarifaAlojamientos.FindAsync(id);
            if (tarifaAlojamiento == null)
            {
                return NotFound();
            }
            ViewData["IdAlojamiento"] = new SelectList(_context.Alojamientos, "IdAlojamiento", "IdAlojamiento", tarifaAlojamiento.IdAlojamiento);
            ViewData["IdTemporada"] = new SelectList(_context.Temporada, "IdTemporada", "IdTemporada", tarifaAlojamiento.IdTemporada);
            return View(tarifaAlojamiento);
        }

        // POST: TarifaAlojamientoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTarifa,IdAlojamiento,IdTemporada,NumeroHabitacion,Precio,PersonasIncluidas")] TarifaAlojamiento tarifaAlojamiento)
        {
            if (id != tarifaAlojamiento.IdTarifa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarifaAlojamiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarifaAlojamientoExists(tarifaAlojamiento.IdTarifa))
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
            ViewData["IdAlojamiento"] = new SelectList(_context.Alojamientos, "IdAlojamiento", "IdAlojamiento", tarifaAlojamiento.IdAlojamiento);
            ViewData["IdTemporada"] = new SelectList(_context.Temporada, "IdTemporada", "IdTemporada", tarifaAlojamiento.IdTemporada);
            return View(tarifaAlojamiento);
        }

        // GET: TarifaAlojamientoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarifaAlojamiento = await _context.TarifaAlojamientos
                .Include(t => t.IdAlojamientoNavigation)
                .Include(t => t.IdTemporadaNavigation)
                .FirstOrDefaultAsync(m => m.IdTarifa == id);
            if (tarifaAlojamiento == null)
            {
                return NotFound();
            }

            return View(tarifaAlojamiento);
        }

        // POST: TarifaAlojamientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarifaAlojamiento = await _context.TarifaAlojamientos.FindAsync(id);
            if (tarifaAlojamiento != null)
            {
                _context.TarifaAlojamientos.Remove(tarifaAlojamiento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarifaAlojamientoExists(int id)
        {
            return _context.TarifaAlojamientos.Any(e => e.IdTarifa == id);
        }
    }
}
