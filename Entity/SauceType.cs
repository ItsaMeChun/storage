using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hcode.Entity
{
    public class SauceType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Type")]
        [Column("type_id")]
        public int TypeId { get; set; }

        public Types Type { get; set; }

        [ForeignKey("Sauce")]
        [Column("sauce_id")]
        public int SauceId { get; set; }

        public Sauce Sauce { get; set; }

    }
}
