using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafetyFund.Data.Models
{
    public class User
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("full_name")]
        [Display(Name = "Vārds, uzvārds")]
        [Required]
        public string FullName { get; set; }
    }
}