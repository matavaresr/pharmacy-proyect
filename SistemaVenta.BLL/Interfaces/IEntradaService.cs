using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.Entity; 


namespace SistemaVenta.BLL.Interfaces
{
    public interface IEntradaService
    {
        
        Task<List<Producto>> ObtenerProductos(string busqueda);

        Task<Entrada> Registrar(Entrada entidad);

        Task<List<Entrada>> Historial(string numeroEntrada, string fechaInicio, string fechaFin);

        Task<Entrada> Detalle(string numeroEntrada);

        Task<List<DetalleEntrada>> Reporte(string fechaInicio,string fechaFin);


    }
}
