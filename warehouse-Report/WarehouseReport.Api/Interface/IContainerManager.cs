using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseReport.Api.Models;

namespace WarehouseReport.Api.Interface
{
    public interface IContainerManager
    {
        List<ContainerModel> GetAllContainers();
        Task<int> SaveContainer(ContainerModel containerModel);
        Task<ContainerModel> UpdateContainer(ContainerModel containerModel);
    }
}