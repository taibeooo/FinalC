using System.ComponentModel.DataAnnotations;

namespace Report.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string? CategoryName { get; set; }
        public ICollection<Rent_Buy>?  rent_Buys { get; set; }
    }
}
