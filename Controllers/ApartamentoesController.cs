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
    public class ApartamentoesController : Controller
    {
        private readonly FondoXyzContext _context;

        public ApartamentoesController(FondoXyzContext context)
        {
            _context = context;
        }

        // GET: Apartamentoes
        public async Task<IActionResult> Index()
        {
            var apartamentos = await _context.Apartamentoes
            .Include(h => h.IdCiudadNavigation)
            .ToListAsync();
            foreach (var apartamento in apartamentos)
            {
                if (apartamento.ImagenApartamento != null)
                {
                    string imageBase64Data = Convert.ToBase64String(apartamento.ImagenApartamento);
                    string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);

                    apartamento.ImagenDataURL = imageDataURL;
                }
            }
            return View(apartamentos);
        }
        [HttpPost]
        public async Task<IActionResult> SubirImagen(int id, IFormFile Imagen)
        {
            var apartamento = await _context.Apartamentoes.FindAsync(id);
            if (apartamento == null)
                return NotFound();

            if (Imagen != null && Imagen.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await Imagen.CopyToAsync(ms);
                    apartamento.ImagenApartamento = ms.ToArray();
                }
                _context.Update(apartamento);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        // GET: Apartamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartamento = await _context.Apartamentoes
                .Include(a => a.IdCiudadNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (apartamento == null)
            {
                return NotFound();
            }

            // Convertir imagen a base64 para mostrarla en la vista
            if (apartamento.ImagenApartamento != null)
            {
                string imageBase64Data = Convert.ToBase64String(apartamento.ImagenApartamento);
                string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                apartamento.ImagenDataURL = imageDataURL;
            }

            return View(apartamento);
        }

        // GET: Apartamentoes/Create
        public IActionResult Create()
        {
            ViewData["IdCiudad"] = new SelectList(_context.ApartamentoCiudads, "Id", "Id");
            return View();
        }

        // POST: Apartamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,IdCiudad,EsHabitacion,CapacidadMaxima,TieneSalaComedor,TieneCocina,TieneParqueadero,CantidadHabitaciones")] Apartamento apartamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCiudad"] = new SelectList(_context.ApartamentoCiudads, "Id", "Id", apartamento.IdCiudad);
            return View(apartamento);
        }

        // GET: Apartamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartamento = await _context.Apartamentoes.FindAsync(id);
            if (apartamento == null)
            {
                return NotFound();
            }
            ViewData["IdCiudad"] = new SelectList(_context.ApartamentoCiudads, "Id", "Id", apartamento.IdCiudad);
            return View(apartamento);
        }

        // POST: Apartamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,IdCiudad,EsHabitacion,CapacidadMaxima,TieneSalaComedor,TieneCocina,TieneParqueadero,CantidadHabitaciones")] Apartamento apartamento)
        {
            if (id != apartamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartamentoExists(apartamento.Id))
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
            ViewData["IdCiudad"] = new SelectList(_context.ApartamentoCiudads, "Id", "Id", apartamento.IdCiudad);
            return View(apartamento);
        }

        // GET: Apartamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartamento = await _context.Apartamentoes
                .Include(a => a.IdCiudadNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apartamento == null)
            {
                return NotFound();
            }

            return View(apartamento);
        }

        // POST: Apartamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartamento = await _context.Apartamentoes.FindAsync(id);
            if (apartamento != null)
            {
                _context.Apartamentoes.Remove(apartamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartamentoExists(int id)
        {
            return _context.Apartamentoes.Any(e => e.Id == id);
        }
    }
}
