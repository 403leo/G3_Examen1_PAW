using Examen1_LeonardoMadrigal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Examen1_LeonardoMadrigal.Controllers
{
    public class DashboardController : Controller
    {
        private readonly Examen1Context _context;

        public DashboardController(Examen1Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Obtener los datos
            var totalRutasActivas = await _context.Rutas.CountAsync(r => r.Estado == "Activo");
            var totalVehiculosEnBuenEstado = await _context.Vehiculos.CountAsync(v => v.Estado == "Buen Estado");
            var fechaInicioMesActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var totalBoletosVendidosMesActual = await _context.Boletos
                .Where(b => b.FechaCompra >= fechaInicioMesActual)
                .CountAsync();

            var usuariosConBoletos = await _context.Boletos
                .Include(b => b.Usuario)
                .Include(b => b.Ruta)
                .Where(b => b.FechaCompra >= fechaInicioMesActual)
                .GroupBy(b => new { b.UsuarioId, b.RutaId, b.Cantidad })
                .Select(g => new UsuarioBoletosViewModel
                {
                    UsuarioNombre = g.First().Usuario.Nombre,
                    Ruta = g.First().Ruta.Nombre,
                    BoletosVendidos = g.Count()
                })
                .ToListAsync();

            var dashboardData = new DashboardViewModel
            {
                TotalRutasActivas = totalRutasActivas,
                TotalVehiculosEnBuenEstado = totalVehiculosEnBuenEstado,
                TotalBoletosVendidosMesActual = totalBoletosVendidosMesActual,
                UsuariosConBoletos = usuariosConBoletos
            };

            return View(dashboardData);
        }
    }
}
