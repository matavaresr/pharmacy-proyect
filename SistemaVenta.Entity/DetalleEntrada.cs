using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{


    public partial class DetalleEntrada
    {
        public int IdDetalleEntrada { get; set; }

        public int? IdEntrada { get; set; }

        public int? IdProducto { get; set; }

        public string? MarcaProducto { get; set; }

        public string? DescripcionProducto { get; set; }

        public string? CategoriaProducto { get; set; }

        public int? Cantidad { get; set; }

        public decimal? Precio { get; set; }

        public decimal? Total { get; set; }

        public virtual Entrada? IdEntradaNavigation { get; set; }
    }

}
