using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{


    public partial class Entrada
    {
        public int IdEntrada { get; set; }

        public string? NumeroEntrada { get; set; }

        public int? IdTipoDocumentoVenta { get; set; }

        public int? IdUsuario { get; set; }

        public string? DocumentoCliente { get; set; }

        public string? NombreCliente { get; set; }

        public decimal? SubTotal { get; set; }

        public decimal? ImpuestoTotal { get; set; }

        public decimal? Total { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public DateTime? FechaCaducidad { get; set; }

        public virtual ICollection<DetalleEntrada> DetalleEntrada { get; set; } = new List<DetalleEntrada>();

        public virtual TipoDocumentoVenta? IdTipoDocumentoVentaNavigation { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }








}
