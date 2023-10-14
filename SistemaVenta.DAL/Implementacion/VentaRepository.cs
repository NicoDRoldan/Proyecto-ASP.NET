using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.DAL.Implementacion
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private readonly DbventaContext _dbContext;

        public VentaRepository(DbventaContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Venta> Registrar(Venta entidad)
        {
            Venta vtaGenerada = new Venta();

            using (var transacion = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleVenta dv in entidad.DetalleVenta) 
                    {
                        Producto producto = _dbContext.Productos.Where(P=>P.IdProducto == dv.IdProducto).First();
                        producto.Stock = producto.Stock - dv.Cantidad;
                        _dbContext.Productos.Update(producto);
                    }
                    await _dbContext.SaveChangesAsync();

                    NumeroCorrelativo nroCorrelativo = _dbContext.NumeroCorrelativos.Where(n=>n.Gestion == "venta").First();

                    nroCorrelativo.UltimoNumero = nroCorrelativo.UltimoNumero + 1;
                    nroCorrelativo.FechaActualizacion = DateTime.Now;

                    _dbContext.NumeroCorrelativos.Update(nroCorrelativo);
                    await _dbContext.SaveChangesAsync();

                    string ceros = string.Concat(Enumerable.Repeat("0", nroCorrelativo.CantidadDigitos.Value));
                    string numeroVenta = ceros + nroCorrelativo.ToString();
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - nroCorrelativo.CantidadDigitos.Value, nroCorrelativo.CantidadDigitos.Value);

                    entidad.NumeroVenta = numeroVenta;

                    await _dbContext.Venta.AddAsync(entidad);
                    await _dbContext.SaveChangesAsync();

                    vtaGenerada = entidad;

                    transacion.Commit();
                }
                catch (Exception ex)
                {
                    transacion.Rollback();
                    throw ex;
                }
            }
            return vtaGenerada;
        }

        public async Task<List<DetalleVenta>> Reporte(DateTime fechaInicio, DateTime fechaFin)
        {
            List<DetalleVenta> listaResumen = await _dbContext.DetalleVenta
                .Include(v => v.IdVentaNavigation)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .Include(v => v.IdVentaNavigation)
                .ThenInclude(tdv => tdv.IdTipoDocumentoVentaNavigation)
                .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date>=fechaInicio.Date &&
                dv.IdVentaNavigation.FechaRegistro.Value.Date <= fechaFin.Date).ToListAsync();

            return listaResumen;
        }
    }
}
