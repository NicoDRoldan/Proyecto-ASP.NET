using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Models.ViewModel
{
    public class VMDetalleventa
    {
        public int? IdProducto { get; set; }

        public string? MarcaProducto { get; set; }

        public string? DescripcionProducto { get; set; }

        public string? CategoriaProducto { get; set; }

        public int? Cantidad { get; set; }

        public decimal? Precio { get; set; }

        public decimal? Total { get; set; }
    }
}
