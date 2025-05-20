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
    public class ReservaApartamentoesController : Controller
    {
        private readonly FondoXyzContext _context;

        public ReservaApartamentoesController(FondoXyzContext context)
        {
            _context = context;
        }

        // GET: ReservaApartamentoes
        public async Task<IActionResult> Index()
        {
            var reservaapartamentos = await _context.ReservaApartamentos
            .Include(h => h.IdApartamento)
            .ToListAsync();
            foreach (var reservaapartamento in reservaapartamentos)
            {
                if (reservaapartamento.ImagenReserva != null)
                {
                    string imageBase64Data = Convert.ToBase64String(reservaapartamento.ImagenReserva);
                    string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);

                    reservaapartamento.ImagenDataURL = imageDataURL;
                }
            }
            return View(reservaapartamentos);
        }

        // GET: ReservaApartamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservaApartamento = await _context.ReservaApartamentos
                .Include(r => r.Apartamento)
                .Include(r => r.Temporada)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reservaApartamento == null)
            {
                return NotFound();
            }

            return View(reservaApartamento);
        }

        // GET: ReservaApartamentoes/Create
        public IActionResult Create(int idApartamento)
        {
            var reserva = new ReservaApartamento
            {
                IdApartamento = idApartamento,
                FechaLlegada = DateTime.Today,
                FechaSalida = DateTime.Today.AddDays(1),
                NumeroPersonas = 1,
                IncluyeLavanderia = false
            };

            return View(reserva);
        }

        // POST: ReservaApartamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        private int ObtenerTemporada(DateTime fecha)
        {
            // Especial si es lunes a jueves
            var dia = fecha.DayOfWeek;
            if (dia >= DayOfWeek.Monday && dia <= DayOfWeek.Thursday)
                return 4; // ID de temporada especial

            return 1; // ID de temporada regular
        }
        private List<object> ObtenerTarifas(int idApartamento, int idTemporada)
        {
            return _context.TarifaApartamentos
                .Where(t => t.Id == idApartamento && t.IdTemporada == idTemporada)
                .Select(t => new {
                    t.PersonasIncluidas,
                    t.Precio
                }).ToList<object>();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservaApartamento reserva)
        {
            if (!ModelState.IsValid)
                return View(reserva);

            int idTemporada = ObtenerTemporada(reserva.FechaLlegada);

            var tarifa = await _context.TarifaApartamentos
                .Where(t => t.Id == reserva.IdApartamento && t.IdTemporada == idTemporada && t.PersonasIncluidas >= reserva.NumeroPersonas)
                .OrderBy(t => t.PersonasIncluidas)
                .FirstOrDefaultAsync();

            if (tarifa == null)
            {
                ModelState.AddModelError("", "No hay tarifa disponible para esta combinación.");
                return View(reserva);
            }

            decimal precio = (decimal)tarifa.Precio;

            if (reserva.IncluyeLavanderia)
            {
                var lavanderia = await _context.TarifaLavanderia.OrderByDescending(t => t.IdTarifaLavanderia).FirstOrDefaultAsync();
                if (lavanderia != null)
                    precio += (decimal)lavanderia.Precio;
            }

            reserva.PrecioTotal = precio;

            _context.Add(reserva);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: ReservaApartamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservaApartamento = await _context.ReservaApartamentos.FindAsync(id);
            if (reservaApartamento == null)
            {
                return NotFound();
            }
            ViewData["IdApartamento"] = new SelectList(_context.Apartamentoes, "Id", "Id", reservaApartamento.IdApartamento);
            ViewData["IdTemporada"] = new SelectList(_context.Temporada, "IdTemporada", "IdTemporada", reservaApartamento.IdTemporada);
            return View(reservaApartamento);
        }

        // POST: ReservaApartamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReserva,IdApartamento,IdTemporada,FechaLlegada,FechaSalida,Noches,NumeroPersonas,IncluyeLavanderia,PrecioTotal")] ReservaApartamento reservaApartamento)
        {
            if (id != reservaApartamento.IdReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservaApartamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaApartamentoExists(reservaApartamento.IdReserva))
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
            ViewData["IdApartamento"] = new SelectList(_context.Apartamentoes, "Id", "Id", reservaApartamento.IdApartamento);
            ViewData["IdTemporada"] = new SelectList(_context.Temporada, "IdTemporada", "IdTemporada", reservaApartamento.IdTemporada);
            return View(reservaApartamento);
        }

        // GET: ReservaApartamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservaApartamento = await _context.ReservaApartamentos
                .Include(r => r.Apartamento)
                .Include(r => r.Temporada)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reservaApartamento == null)
            {
                return NotFound();
            }

            return View(reservaApartamento);
        }

        // POST: ReservaApartamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservaApartamento = await _context.ReservaApartamentos.FindAsync(id);
            if (reservaApartamento != null)
            {
                _context.ReservaApartamentos.Remove(reservaApartamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaApartamentoExists(int id)
        {
            return _context.ReservaApartamentos.Any(e => e.IdReserva == id);
        }
    }
}
