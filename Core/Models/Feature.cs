using System.ComponentModel.DataAnnotations;

namespace Vega_New.Models
{
    public class Feature
    {
        public int ID { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}