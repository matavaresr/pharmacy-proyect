﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMEntrada
    {
        public int IdEntrada { get; set; }

        public string? NumeroEntrada { get; set; }

        public int? IdTipoDocumentoVenta { get; set; }

        public string? TipoDocumentoVenta { get; set; }

        public int? IdUsuario { get; set; }

        public string? Usuario { get; set; }

        public string? DocumentoCliente { get; set; }

        public string? NombreCliente { get; set; }

        public string? SubTotal { get; set; }

        public string? ImpuestoTotal { get; set; }

        public string? Total { get; set; }

        public string? FechaRegistro { get; set; }
        public string? FechaCaducidad { get; set; }

        public virtual ICollection<VMDetalleEntrada> DetalleEntrada { get; set; }




    }
}
