using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarehouseReport.Api.Interface;
using WarehouseReport.Api.Models;

namespace WarehouseReport.Api.Managers
{
    public class CargoManager: ICargoManager

    {
    private readonly ApplicationDbContext _context;

    public CargoManager(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<CargoModel> GetAllAvailCargo()
    {
        var data = _context.Cargo
            .Include(b => b.PackageModels)
            .Include(c => c.Container)
            .Where(a => a.IsActive == true)
            .Where(d => d.IsComplete == false)
            .ToList();

        return data;
    }

    public List<CargoModel> GetAllCompleteCargo()
    {
        var data = _context.Cargo
            .Include(b => b.PackageModels)
            .Include(c => c.Container)
            .Where(a => a.IsActive == true)
            .Where(d => d.IsComplete == true)
            .ToList();

        return data;
    }

    public async Task<int> SaveCargo(CargoModel cargoModel)
    {
        try
        {
            cargoModel.DateCreated = Convert.ToDateTime(DateTime.Now.ToLocalTime()
            .ToString(System.Globalization.CultureInfo.InvariantCulture));
            cargoModel.EndDateOfFreeStorage = cargoModel.DateReceived.AddDays(7);
            cargoModel.CargoReadyPlace = "Atrax";
            
            if (cargoModel.DollarRate == 0)
            {
                cargoModel.DollarRate = 0.55;
            }
            _context.Add(cargoModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        await _context.SaveChangesAsync();
        return cargoModel.CargoId;
    }

    public async Task<CargoModel> UpdateCargo(CargoModel cargoModel)
    {
        try
        {
            if (cargoModel.CargoId == 0)
            {
                throw new Exception("Invalid Cargo Id");
            }

            if (cargoModel.ContainerId != null)
            {
                cargoModel.PackDate = Convert.ToDateTime(DateTime.Now.ToLocalTime()
                    .ToString(System.Globalization.CultureInfo.InvariantCulture));
                cargoModel.NumberOfStorageDays = (cargoModel.PackDate.Date - cargoModel.EndDateOfFreeStorage.Date).Days;
                cargoModel.StorageCost = (cargoModel.DollarRate * cargoModel.TotalChargeableWeight) * cargoModel.NumberOfStorageDays;
                cargoModel.DateComplete = Convert.ToDateTime(DateTime.Now.ToLocalTime()
                    .ToString(System.Globalization.CultureInfo.InvariantCulture));
            }

            _context.Cargo.Update(cargoModel);
            await _context.SaveChangesAsync();
            return cargoModel;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public CargoModel GetById(int cargoId)
    {
        try
        {
            var data = _context.Cargo
                .Include(a => a.PackageModels)
                .Single(b => b.CargoId == cargoId);

            return data;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    }
}