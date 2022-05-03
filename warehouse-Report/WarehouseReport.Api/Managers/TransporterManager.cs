using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarehouseReport.Api.Interface;
using WarehouseReport.Api.Models;

namespace WarehouseReport.Api.Managers
{
    public class TransporterManager : ITransporterManager
    {
        private readonly ApplicationDbContext _context;

        public TransporterManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<TransporterModel> GetAllTransporter()
        {
            var data = _context.Transporter
                .Where(a => a.IsActive == true)
                .Include(b => b.Package)
                .ToList();

            return data;
        }

        public async Task<int> SaveTransporter(TransporterModel transporterModel)
        {
            try
            {
                _context.Add(transporterModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            await _context.SaveChangesAsync();
            return transporterModel.TransporterId;
        }

        public Task<TransporterModel> UpdateTransporter(TransporterModel transporterModel)
        {
            throw new System.NotImplementedException();
        }
    }
}