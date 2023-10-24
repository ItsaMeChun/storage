using System.ComponentModel.DataAnnotations;

namespace hcode.Entity
{
    public class Author
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
