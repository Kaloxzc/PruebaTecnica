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
    public class MisReservasAlojsController : Controller
    {
        private readonly FondoXyzContext _context;

        public MisReservasAlojsController(FondoXyzContext context)
        {
            _context = context;
        }

        // GET: MisReservasAloj
        public async Task<IActionResult> Index()
        {
            var reservas = await _context.MisReservasAloj
                .Include(r => r.Alojamiento)
                .Include(r => r.Tarifa)
                .ThenInclude(t => t.IdTemporadaNavigation)
                .OrderByDescending(r => r.FechaLlegada)
                .ToListAsync();

            return View(reservas);
        }
        [HttpGet]
        public async Task<IActionResult> CreateFromTarifa(int idTarifa)
        {
            var tarifa = await _context.TarifaAlojamientos
                .Include(t => t.IdAlojamientoNavigation)
                .Include(t => t.IdTemporadaNavigation)
                .FirstOrDefaultAsync(t => t.IdTarifa == idTarifa);

            if (tarifa == null)
            {
                return NotFound();
            }

            var model = new MisReservasAloj
            {
                IdTarifa = idTarifa,
                IdAlojamiento = tarifa.IdAlojamiento.Value,
                IdTemporada = tarifa.IdTemporada.Value,
                NumeroPersonas = tarifa.PersonasIncluidas ?? 1
            };

            // Pasar la información completa de la tarifa a la vista
            ViewBag.TarifaInfo = new
            {
                tarifa.Precio,
                NumeroHabitacion = tarifa.NumeroHabitacion?.ToString() ?? "No definido",
                PersonasIncluidas = tarifa.PersonasIncluidas?.ToString() ?? "No definido",
                TemporadaNombre = tarifa.IdTemporadaNavigation?.Nombre ?? "No definido"
            };

            ViewBag.AlojamientoInfo = tarifa.IdAlojamientoNavigation;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFromTarifa(MisReservasAloj reserva)
        {
            try
            {
                reserva.FechaCreacion = DateTime.Now;
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("CreateFromTarifa", new { idTarifa = reserva.IdTarifa });
            }
        }



        private void CargarViewData(int idTarifa)
        {
            var tarifa = _context.TarifaAlojamientos
                .Include(t => t.IdAlojamientoNavigation)
                .Include(t => t.IdTemporadaNavigation)
                .FirstOrDefault(t => t.IdTarifa == idTarifa);

            ViewBag.TarifaInfo = tarifa;
        }

        private async Task<IActionResult> RecargarVistaConDatos(MisReservasAloj reserva)
        {
            try
            {
                var tarifa = await _context.TarifaAlojamientos
                    .Include(t => t.IdAlojamientoNavigation)
                    .Include(t => t.IdTemporadaNavigation)
                    .FirstOrDefaultAsync(t => t.IdTarifa == reserva.IdTarifa);

                if (tarifa == null)
                {
                    TempData["ErrorMessage"] = "No se encontró la tarifa asociada";
                    return RedirectToAction("Index", "TarifaAlojamientoes");
                }

                ViewBag.TarifaInfo = new
                {
                    Precio = tarifa.Precio?.ToString("C") ?? "No definido",
                    NumeroHabitacion = tarifa.NumeroHabitacion?.ToString() ?? "No definido",
                    PersonasIncluidas = tarifa.PersonasIncluidas?.ToString() ?? "No definido",
                    TemporadaNombre = tarifa.IdTemporadaNavigation?.Nombre ?? "No definido"
                };

                ViewBag.AlojamientoInfo = tarifa.IdAlojamientoNavigation;

                return View("CreateFromTarifa", reserva);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al cargar datos: {ex.Message}";
                return RedirectToAction("Index", "TarifaAlojamientoes");
            }
        }

        // GET: MisReservasAloj/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var reserva = await _context.MisReservasAloj
                .Include(r => r.Alojamiento)
                .Include(r => r.Tarifa)
                .ThenInclude(t => t.IdTemporadaNavigation)
                .FirstOrDefaultAsync(r => r.IdReserva == id);

            if (reserva == null)
            {
                return NotFound();
            }

            ViewBag.TarifaInfo = new
            {
                Precio = reserva.Tarifa.Precio?.ToString("C") ?? "No definido",
                TemporadaNombre = reserva.Tarifa.IdTemporadaNavigation?.Nombre ?? "No definido"
            };

            ViewBag.AlojamientoInfo = reserva.Alojamiento;

            return View(reserva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MisReservasAloj reserva)
        {
            if (id != reserva.IdReserva)
            {
                return NotFound();
            }

            try
            {
                // Validación para temporada 4
                if (reserva.IdTemporada == 4 &&
                    (reserva.FechaLlegada.DayOfWeek >= DayOfWeek.Friday ||
                     reserva.FechaSalida.DayOfWeek >= DayOfWeek.Friday))
                {
                    ModelState.AddModelError("FechaLlegada", "Para la temporada Especial solo se permiten reservas de Lunes a Jueves");
                    ModelState.AddModelError("FechaSalida", "Para la temporada Especial solo se permiten reservas de Lunes a Jueves");
                }

                if (ModelState.IsValid)
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Reserva actualizada correctamente";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(reserva.IdReserva))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al editar reserva: {ex.Message}");
                TempData["ErrorMessage"] = "Error al actualizar la reserva";
            }

            // Recargar datos si hay error
            var tarifa = await _context.TarifaAlojamientos
                .Include(t => t.IdTemporadaNavigation)
                .FirstOrDefaultAsync(t => t.IdTarifa == reserva.IdTarifa);

            ViewBag.TarifaInfo = new
            {
                Precio = tarifa?.Precio?.ToString("C") ?? "No definido",
                TemporadaNombre = tarifa?.IdTemporadaNavigation?.Nombre ?? "No definido"
            };

            ViewBag.AlojamientoInfo = await _context.Alojamientos.FindAsync(reserva.IdAlojamiento);

            return View(reserva);
        }

        private bool ReservaExists(int id)
        {
            return _context.MisReservasAloj.Any(e => e.IdReserva == id);
        }

        private bool MisReservasAlojExists(int id)
        {
            return _context.MisReservasAloj.Any(e => e.IdReserva == id);
        }
    }
}
