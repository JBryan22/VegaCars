using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vega_New.Controllers.Resources
{
    //the reason we create resource classes is to add a layer of abstraction between our database domain model and the public interface.
    //we dont want to directly serialize our domain model and send it over to angular. if a client depends on our implementation details, any change would break the client.
    //by creating the resource we can change the internal details without changing or breaking the public interface
    //it also creates a problem in JSON data because it creates a loop with our data. Makes contain 0..n amount of models and models depend on a make.
    //this works fine in EF but doesn't work with our json data. By creating the resource we remove the properties associating models with a make
    public class MakeResource : KeyValuePairResource
    {
        public ICollection<KeyValuePairResource> Models { get; set; }

        
        public MakeResource()
        {
            Models = new Collection<KeyValuePairResource>();
        }
    }
}