using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NetCoreExampleAuth.Domain.Core.Model
{
    /// <summary>
    /// Product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Just Id of product.
        /// </summary>
        [Column("ProductId")]
        public int Id { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(80, ErrorMessage = "Maximum length for the Name is 80 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// Is good quality product or not.
        /// </summary>
        [DefaultValue(false)]
        public bool IsGoodQuality { get; set; }

        /// <summary>
        /// Product Color
        /// </summary>
        public ProductColor Color { get; set; }
    }
    /// <summary>
    /// Color of product.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProductColor
    {
        None,

        Red,

        Blue,

        Green
    }
}
