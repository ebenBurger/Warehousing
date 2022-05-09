using System;
using System.ComponentModel.DataAnnotations;

namespace WarehouseReport.Api.Models
{
    public class ClientModel
    {
        [Key]
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mobile { get; set; }
        public string Landline { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Building { get; set; }
        public string OfficePark { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Provence { get; set; }
        public string Location { get; set; }
        public string SpecialInstructions { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
    }
}