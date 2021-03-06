using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                .Include(b => b.Cargo)
                .Include(c => c.Container)
                .ToList();

            return data;
        }

        public async Task<int> SavePackage(PackageModel packageModel)
        {
            try
            {
                
                packageModel.DateCreated = Convert.ToDateTime(DateTime.Now.ToLocalTime()
                .ToString(System.Globalization.CultureInfo.InvariantCulture));
                
                var volume = (packageModel.Length * packageModel.Width * packageModel.Height);
                
                packageModel.KgCBMConversion = (packageModel.Weight / 1000);
                packageModel.VolumeCbm = (volume / 1000000) * packageModel.Quantity;
                packageModel.ChargeableWeight = Math.Max(packageModel.VolumeCbm, packageModel.KgCBMConversion);
                packageModel.VolumeMetric = (volume / 6000) * packageModel.Quantity;

                _context.Add(packageModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            await _context.SaveChangesAsync();
            return packageModel.PackageId;
        }

        public async Task<PackageModel> UpdatePackage(PackageModel packageModel)
        {
            try
            {
                if (packageModel.PackageId == 0)
                {
                    throw new Exception("Invalid Package Id");
                }
                
                //this needs to be tha same as above
                var volume = (packageModel.Length * packageModel.Width * packageModel.Height);
                
                packageModel.KgCBMConversion = (packageModel.Weight / 1000);
                packageModel.VolumeCbm = (volume / 1000000) * packageModel.Quantity;
                packageModel.ChargeableWeight = Math.Max(packageModel.VolumeCbm, packageModel.KgCBMConversion);
                packageModel.VolumeMetric = (volume / 6000) * packageModel.Quantity;

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