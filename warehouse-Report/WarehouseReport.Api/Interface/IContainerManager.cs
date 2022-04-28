using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseReport.Api.Models;

namespace WarehouseReport.Api.Interface
{
    public interface IContainerManager
    {
        List<ContainerModel> GetAllAvailContainers();
        List<ContainerModel> GetAllCompleteContainers();
        Task<int> SaveContainer(ContainerModel containerModel);
        Task<ContainerModel> UpdateContainer(ContainerModel containerModel);
    }
}