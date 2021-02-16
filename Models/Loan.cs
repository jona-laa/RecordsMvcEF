using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordsMvcEf.Models
{
    public class Loan
    {
        public int Id { get; set; }

        [Required, Display(Name = "Lent To")]
        public string LentTo { get; set; }

        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}"), Display(Name = "Loan Date")]
        public DateTime LoanDate { get; set; }

        [Display(Name = "Is Returned")]
        public bool IsReturned { get; set; } = false;

        [Required, Display(Name = "Album")]
        public int AlbumId { get; set; }

        [ForeignKey("AlbumId")]
        public Album Album { get; set; }
    }
}