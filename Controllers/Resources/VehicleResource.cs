using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vega_New.Controllers.Resources
{
    public class VehicleResource
    {
        //the reason we created this class and the save resource class is because they serve different purposes. When we create/update/delete an object(vehicle) we don't need
        //the entire object including model objects, make objects, contactinfo etc. We just need the modelID and featureId's.
        //
        public int ID { get; set; }
        public KeyValuePairResource Model { get; set; }
        public KeyValuePairResource Make { get; set; }
        public bool IsRegistered { get; set; }
        public ContactResource Contact { get; set; }
        public DateTime LastUpdate { get; set; }
        public ICollection<KeyValuePairResource> Features { get; set; }

        public VehicleResource()
        {
            Features = new Collection<KeyValuePairResource>();
        }
    }
}