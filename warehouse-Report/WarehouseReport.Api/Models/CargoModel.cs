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
        public double Quantity { get; set; }
        public double Weight { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string CargoReadyPlace { get; set; }
        public DateTime EndDateOfFreeStorage { get; set; }
        public string InvoieNumber { get; set; }
        public double KgCBMConvertion { get; set; }
        public double Volume { get; set; }
        public double ChargeableWeight { get; set; }
        public double NumberOfStorageDays { get; set; }
        public double StorageCost { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime StorageInvoiceDate { get; set; }
        public DateTime CurrentDate { get; set; }
        public double DollarRate { get; set; }
    }
    
}