using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_Web.Models.Dto
{
    // This VillaDTO class is being used in place of the actual model class Villa.
    // This is to hide some properties that we don't want to return to the end users.

    public class VillaUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public string Details {get; set;}

        [Required]
        public double Rate {get; set;}

        [Required]
        public int Occupancy { get; set; }

        [Required]
        public int Sqft { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }
    }
}