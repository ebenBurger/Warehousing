using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarehouseReport.Api.Interface;
using WarehouseReport.Api.Models;

namespace WarehouseReport.Api.Managers
{
    public class ContainerManager : IContainerManager
    {
        private readonly ApplicationDbContext _context;

        public ContainerManager(ApplicationDbContext context)
        {
            _context = context;
        }


        public List<ContainerModel> GetAllAvailContainers()
        {
            var data = _context.Container
                .Where(a => a.IsActive == true)
                .Where(c => c.IsComplete == false)
                .Include(b => b.Cargo)
                .Include(d => d.Package)
                .ToList();

            return data;
        }

        public List<ContainerModel> GetAllCompleteContainers()
        {
            var data = _context.Container
                .Where(a => a.IsActive == true)
                .Where(c => c.IsComplete == true)
                .Include(b => b.Cargo)
                .Include(d => d.Package)
                .ToList();

            return data;
        }

        public async Task<int> SaveContainer(ContainerModel containerModel)
        {
            try
            {
                containerModel.DateCreated = Convert.ToDateTime(DateTime.Now.ToLocalTime()
                    .ToString(System.Globalization.CultureInfo.InvariantCulture));
                _context.Add(containerModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            await _context.SaveChangesAsync();
            return containerModel.ContainerId;
        }

        public async Task<ContainerModel> UpdateContainer(ContainerModel containerModel)
        {
            try
            {
                if (containerModel.ContainerId == 0)
                {
                    throw new Exception("Invalid Container Id");
                }

                if (containerModel.PackingDate != null)
                {
                    containerModel.Status = "Loading";
                }
                if (containerModel.PackingDate == null)
                {
                    containerModel.Status = "Available";
                }

                if (containerModel.IsComplete)
                {
                    containerModel.DateCompleted = Convert.ToDateTime(DateTime.Now.ToLocalTime()
                        .ToString(System.Globalization.CultureInfo.InvariantCulture));
                    containerModel.Status = "Complete";
                }

                _context.Container.Update(containerModel);
                await _context.SaveChangesAsync();
                return containerModel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}