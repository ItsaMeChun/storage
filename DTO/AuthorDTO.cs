using System.ComponentModel.DataAnnotations;

namespace hcode.DTO
{
    public class AuthorDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public List<SauceDTO> Sauces { get; set; }
    }
}
