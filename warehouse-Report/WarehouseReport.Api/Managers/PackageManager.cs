using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarehouseReport.Api.Interface;
using WarehouseReport.Api.Models;

namespace WarehouseReport.Api.Managers
{
    public class PackageManager : IPackageManager
    {
        private readonly ApplicationDbContext _context;

        public PackageManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<PackageModel> GetAllPackages()
        {
            var data = _context.Package
                .Where(a => a.IsActive == true)
                .Include(b => b.CargoModel)
                .ToList();

            return data;
        }

        public async Task<int> SavePackage(PackageModel packageModel)
        {
            try
            {
                var volume = (packageModel.Length * packageModel.Width * packageModel.Height);
                packageModel.DateCreated = Convert.ToDateTime(DateTime.Now.ToLocalTime()
                .ToString(System.Globalization.CultureInfo.InvariantCulture));
                packageModel.DateCreated = packageModel.DateCreated;
                packageModel.KgCBMConversion = packageModel.Weight / 1000;
                packageModel.VolumeCbm = (volume / 1000000) *
                packageModel.Quantity;
                packageModel.ChargeableWeight = Math.Max(packageModel.VolumeCbm, packageModel.KgCBMConversion);
                packageModel.VolumeMetric = volume / 6000;

                _context.Add(packageModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            await _context.SaveChangesAsync();
            return packageModel.CargoId;
        }

        public async Task<PackageModel> UpdatePackage(PackageModel packageModel)
        {
            try
            {
                if (packageModel.PackageId == 0)
                {
                    throw new Exception("Invalid Package Id");
                }

                _context.Package.Update(packageModel);
                await _context.SaveChangesAsync();
                return packageModel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}