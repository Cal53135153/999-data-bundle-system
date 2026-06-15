using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBundleSystem.Models
{
    public class Bundle
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string DataSize { get; set; } = string.Empty;
        public int ValidityDays { get; set; }
        public bool IsAvailable { get; set; }
    }
}