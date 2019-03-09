using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCastle.Models
{
    public class Bug
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Summary { get; set; }
        public DateTime TimeCreated => DateTime.Now;

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
