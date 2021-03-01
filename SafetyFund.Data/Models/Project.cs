using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafetyFund.Data.Models
{
    public class Project
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        [Display(Name = "Nosaukums")]
        [Required]
        public string Title { get; set; }

        [Column("image")]
        [Display(Name = "Bilde")]
        public byte[] Image { get; set; }

        [Column("intro")]
        [Display(Name = "Intro")]
        [Required]
        public string Intro { get; set; }

        [Column("description")]
        [Display(Name = "Apraksts")]
        [Required]
        public string Description { get; set; }

        [Column("order_number")]
        [Required]
        [Display(Name = "Secības numurs")]
        public int OrderNumber { get; set; }
        
        //FK
        public Campaign Campaign { get; set; }

        [Column("campaign_id")]
        public int CampaignId { get; set; }
    }
}
