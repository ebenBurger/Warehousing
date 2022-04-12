using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseReport.Api.Models;

namespace WarehouseReport.Api.Interface
{
    public interface ICargoManager
    {
        List<CargoModel> GetAllCargo();
        Task<int> SaveCargo(CargoModel cargoModel);
        Task<CargoModel> UpdateCargo(CargoModel cargoModel);
    }
}