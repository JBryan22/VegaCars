using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Vega_New.Models
{
    public class Make
    {
        public int ID { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; }

        //we create a contructor that initializes models because we dont want other classes to need to initialize the models (make.Models = new Collection...).
        //it is the job of this class to deal with initialization of its properties. this helps us avoid null ref exceptions if someone forgets to initialize models somewhere in the code
        public Make()
        {
            //we use a collection here instead of a list because Lists allow you to access element based on an index (arr[0]) whereas collections do not
            //this is desirable because it is unlikely we would want to access models inside of asp.net. We just was to serialize it and send it to angular
            Models = new Collection<Model>();
        }
    }
}