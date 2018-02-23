using AutoMapper;
using Vega_New.Controllers.Resources;
using Vega_New.Models;

namespace Vega_New.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //the reason we use automapper in the first place is to seperate the internal details of our domain model from our interface.
            //when we seperated these concerns, we had to create a resource class for the data.
            //so when we get makes from our database using our make class, we need to map the properties of those makes into a makesresource class
            //the resource class will then get serialized and sent back

            //auto mapper maps the properties from one class to another by default if they have the same property name
            //this works one way -- make --> makeresource. not the other way around
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();
        }
    }
}