using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Examen1_LeonardoMadrigal.Models;

namespace Examen1_LeonardoMadrigal.Controllers
{
    public class BoletoController : Controller
    {
        private readonly Examen1Context _context;

        public BoletoController(Examen1Context context)
        {
            _context = context;
        }

        //  MOSTRAR LISTA DE BOLETOS
        public async Task<IActionResult> Index()
        {
            var boletos = _context.Boletos
                .Include(b => b.Usuario)
                .Include(b => b.Ruta)
                .Include(b => b.Vehiculo);

            return View(await boletos.ToListAsync());
        }

        //  DETALLES DE UN BOLETO
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var boleto = await _context.Boletos
                .Include(b => b.Usuario)
                .Include(b => b.Ruta)
                .Include(b => b.Vehiculo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (boleto == null)
                return NotFound();

            return View(boleto);
        }

        //  MOSTRAR FORMULARIO PARA COMPRAR BOLETO
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            ViewData["RutaId"] = new SelectList(_context.Rutas, "Id", "Nombre");
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "Id", "Placa");
            return View();
        }

        //  PROCESAR COMPRA DE BOLETO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,RutaId,VehiculoId,Cantidad")] Boleto boleto)
        {
            var ruta = await _context.Rutas.FindAsync(boleto.RutaId);
            if (ruta == null)
                return NotFound("Ruta no encontrada.");

            var vehiculo = await _context.Vehiculos.FindAsync(boleto.VehiculoId);
            if (vehiculo == null)
                return NotFound("Vehículo no encontrado.");

            if (vehiculo.CapacidadDePasajeros < boleto.Cantidad)
                return BadRequest("No hay suficientes asientos disponibles.");

            // Restar asientos disponibles
            vehiculo.CapacidadDePasajeros -= boleto.Cantidad;
            boleto.FechaCompra = DateTime.Now;

            _context.Add(boleto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //  MOSTRAR FORMULARIO PARA EDITAR UN BOLETO
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var boleto = await _context.Boletos.FindAsync(id);
            if (boleto == null)
                return NotFound();

            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", boleto.UsuarioId);
            ViewData["RutaId"] = new SelectList(_context.Rutas, "Id", "Nombre", boleto.RutaId);
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "Id", "Placa", boleto.VehiculoId);
            return View(boleto);
        }

        //  ACTUALIZAR BOLETO EN LA BASE DE DATOS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,RutaId,VehiculoId,Cantidad,FechaCompra")] Boleto boleto)
        {
            if (id != boleto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boleto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Boletos.Any(e => e.Id == boleto.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", boleto.UsuarioId);
            ViewData["RutaId"] = new SelectList(_context.Rutas, "Id", "Nombre", boleto.RutaId);
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "Id", "Placa", boleto.VehiculoId);
            return View(boleto);
        }

        //  MOSTRAR FORMULARIO PARA ELIMINAR BOLETO
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var boleto = await _context.Boletos.FindAsync(id);
            if (boleto == null)
                return NotFound();

            _context.Boletos.Remove(boleto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }





    }
}
