using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba_Tecnica.Controllers
{
    public class MisReservasAptController : Controller
    {
        private readonly FondoXyzContext _context;
        private readonly ILogger<MisReservasAptController> _logger;

        public MisReservasAptController(FondoXyzContext context, ILogger<MisReservasAptController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: MisReservasApt
        public async Task<IActionResult> Index()
        {
            try
            {
                var reservas = await _context.MisReservasApt
                    .Include(r => r.Apartamento)
                    .Include(r => r.Tarifa)
                        .ThenInclude(t => t.IdTemporadaNavigation)
                    .OrderByDescending(r => r.FechaLlegada)
                    .ToListAsync();

                return View(reservas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar reservas");
                return View(new List<MisReservasApt>());
            }
        }

        // GET: MisReservasApt/Create
        public IActionResult Create()
        {
            ViewBag.Apartamentos = _context.Apartamentoes.ToList();
            ViewBag.Tarifas = _context.TarifaApartamentos.Include(t => t.IdTemporadaNavigation).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdApartamento,IdTarifa,FechaLlegada,FechaSalida")] MisReservasApt reserva)
        {
            // Validar que la tarifa existe
            var tarifa = await _context.TarifaApartamentos
                .FirstOrDefaultAsync(t => t.IdTarifa == reserva.IdTarifa);

            if (tarifa == null)
            {
                ModelState.AddModelError("", "La tarifa seleccionada no existe");
            }

            // Validar fechas
            if (reserva.FechaLlegada >= reserva.FechaSalida)
            {
                ModelState.AddModelError("FechaSalida", "La fecha de salida debe ser posterior a la de llegada");
            }

            if (ModelState.IsValid)
            {
                reserva.FechaCreacion = DateTime.Now;

                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recargar datos para la vista si hay error
            var tarifaReloaded = await _context.TarifaApartamentos
                .Include(t => t.IdNavigation)
                .Include(t => t.IdTemporadaNavigation)
                .FirstOrDefaultAsync(t => t.IdTarifa == reserva.IdTarifa);

            ViewBag.ApartamentoNombre = tarifaReloaded?.IdNavigation?.Nombre ?? "Desconocido";
            ViewBag.TarifaInfo = $"{tarifaReloaded?.Precio:C} ({tarifaReloaded?.IdTemporadaNavigation?.Nombre ?? "Sin temporada"})";

            return View(reserva);
        }
        [HttpGet]
        public IActionResult CreateFromTarifa(int idTarifa)
        {
            var tarifa = _context.TarifaApartamentos
                .Include(t => t.IdNavigation)
                .Include(t => t.IdTemporadaNavigation)
                .FirstOrDefault(t => t.IdTarifa == idTarifa);

            if (tarifa == null)
            {
                return NotFound();
            }

            var model = new MisReservasApt
            {
                IdTarifa = idTarifa,
                IdApartamento = (int)tarifa.Id
            };

            // Pasar información adicional a la vista
            ViewBag.TarifaInfo = tarifa;

            return View(model); // Esto buscará Views/MisReservasApts/CreateFromTarifa.cshtml
        }
        [HttpPost]
        public async Task<IActionResult> CreateFromTarifa(MisReservasApt reserva)
        {
            _context.MisReservasApt.Add(reserva);
            await _context.SaveChangesAsync();
            Console.WriteLine("Guardado exitoso"); // Ver en output de Visual Studio
            Debug.WriteLine("Redirigiendo a Index"); // Ver en ventana de Debug
            return RedirectToAction("Index"); // Versión explícita
        }

        private void CargarViewData(int idTarifa)
        {
            var tarifa = _context.TarifaApartamentos
                .Include(t => t.IdNavigation)
                .Include(t => t.IdTemporadaNavigation)
                .FirstOrDefault(t => t.IdTarifa == idTarifa);

            ViewBag.TarifaInfo = tarifa;
        }

        private async Task<IActionResult> RecargarVistaConDatos(MisReservasApt reserva)
        {
            var tarifa = await _context.TarifaApartamentos
                .Include(t => t.IdNavigation)
                .Include(t => t.IdTemporadaNavigation)
                .FirstOrDefaultAsync(t => t.IdTarifa == reserva.IdTarifa);

            if (tarifa == null)
            {
                return NotFound();
            }

            ViewBag.TarifaInfo = tarifa;
            return View(reserva);
        }
        // GET: MisReservasApt/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.MisReservasApt
                .Include(r => r.Apartamento)
                .Include(r => r.Tarifa)
                .ThenInclude(t => t.IdTemporadaNavigation)
                .FirstOrDefaultAsync(r => r.IdReserva == id);

            if (reserva == null)
            {
                return NotFound();
            }

            ViewBag.ApartamentoInfo = reserva.Apartamento;
            ViewBag.TarifaInfo = reserva.Tarifa;

            return View(reserva);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReserva,IdApartamento,IdTarifa,FechaLlegada,FechaSalida,FechaCreacion,Estado")] MisReservasApt reserva)
        {
            if (id != reserva.IdReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Validación de fechas
                    if (reserva.FechaLlegada >= reserva.FechaSalida)
                    {
                        ModelState.AddModelError("", "La fecha de salida debe ser posterior a la de llegada");
                        ViewBag.ApartamentoInfo = await _context.Apartamentoes.FindAsync(reserva.IdApartamento);
                        ViewBag.TarifaInfo = await _context.TarifaApartamentos
                            .Include(t => t.IdTemporadaNavigation)
                            .FirstOrDefaultAsync(t => t.IdTarifa == reserva.IdTarifa);
                        return View(reserva);
                    }

                    _context.Update(reserva);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Reserva actualizada correctamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!ReservaExists(reserva.IdReserva))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error al guardar cambios: " + ex.Message);
                    }
                }
            }

            ViewBag.ApartamentoInfo = await _context.Apartamentoes.FindAsync(reserva.IdApartamento);
            ViewBag.TarifaInfo = await _context.TarifaApartamentos
                .Include(t => t.IdTemporadaNavigation)
                .FirstOrDefaultAsync(t => t.IdTarifa == reserva.IdTarifa);

            return View(reserva);
        }

       


        // GET: MisReservasApt/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var reserva = await _context.MisReservasApt
                .Include(r => r.Apartamento)
                .Include(r => r.Tarifa)
                .FirstOrDefaultAsync(m => m.IdReserva == id);

            if (reserva == null) return NotFound();

            return View(reserva);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.MisReservasApt.FindAsync(id);
            _context.MisReservasApt.Remove(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.MisReservasApt.Any(e => e.IdReserva == id);
        }
    }
}

