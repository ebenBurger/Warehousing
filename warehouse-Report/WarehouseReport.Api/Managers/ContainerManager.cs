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


        public List<ContainerModel> GetAllContainers()
        {
            var data = _context.Container
                .Where(a => a.IsActive == true)
                .Include(b => b.CargoModels)
                .ToList();

            return data;
        }

        public async Task<int> SaveContainer(ContainerModel containerModel)
        {
            try
            {
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