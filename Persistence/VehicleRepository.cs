using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega_New.Core;
using Vega_New.Core.Models;
using Vega_New.Extensions;
using Vega_New.Models;

namespace Vega_New.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;
        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;

        }
        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if(includeRelated == false)
            {
                return await context.Vehicles.FindAsync(id);
            }
            
            return await context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.ID == id);
        }

        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObject)
        {
            var result = new QueryResult<Vehicle>();
            var query = context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .AsQueryable();

            if (queryObject.MakeId.HasValue)
            {
                query = query.Where(v => v.Model.MakeID == queryObject.MakeId);
            }

            if (queryObject.ModelId.HasValue)
            {
                query = query.Where(v => v.ModelId == queryObject.ModelId);
            }

            //https://stackoverflow.com/questions/793571/why-would-you-use-expressionfunct-rather-than-funct - explanation of expression and func
            //func is a delegate for the linq method expression (v => v.Whatever)
            //we need to use Expression<Func> here instead of just Func<> because expression returns meta data about the lamba expression whereas func just executes the code
            //we are using this lambda with LINQ, which tries to convert it into a SQL statement. 
            //it cannot do this conversion unless it is an Expression<Func> because it needs the metadata contained within expression
            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName
            };

            query = query.ApplyOrdering(queryObject, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObject);

            result.Items = await query.ToListAsync();

            return result;

            //cleaner way of doing this is above.
            // if (queryObject.SortBy == "make")
            // {
            //     query = (queryObject.IsSortAscending) ? query.OrderBy(v => v.Model.Make.Name) : query.OrderByDescending(v => v.Model.Make.Name);
            // }

            // if (queryObject.SortBy == "model")
            // {
            //     query = (queryObject.IsSortAscending) ? query.OrderBy(v => v.Model.Name) : query.OrderByDescending(v => v.Model.Name);
            // }

            // if (queryObject.SortBy == "contactName")
            // {
            //     query = (queryObject.IsSortAscending) ? query.OrderBy(v => v.ContactName) : query.OrderByDescending(v => v.ContactName);
            // }

            // if (queryObject.SortBy == "id")
            // {
            //     query = (queryObject.IsSortAscending) ? query.OrderBy(v => v.ID) : query.OrderByDescending(v => v.ID);
            // }
        }

        
        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Remove(vehicle);
        }
    }
}