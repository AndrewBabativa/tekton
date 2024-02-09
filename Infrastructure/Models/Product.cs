using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    /// <summary>
    /// Representa un Producto dentro del sistema.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Obtiene o establece el Id del Producto.
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del Producto.
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del estado del Producto. Este campo se obtiene de la caché.
        /// </summary>
        [Required]
        public string StatusName { get; set; }

        /// <summary>
        /// Obtiene o establece el Stock del Producto.
        /// </summary>
        [Required]
        public int Stock { get; set; }

        /// <summary>
        /// Obtiene o establece la descripción del Producto.
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Obtiene o establece el precio del Producto.
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Obtiene o establece el descuento del Producto. Este campo se obtiene de un servicio externo.
        /// </summary>
        public decimal Discount { get; set; }
    }
}
