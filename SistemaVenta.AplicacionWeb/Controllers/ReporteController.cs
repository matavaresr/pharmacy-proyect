using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;




namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class ReporteController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVentaService _ventaServicio;
        private readonly IEntradaService _entradaServicio;



        public ReporteController(IMapper mapper, IVentaService ventaServicio, IEntradaService entradaServicio)
        {
            _mapper = mapper;
            _ventaServicio = ventaServicio; 
            _entradaServicio = entradaServicio;
        }

        public IActionResult Entradas()
        {
            return View();
        }


        public IActionResult Salidas()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ReporteVenta(string fechaInicio, string fechaFin)
        {
            List<VMReporteVenta> vmLista = _mapper.Map<List<VMReporteVenta>>(await _ventaServicio.Reporte(fechaInicio, fechaFin));
            return StatusCode(StatusCodes.Status200OK, new { data = vmLista });

        }

        [HttpGet]

        public async Task<IActionResult> ReporteEntrada(string fechaInicio, string fechaFin)
        {
            List<VMReporteEntrada> vmLista = _mapper.Map<List<VMReporteEntrada>>(await _entradaServicio.Reporte(fechaInicio, fechaFin));
            return StatusCode(StatusCodes.Status200OK, new { data = vmLista });

        }
    }
}
