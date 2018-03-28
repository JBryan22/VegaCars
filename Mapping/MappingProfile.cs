using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Vega_New.Controllers.Resources;
using Vega_New.Core.Models;
using Vega_New.Models;
using static Vega_New.Controllers.Resources.SaveVehicleResource;

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

            //These 3 are Domain to API Resource
            //queryresult is a generic type which is why we have to use typeof
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<Photo, PhotoResource>();
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairResource { ID = vf.Feature.ID, Name = vf.Feature.Name})))
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make));

            //API Resource to Domain
            CreateMap<VehicleQueryResource, VehicleQuery>();

            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.ID, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) => {

                    var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId)).ToList();
                    foreach(var f in removedFeatures)
                    {
                        v.Features.Remove(f);
                    }

                    var addedFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature { FeatureId = id }).ToList();
                    foreach (var f in addedFeatures)
                    {
                        v.Features.Add(f);
                    }
                });
        }
    }
}