using System.ComponentModel.DataAnnotations;

namespace buoi5.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string className { get; set; }


        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
