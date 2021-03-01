using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafetyFund.Data.Models
{
    public class Location
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("country")]
        [DisplayName("Valsts")]
        public string Country { get; set; }
    }
}
