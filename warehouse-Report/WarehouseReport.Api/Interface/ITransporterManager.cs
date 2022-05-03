using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseReport.Api.Models;

namespace WarehouseReport.Api.Interface
{
    public interface ITransporterManager
    {
        List<TransporterModel> GetAllTransporter();

        Task<int> SaveTransporter(TransporterModel transporterModel);

        Task<TransporterModel> UpdateTransporter(TransporterModel transporterModel);
    }
}