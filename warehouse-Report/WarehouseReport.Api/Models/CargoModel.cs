using System;
using System.ComponentModel.DataAnnotations;

namespace WarehouseReport.Api.Models
{
    public class CargoModel
    {
        [Key]
        public int CargoId { get; set; }
        public string Supplier { get; set; }
        public DateTime DateCollected { get; set; }
        public string OrderNumber { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Weight { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string CargoReadyPlace { get; set; }
        public DateTime EndDateOfFreeStorage { get; set; }
        public string InvoieNumber { get; set; }
        public double KgCBMConvertion { get; set; }
        public double Volume { get; set; }
        public double ChargeableWeigth { get; set; }
        public int NumberOfStorageDays { get; set; }
        public double StorageCost { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
    }
}