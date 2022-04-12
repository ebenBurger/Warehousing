using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using WarehouseReport.Api.Interface;
using WarehouseReport.Api.Models;

namespace WarehouseReport.Api.Managers
{
    
    public class ClientManager : IClientManager
    {
        private readonly ApplicationDbContext _context;

        public ClientManager(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public List<ClientModel> GetAllClients()
        {
            var client = _context.Client
                .Where(a => a.IsActive == true)
                .ToList();
            return client;
        }

        public async Task<int> SaveClient(ClientModel clientModel)
        {
            try
            {
                clientModel.DateCreated = Convert.ToDateTime(DateTime.Now.ToLocalTime()
                    .ToString(System.Globalization.CultureInfo.InvariantCulture));
                clientModel.DateCreated = clientModel.DateCreated;
                _context.Add(clientModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            await _context.SaveChangesAsync();
            return clientModel.ClientId;
        }

        public async Task<ClientModel> UpdateClient(ClientModel clientModel)
        {
            try
            {
                if (clientModel.ClientId == 0)
                {
                    throw new Exception("Invalid Client ID");
                }

                _context.Client.Update(clientModel);
                await _context.SaveChangesAsync();
                return clientModel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}