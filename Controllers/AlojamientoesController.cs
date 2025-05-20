using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Models;

namespace Prueba_Tecnica.Controllers
{
    public class AlojamientoesController : Controller
    {
        private readonly FondoXyzContext _context;

        public AlojamientoesController(FondoXyzContext context)
        {
            _context = context;
        }

        // GET: Alojamientoes
        public async Task<IActionResult> Index()
        {
            var alojamientos = await _context.Alojamientos
            .Include(g => g.IdSedeNavigation)
            .ToListAsync();
            foreach (var alojamiento in alojamientos)
            {
                if (alojamiento.ImagenAlojamiento != null)
                {
                    string imageBase64Data = Convert.ToBase64String(alojamiento.ImagenAlojamiento);
                    string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);

                    alojamiento.ImagenDataURL = imageDataURL;
                }
            }
            return View(alojamientos);
        }
        [HttpPost]
        public async Task<IActionResult> SubirImagen(int id, IFormFile Imagen)
        {
            var alojamiento = await _context.Alojamientos.FindAsync(id);
            if (alojamiento == null)
                return NotFound();

            if (Imagen != null && Imagen.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await Imagen.CopyToAsync(ms);
                    alojamiento.ImagenAlojamiento = ms.ToArray();
                }
                _context.Update(alojamiento);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Alojamientoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alojamiento = await _context.Alojamientos
                .Include(a => a.IdSedeNavigation)
                .FirstOrDefaultAsync(m => m.IdAlojamiento == id);

            if (alojamiento == null)
            {
                return NotFound();
            }

            // Convertir imagen a base64 para mostrarla en la vista
            if (alojamiento.ImagenAlojamiento != null)
            {
                string imageBase64Data = Convert.ToBase64String(alojamiento.ImagenAlojamiento);
                string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                alojamiento.ImagenDataURL = imageDataURL;
            }

            return View(alojamiento);
        }

        // GET: Alojamientoes/Create
        public IActionResult Create()
        {
            ViewData["IdSede"] = new SelectList(_context.Sedes, "IdSede", "IdSede");
            return View();
        }

        // POST: Alojamientoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAlojamiento,IdSede,Nombre,Tipo,NumeroHabitaciones,NumeroBanos,CapacidadMaxima,Sala,Cocina,Cocineta,Terraza,Comedor,Televisor,SofaCama,Nevera,Observaciones")] Alojamiento alojamiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alojamiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSede"] = new SelectList(_context.Sedes, "IdSede", "IdSede", alojamiento.IdSede);
            return View(alojamiento);
        }

        // GET: Alojamientoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alojamiento = await _context.Alojamientos.FindAsync(id);
            if (alojamiento == null)
            {
                return NotFound();
            }
            ViewData["IdSede"] = new SelectList(_context.Sedes, "IdSede", "IdSede", alojamiento.IdSede);
            return View(alojamiento);
        }

        // POST: Alojamientoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAlojamiento,IdSede,Nombre,Tipo,NumeroHabitaciones,NumeroBanos,CapacidadMaxima,Sala,Cocina,Cocineta,Terraza,Comedor,Televisor,SofaCama,Nevera,Observaciones")] Alojamiento alojamiento)
        {
            if (id != alojamiento.IdAlojamiento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alojamiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlojamientoExists(alojamiento.IdAlojamiento))
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
            ViewData["IdSede"] = new SelectList(_context.Sedes, "IdSede", "IdSede", alojamiento.IdSede);
            return View(alojamiento);
        }

        // GET: Alojamientoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alojamiento = await _context.Alojamientos
                .Include(a => a.IdSedeNavigation)
                .FirstOrDefaultAsync(m => m.IdAlojamiento == id);
            if (alojamiento == null)
            {
                return NotFound();
            }

            return View(alojamiento);
        }

        // POST: Alojamientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alojamiento = await _context.Alojamientos.FindAsync(id);
            if (alojamiento != null)
            {
                _context.Alojamientos.Remove(alojamiento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlojamientoExists(int id)
        {
            return _context.Alojamientos.Any(e => e.IdAlojamiento == id);
        }
    }
}
