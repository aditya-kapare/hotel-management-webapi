using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CabDriver
    {
        [Key]
        [Required]
        public int DriverId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public string CarVendor { get; set; } = string.Empty;

        public string CarType { get; set; } = string.Empty;

        public ICollection<DropPickRequest> DropPickRequests { get; set; } = new List<DropPickRequest>();
    }
}
