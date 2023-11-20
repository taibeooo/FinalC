using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report.Models
{
    public class Rent_Buy
    {
        [Key] 
        public int RentBuyId { get; set; }
        [Required]
        public string? RentBuyName { get; set; }
        [Required]  
        public string? RentBuyDescription { get; set; }
        [Required]
        public string? RentBuyProvince { get; set;}
        [Required]
        public int? RentBuyPrice { get; set; }
        [Required]
        public int? RentBuyArea { get; set; }
        [Required]
        [StringLength(50)]
        public string? RentBuyPhone { get; set; }
        [Required]
        public string? RentBuyImage { get; set; }
        public string? RentBuyImage1 { get; set; }
        public string? RentBuyImage2 { get; set; }
        [Required]
        [StringLength(50)]
        public string? RentBuyStatus { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? UserId { get; set; }
    }
}
