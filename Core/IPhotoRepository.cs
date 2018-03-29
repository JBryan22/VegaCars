using System.Collections.Generic;
using System.Threading.Tasks;
using Vega_New.Core.Models;

namespace VegaCars.Core
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}