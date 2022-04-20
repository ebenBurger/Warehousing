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

    public List<CargoModel> GetAllCargo()
    {
        var data = _context.Cargo
            .Include(a => a.ContainerModel)
            .Where(a => a.IsActive == true)
            //TODO update the currentDate var to the actual current date.
            // .(b => b.CurrentDate = DateTime.Now)
            .ToList();

        return data;
    }

    public async Task<int> SaveCargo(CargoModel cargoModel)
    {
        try
        {
            // var volume = (cargoModel.Length * cargoModel.Width * cargoModel.Height);
            // cargoModel.DateCreated = Convert.ToDateTime(DateTime.Now.ToLocalTime()
            //     .ToString(System.Globalization.CultureInfo.InvariantCulture));
            // cargoModel.DateCreated = cargoModel.DateCreated;
            cargoModel.EndDateOfFreeStorage = cargoModel.DateCollected.AddDays(7);
            cargoModel.CargoReadyPlace = "Atrax";
            // cargoModel.KgCBMConversion = cargoModel.Weight / 1000;
            // cargoModel.VolumeCbm = (volume / 1000000) *
            //                     cargoModel.Quantity;
            // cargoModel.ChargeableWeight = Math.Max(cargoModel.VolumeCbm, cargoModel.KgCBMConversion);
            // cargoModel.VolumeMetric = volume / 6000;
            if (cargoModel.DollarRate == 0)
            {
                cargoModel.DollarRate = 0.55;
            }
            // DateTime thisDay = DateTime.Today;
            // cargoModel.NumberOfStorageDays = (thisDay - cargoModel.EndDateOfFreeStorage).Days;
            // cargoModel.StorageCost = (2.7 * cargoModel.ChargeableWeight) * cargoModel.NumberOfStorageDays;
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
    }
}