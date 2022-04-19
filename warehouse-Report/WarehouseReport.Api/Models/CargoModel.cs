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
        public string AtraxInvoiceNumber { get; set; }
        public DateTime? AtraxInvoiceDate { get; set; }
        public double KgCBMConversion { get; set; }
        public double VolumeCbm { get; set; }
        public double VolumeMetric { get; set; }
        public double ChargeableWeight { get; set; }
        public double NumberOfStorageDays { get; set; }
        public double StorageCost { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? StorageInvoiceDate { get; set; }
        public DateTime? CurrentDate { get; set; }
        public double DollarRate { get; set; }
        
        public string DeliveryArea { get; set; }
        public DateTime? DateOfCollection { get; set; }
        public string Transporter { get; set; }
        public double TransporterCost { get; set; }
        public string TransporterInvoiceNumber { get; set; }
        public DateTime? TransporterInvoiceDate { get; set; }
        public string SpecialRequirements { get; set; }

        public string DeleteReason { get; set; }
        
        public bool BilledToJkn { get; set; }
        public bool CommercialInvoiceReceived { get; set; }
        public bool PackingListReceived { get; set; }
        public bool Hazardous { get; set; }
        public bool IsComplete { get; set; }
        public bool IsActive { get; set; }

        public ContainerModel ContainerModel { get; set; }
    }
    
}