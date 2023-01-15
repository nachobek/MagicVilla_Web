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

        public string Details { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Please enter a number greater than 0.")]
        public double? Rate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a number greater than 0.")]
        public int? Occupancy { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a number greater than 0.")]
        public int? Sqft { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }
    }
}