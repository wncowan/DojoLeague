using System.ComponentModel.DataAnnotations;
using DojoLeague.Models;
using System;

namespace DojoLeague.Models {
    public class Ninja : BaseEntity {
        [Key]
        public long Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public int Level { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Dojo Dojo { get; set; }

    
    }
}