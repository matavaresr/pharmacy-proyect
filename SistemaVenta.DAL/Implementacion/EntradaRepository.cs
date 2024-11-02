using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity; 


namespace SistemaVenta.DAL.Implementacion
{
    public class EntradaRepository : GenericRepository<Entrada>, IEntradaRepository
    {
        private readonly DbventaContext _dbContext;


        public EntradaRepository(DbventaContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Entrada> Registrar(Entrada entidad)
        {
            Entrada entradaGenerada = new Entrada();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleEntrada dv in entidad.DetalleEntrada)
                    {

                        Producto producto_encontrado = _dbContext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();

                        producto_encontrado.Stock = producto_encontrado.Stock + dv.Cantidad;
                        _dbContext.Productos.Update(producto_encontrado);

                    }   
                    await _dbContext.SaveChangesAsync();


                    NumeroCorrelativo correlativo = _dbContext.NumeroCorrelativos.Where(n => n.Gestion == "entrada").First();


                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaActualizacion = DateTime.Now;

                    
                    _dbContext.NumeroCorrelativos.Update(correlativo);
                    await _dbContext.SaveChangesAsync();


                    string ceros = string.Concat(Enumerable.Repeat("0", correlativo.CantidadDigitos.Value));
                    string numeroEntrada = ceros + correlativo.UltimoNumero.ToString(); 
                    numeroEntrada = numeroEntrada.Substring(numeroEntrada.Length - correlativo.CantidadDigitos.Value, correlativo.CantidadDigitos.Value);

                    entidad.NumeroEntrada = numeroEntrada;
                    await _dbContext.Entrada.AddAsync(entidad);
                    await _dbContext.SaveChangesAsync();

                    entradaGenerada = entidad;

                    transaction.Commit();


                }
                catch (Exception ex){
                    transaction.Rollback();
                    throw ex; 
                
                
                }
            }

            return entradaGenerada;

        }

        public async Task<List<DetalleEntrada>> Reporte(DateTime FechaInicio, DateTime FechaFin)
        {
            List<DetalleEntrada> listaResumen = await _dbContext.DetalleEntrada
            .Include(v => v.IdEntradaNavigation)
            .ThenInclude(u => u.IdUsuarioNavigation)
            .Include(v => v.IdEntradaNavigation)
            .ThenInclude(tdv => tdv.IdTipoDocumentoVentaNavigation)
            .Where(dv => dv.IdEntradaNavigation.FechaRegistro.Value.Date >= FechaInicio.Date &&
            dv.IdEntradaNavigation.FechaRegistro.Value.Date <= FechaFin.Date).ToListAsync();

            return listaResumen; 
        }
    }
}
