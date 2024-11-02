using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.Entity; 

namespace SistemaVenta.DAL.Interfaces
{
    public interface IEntradaRepository : IGenericRepository<Entrada>
    {
        Task<Entrada> Registrar(Entrada entidad);

        Task<List<DetalleEntrada>> Reporte(DateTime FechaInicio, DateTime FechaFin); 


    }
}
