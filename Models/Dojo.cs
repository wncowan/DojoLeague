using System.ComponentModel.DataAnnotations;
using DojoLeague.Models;
using System.Collections.Generic;
using System;

namespace DojoLeague.Models {
    public class Dojo : BaseEntity {

        public Dojo() {
            ninjas = new List<Ninja>();
        }
        
        [Key]
        public long Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<Ninja> ninjas { get; set; }
    }
}