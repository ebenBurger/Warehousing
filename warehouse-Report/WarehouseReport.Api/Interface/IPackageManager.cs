using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseReport.Api.Models;

namespace WarehouseReport.Api.Interface
{
    public interface IPackageManager
    {
        List<PackageModel> GetAllPackages();
        Task<int> SavePackage(PackageModel packageModel);
        Task<PackageModel> UpdatePackage(PackageModel packageModel);
    }
}