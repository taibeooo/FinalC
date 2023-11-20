using System.ComponentModel.DataAnnotations;

namespace Report.Models
{
    public class EditUserViewModel
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
