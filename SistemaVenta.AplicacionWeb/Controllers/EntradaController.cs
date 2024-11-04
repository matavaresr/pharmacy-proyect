using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.ViewModels;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;

using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class EntradaController : Controller
    {

        private readonly ITipoDocumentoVentaService _tipoDocumentoVentaServicio;
        private readonly IEntradaService _entradaServicio;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;


        public EntradaController(
            ITipoDocumentoVentaService tipoDocumentoVentaServicio,
            IEntradaService entradaServicio,
            IMapper mapper,
            IConverter converter)
        {
            _tipoDocumentoVentaServicio = tipoDocumentoVentaServicio;
            _entradaServicio = entradaServicio;
            _mapper = mapper;
            _converter = converter;
        }




        public IActionResult NuevaEntrada()
        {
            return View();
        }

        public IActionResult HistorialEntrada()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaTipoDocumentoVenta()
        {
            List<VMTipoDocumentoVenta> vmListaTipoDocumentos = _mapper.Map<List<VMTipoDocumentoVenta>>(await _tipoDocumentoVentaServicio.Lista());

            return StatusCode(StatusCodes.Status200OK, vmListaTipoDocumentos);
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerProductos(string busqueda)
        {
            List<VMProducto> vmListaProductos = _mapper.Map<List<VMProducto>>(await _entradaServicio.ObtenerProductos(busqueda));

            return StatusCode(StatusCodes.Status200OK, vmListaProductos);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarEntrada([FromBody] VMEntrada modelo)
        {

            GenericResponse<VMEntrada> gResponse = new GenericResponse<VMEntrada>();

            try
            {

                ClaimsPrincipal claimUser = HttpContext.User;

                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();



                modelo.IdUsuario = int.Parse(idUsuario);

                Entrada entradaServicio = await _entradaServicio.Registrar(_mapper.Map<Entrada>(modelo));
                modelo = _mapper.Map<VMEntrada>(entradaServicio);



                gResponse.Estado = true;
                gResponse.Objeto = modelo;




            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;

            }



            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpGet]
        public async Task<IActionResult> Historial(string numeroEntrada, string fechaInicio, string fechaFin)
        {
            List<VMEntrada> vmHistorialEntrada = _mapper.Map<List<VMEntrada>>(await _entradaServicio.Historial(numeroEntrada, fechaInicio, fechaFin));

            return StatusCode(StatusCodes.Status200OK, vmHistorialEntrada);
        }


        public IActionResult MostrarPDFVenta(string numeroEntrada)
        {

            string urlPlantillaVista = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/PDFEntrada?numeroEntrada={numeroEntrada}";

            var pdf = new HtmlToPdfDocument()
            {

                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,

                },
                Objects = {
                    new ObjectSettings(){
                       Page   =  urlPlantillaVista
                    }
                  }

            };

            var archivoPDF = _converter.Convert(pdf);

            return File(archivoPDF, "application/pdf");


        }



    }
}
