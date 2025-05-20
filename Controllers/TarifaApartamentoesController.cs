using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba_Tecnica.Controllers
{
    
    public class TarifaApartamentoesController : Controller
    {
        private readonly FondoXyzContext _context;

        public TarifaApartamentoesController(FondoXyzContext context)
        {
            _context = context;
        }

        // GET: TarifaApartamentoes

        public async Task<IActionResult> Index()
        {
            var tarifas = await _context.TarifaApartamentos
            .Include(h => h.IdNavigation)
            .Include(j => j.IdTemporadaNavigation)
            .ToListAsync();
            foreach (var tarifa in tarifas)
            {
                if (tarifa.ImagenAlojamiento != null)
                {
                    string imageBase64Data = Convert.ToBase64String(tarifa.ImagenAlojamiento);
                    string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);

                    tarifa.ImagenDataURL = imageDataURL;
                }
            }
            return View(tarifas);
        }
        [HttpPost]
        public async Task<IActionResult> SubirImagen(int id, IFormFile Imagen)
        {
            var tarifa = await _context.TarifaApartamentos.FindAsync(id);
            if (tarifa == null)
                return NotFound();

            if (Imagen != null && Imagen.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await Imagen.CopyToAsync(ms);
                    tarifa.ImagenAlojamiento = ms.ToArray();
                }
                _context.Update(tarifa);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: TarifaApartamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarifaApartamento = await _context.TarifaApartamentos
                .Include(t => t.IdNavigation)
                .Include(t => t.IdTemporadaNavigation)
                .FirstOrDefaultAsync(m => m.IdTarifa == id);
            if (tarifaApartamento == null)
            {
                return NotFound();
            }

            return View(tarifaApartamento);
        }

        // GET: TarifaApartamentoes/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Apartamentoes, "Id", "Id");
            ViewData["IdTemporada"] = new SelectList(_context.Temporada, "IdTemporada", "IdTemporada");
            return View();
        }

        // POST: TarifaApartamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTarifa,Id,IdTemporada,Precio,PersonasIncluidas")] TarifaApartamento tarifaApartamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarifaApartamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Apartamentoes, "Id", "Id", tarifaApartamento.Id);
            ViewData["IdTemporada"] = new SelectList(_context.Temporada, "IdTemporada", "IdTemporada", tarifaApartamento.IdTemporada);
            return View(tarifaApartamento);
        }

        // GET: TarifaApartamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarifaApartamento = await _context.TarifaApartamentos.FindAsync(id);
            if (tarifaApartamento == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Apartamentoes, "Id", "Id", tarifaApartamento.Id);
            ViewData["IdTemporada"] = new SelectList(_context.Temporada, "IdTemporada", "IdTemporada", tarifaApartamento.IdTemporada);
            return View(tarifaApartamento);
        }

        // POST: TarifaApartamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTarifa,Id,IdTemporada,Precio,PersonasIncluidas")] TarifaApartamento tarifaApartamento)
        {
            if (id != tarifaApartamento.IdTarifa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarifaApartamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarifaApartamentoExists(tarifaApartamento.IdTarifa))
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
            ViewData["Id"] = new SelectList(_context.Apartamentoes, "Id", "Id", tarifaApartamento.Id);
            ViewData["IdTemporada"] = new SelectList(_context.Temporada, "IdTemporada", "IdTemporada", tarifaApartamento.IdTemporada);
            return View(tarifaApartamento);
        }

        // GET: TarifaApartamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarifaApartamento = await _context.TarifaApartamentos
                .Include(t => t.IdNavigation)
                .Include(t => t.IdTemporadaNavigation)
                .FirstOrDefaultAsync(m => m.IdTarifa == id);
            if (tarifaApartamento == null)
            {
                return NotFound();
            }

            return View(tarifaApartamento);
        }

        // POST: TarifaApartamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarifaApartamento = await _context.TarifaApartamentos.FindAsync(id);
            if (tarifaApartamento != null)
            {
                _context.TarifaApartamentos.Remove(tarifaApartamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarifaApartamentoExists(int id)
        {
            return _context.TarifaApartamentos.Any(e => e.IdTarifa == id);
        }
    }
}
