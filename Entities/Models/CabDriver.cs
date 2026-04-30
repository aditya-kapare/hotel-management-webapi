using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    [Index(nameof(GovernmentId), Name = "IX_Unique_GovernmentId", IsUnique = true)]
    public class CabDriver
    {
        [Key]
        [Required]
        public int DriverId { get; set; }
        
        [Required]
        public string GovernmentId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public string CarVendor { get; set; } = string.Empty;

        public string CarType { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;


        public ICollection<DropPickRequest> DropPickRequests { get; set; } = new List<DropPickRequest>();
    }
}
