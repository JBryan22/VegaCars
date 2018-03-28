using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Vega_New.Core;
using Vega_New.Core.Models;
using Vega_New.Models;

namespace Vega_New.Core
{
    public interface IVehicleRepository
    {
         Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
         void Add(Vehicle vehicle);
         void Remove(Vehicle vehicle);
         
         Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery filter);
    }
}