using System.ComponentModel.DataAnnotations;

namespace Report.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? UserPassword { get; set; }
        [Required]
        public string? UserEmail { get; set; }
        public string? UserRole { get; set; }
    }
}
