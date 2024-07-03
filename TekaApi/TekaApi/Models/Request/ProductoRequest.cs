using System.ComponentModel.DataAnnotations;

namespace TekaApi.Models.Request
{
    public class ProductoRequest
    {
        [Key]
        public int IdProducto { get; set; }

        public int IdCategoria { get; set; }

        [Required]
        [StringLength(50)]
        public string CodigoProducto { get; set; }

        [Required]
        [StringLength(50)]
        public string Modelo { get; set; }

        public int IdEstadoProducto { get; set; }

        [Required]
        [StringLength(50)]
        public string SerieProducto { get; set; }

        [Required]
        public decimal Precio { get; set; }
    }
}
