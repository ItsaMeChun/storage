using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hcode.Entity
{
    public class SauceHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("date_upload")]
        public DateTime DateUpload { get; set; }

        [ForeignKey("Sauce")]
        [Column("sauce_id")]
        public int SauceId { get; set; }

        public virtual Sauce Sauce { get; set; }
    }
}
