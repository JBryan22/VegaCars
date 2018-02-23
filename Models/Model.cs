using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vega_New.Models
{
    [Table("Models")]
    public class Model
    {
        public int ID { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        //ef knows that make and makeId are referring to the same thing by using this naming convention (classname+Id)
        //note that the id type should match that of the make id type (int and int)
        public Make Make { get; set; }
        public int MakeID { get; set; }
    }
}