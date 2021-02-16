using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordsMvcEf.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        
        [Display(Name = "Album Name"), Required(ErrorMessage = "Field is required")]
        public string AlbumName { get; set; }

        [Display(Name = "Release Date"), Required(ErrorMessage = "Field is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Artist")]
        public int ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        public Artist Artist { get; set; }
    }
}