using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetCoreAxampleAuth.Patterns.Models.Product
{
    /// <summary>
    /// Product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Just Id of product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        [Required]
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
