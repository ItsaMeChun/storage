using System.ComponentModel.DataAnnotations;

namespace hcode.DTO
{
    public class SauceDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string SauceUrl { get; set; }

        public string SauceImage { get; set; }

        public List<SauceTypeDTO> SauceTypes { get; set; }
    }
}
