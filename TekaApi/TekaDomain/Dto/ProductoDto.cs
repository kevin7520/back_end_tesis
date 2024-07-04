using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekaDomain.Dto
{

    public class CreateProductoDto
    {
        public int IdCategoria { get; set; }
        public string CodigoProducto { get; set; }
        public string Modelo { get; set; }
        public int IdEstadoProducto { get; set; }
        public string SerieProducto { get; set; }
        public decimal Precio { get; set; }
    }

    public class UpdateProductoDto
    {
        public int IdCategoria { get; set; }
        public string Modelo { get; set; }
        public int IdEstadoProducto { get; set; }
        public string SerieProducto { get; set; }
        public decimal Precio { get; set; }
    }

}
