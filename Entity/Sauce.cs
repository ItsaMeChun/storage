﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hcode.Entity
{
    public class Sauce
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Column("sauce_url")]
        public string SauceUrl { get; set; }

        [Column("sauce_image")]
        public string SauceImage { get; set; }

        [ForeignKey("Author")]
        [Column("author_id")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public List<SauceHistory> SauceHistory { get; set; }

        public List<SauceType> SauceTypes { get; set; }

    }
}
