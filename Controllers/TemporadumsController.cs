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
    public class TemporadumsController : Controller
    {
        private readonly FondoXyzContext _context;

        public TemporadumsController(FondoXyzContext context)
        {
            _context = context;
        }

        // GET: Temporadums
        public async Task<IActionResult> Index()
        {
            return View(await _context.Temporada.ToListAsync());
        }

        // GET: Temporadums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temporadum = await _context.Temporada
                .FirstOrDefaultAsync(m => m.IdTemporada == id);
            if (temporadum == null)
            {
                return NotFound();
            }

            return View(temporadum);
        }

        // GET: Temporadums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Temporadums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTemporada,Nombre,Descripcion")] Temporadum temporadum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(temporadum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(temporadum);
        }

        // GET: Temporadums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temporadum = await _context.Temporada.FindAsync(id);
            if (temporadum == null)
            {
                return NotFound();
            }
            return View(temporadum);
        }

        // POST: Temporadums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTemporada,Nombre,Descripcion")] Temporadum temporadum)
        {
            if (id != temporadum.IdTemporada)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(temporadum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemporadumExists(temporadum.IdTemporada))
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
            return View(temporadum);
        }

        // GET: Temporadums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temporadum = await _context.Temporada
                .FirstOrDefaultAsync(m => m.IdTemporada == id);
            if (temporadum == null)
            {
                return NotFound();
            }

            return View(temporadum);
        }

        // POST: Temporadums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var temporadum = await _context.Temporada.FindAsync(id);
            if (temporadum != null)
            {
                _context.Temporada.Remove(temporadum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemporadumExists(int id)
        {
            return _context.Temporada.Any(e => e.IdTemporada == id);
        }
    }
}
