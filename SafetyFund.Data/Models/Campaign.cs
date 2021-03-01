using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafetyFund.Data.Models
{
    public class Campaign
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("title")]
        [Display(Name = "Nosaukums")]
        public string Title { get; set; }

        [Column("start_datetime")]
        [Display(Name = "Sākuma datums")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartDateTime { get; set; }

        [Column("end_datetime")]
        [Display(Name = "Beigu datums")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime EndDateTime { get; set; }
    }
}
