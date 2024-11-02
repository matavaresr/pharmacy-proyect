using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;




namespace SistemaVenta.BLL.Implementacion
{
    public class EntradaService : IEntradaService
    {

        private readonly IGenericRepository<Producto> _repositorioProducto;
        private readonly IEntradaRepository _repositorioEntrada;


        public EntradaService(IGenericRepository<Producto> repositorioProducto,
            IEntradaRepository repositorioEntrada)
        {
            _repositorioProducto = repositorioProducto;
            _repositorioEntrada = repositorioEntrada; 
        }

        public async Task<List<Producto>> ObtenerProductos(string busqueda)
        {
            IQueryable<Producto> query = await _repositorioProducto.Consultar(
                p => p.EsActivo == true &&
                p.Stock >= 0 &&
                string.Concat(p.CodigoBarra, p.Marca, p.Descripcion).Contains(busqueda)
                );

            return query.Include(c => c.IdCategoriaNavigation).ToList();

        }

        public async Task<Entrada> Registrar(Entrada entidad)
        {
            try
            {
                return await _repositorioEntrada.Registrar(entidad);
            }
            catch {
                throw;
            
            }



        }
        public async Task<List<Entrada>> Historial(string numeroEntrada, string fechaInicio, string fechaFin)
        {
            IQueryable<Entrada> query = await _repositorioEntrada.Consultar();
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;


            if (fechaInicio != "" && fechaFin != "")
            {

                DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-MX"));
                DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-MX"));


              return  query
                    
                    .Where(v =>
                v.FechaRegistro.Value.Date >= fech_inicio.Date &&
                v.FechaRegistro.Value.Date <= fech_fin.Date
                )
            .Include(tdv => tdv.IdTipoDocumentoVentaNavigation)
            .Include(u => u.IdUsuarioNavigation)
            .Include(dv => dv.DetalleEntrada)
            .ToList();

            }
            else {

                return query.Where(v => v.NumeroEntrada == numeroEntrada
                )
            .Include(tdv => tdv.IdTipoDocumentoVentaNavigation)
            .Include(u => u.IdUsuarioNavigation)
            .Include(dv => dv.DetalleEntrada)
            .ToList();



            }

        }
        public async Task<Entrada> Detalle(string numeroEntrada)
        {
            IQueryable<Entrada> query = await _repositorioEntrada.Consultar(v => v.NumeroEntrada == numeroEntrada);
            
            
         return   query
         .Include(tdv => tdv.IdTipoDocumentoVentaNavigation)
         .Include(u => u.IdUsuarioNavigation)
         .Include(dv => dv.DetalleEntrada)
         .First();





        }

      


        public async Task<List<DetalleEntrada>> Reporte(string fechaInicio, string fechaFin)
        {

            DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-MX"));
            DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-MX"));

            List<DetalleEntrada> lista = await _repositorioEntrada.Reporte(fech_inicio, fech_fin);

            return lista;





        }
    }
}
