using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RecordsMvcEf.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        
        [Display(Name = "Artist"), Required(ErrorMessage = "Field is required")]
        public string ArtistName { get; set; }
    }
}