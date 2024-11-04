using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.BLL.Interfaces; 


namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class PlantillaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INegocioService _negocioServicio;
        private readonly IVentaService _ventaServicio;
        private readonly IEntradaService _entradaServicio;


        public PlantillaController(IMapper mapper,
             INegocioService negocioServicio,
             IVentaService ventaServicio,
             IEntradaService entradaServicio
            )
        {
            _mapper = mapper;
            _negocioServicio = negocioServicio;
            _ventaServicio = ventaServicio;
            _entradaServicio = entradaServicio;
        }

        public IActionResult EnviarClave(string correo, string clave)
        {
            ViewData["Correo"] = correo;
            ViewData["Clave"] = clave;
            ViewData["Url"] = $"{this.Request.Scheme}://{this.Request.Host}";




            return View();
        }

        public async Task<IActionResult> PDFVenta(string numeroVenta)
        {
            VMVenta vmVenta = _mapper.Map<VMVenta>(await _ventaServicio.Detalle(numeroVenta));
            VMNegocio vmNegocio = _mapper.Map<VMNegocio>(await _negocioServicio.Obtener());

            VMPDFVenta modelo = new VMPDFVenta();

            modelo.negocio = vmNegocio;
            modelo.venta = vmVenta;

            return View(modelo);
        }

        public async Task<IActionResult> PDFEntrada(string numeroEntrada)
        {
            VMEntrada vmEntrada = _mapper.Map<VMEntrada>(await _entradaServicio.Detalle(numeroEntrada));
            VMNegocio vmNegocio = _mapper.Map<VMNegocio>(await _negocioServicio.Obtener());

            VMPDFEntrada modelo = new VMPDFEntrada();

            modelo.negocio = vmNegocio;
            modelo.entrada = vmEntrada;

            return View(modelo);
        }


        public IActionResult RestablecerClave(string clave)
        {

            ViewData["Clave"] = clave;

            return View();



        }




    }



}
