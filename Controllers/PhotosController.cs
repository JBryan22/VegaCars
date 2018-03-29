using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Vega_New.Controllers.Resources;
using Vega_New.Core;
using Vega_New.Core.Models;
using VegaCars.Core;

namespace Vega_New.Controllers {

    [Route ("api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller {
        private readonly IHostingEnvironment host;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly PhotoSettings photoSettings;
        private readonly IPhotoRepository photoRepository;
        public PhotosController (IHostingEnvironment host, IVehicleRepository repository, IPhotoRepository photoRepository, IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<PhotoSettings> options) {
            this.photoRepository = photoRepository;
            this.photoSettings = options.Value;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.host = host;
        }

        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotos (int vehicleId) {
            var photos = await photoRepository.GetPhotos (vehicleId);

            return mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }

        [HttpPost]
        public async Task<IActionResult> Upload (int vehicleId, IFormFile file) {
            var vehicle = await repository.GetVehicle (vehicleId, includeRelated : false);
            if (vehicle == null) {
                return NotFound ();
            }

            if (file == null) {
                return BadRequest ("Null file");
            }

            if (file.Length == 0) {
                return BadRequest ("Empty file");
            }

            if (file.Length > photoSettings.MaxBytes) {
                return BadRequest ("File size exceeded");
            }

            if (!photoSettings.IsSupported (file.FileName)) {
                return BadRequest ("Not a supported file type");
            }

            var uploadsFolderPath = Path.Combine (host.WebRootPath, "uploads");
            if (!Directory.Exists (uploadsFolderPath)) {
                Directory.CreateDirectory (uploadsFolderPath);
            }

            var fileName = Guid.NewGuid ().ToString () + Path.GetExtension (file.FileName);
            var filePath = Path.Combine (uploadsFolderPath, fileName);

            using (var stream = new FileStream (filePath, FileMode.Create)) {
                await file.CopyToAsync (stream);
            }

            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add (photo);
            await unitOfWork.CompleteAsync ();

            return Ok (mapper.Map<Photo, PhotoResource> (photo));
        }
    }
}