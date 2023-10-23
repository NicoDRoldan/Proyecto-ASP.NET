﻿namespace SistemaVenta.AplicacionWeb.Models.ViewModel
{
    public class VMReporteVenta
    {
        public string? FechaRegistro { get; set; }

        public string? NumeroVenta{ get; set;}

        public string? TipoDocumento { get; set;}

        public string? DocumentoCliente { get; set;}

        public string? NombreCliente { get; set;}

        public string? SubtotalVenta { get; set;}

        public string? ImuestoTotalVenta { get; set;}

        public string? TotalVenta { get; set;}

        public string? Producto { get;set;}

        public int? Cantidad { get;set;}

        public string? Precio { get; set;}

        public string? Total { get; set;}
    }
}
