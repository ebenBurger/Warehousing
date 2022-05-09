using System;
using System.ComponentModel.DataAnnotations;

namespace WarehouseReport.Api.Models
{
    public class TransporterModel
    {
        [Key]
        public int TransporterId { get; set; }
        public string? DeliveryArea { get; set; }
        public DateTime? DateOfCollection { get; set; }
        public string? TransporterName { get; set; }
        public double? TransporterCost { get; set; }
        public string? TransporterInvoiceNumber { get; set; }
        public DateTime? TransporterInvoiceDate { get; set; }
        public string? SpecialRequirements { get; set; }
        public DateTime? DateComplete { get; set; }
        
        public bool IsComplete { get; set; }
        public bool IsActive { get; set; }
    }
}