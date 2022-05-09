using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseReport.Api.Models;

namespace WarehouseReport.Api.Interface
{
    public interface IClientManager
    {
        List<ClientModel> GetAllClients();
        Task<int> SaveClient(ClientModel clientModel);
        Task<ClientModel> UpdateClient(ClientModel clientModel);
    }
}