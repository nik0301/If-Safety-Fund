using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafetyFund.Data.Models
{
    public class Vote
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]
        public string UserId { get; set; }

        [Column("voting_datetime")]
        public DateTime VotingDateTime { get; set; }

        [Required]
        [Column("socialmedia_name")]
        public string SocialName { get; set; }
        
        //FK
        public Project Project { get; set; }

        [Column("project_id")]
        public int ProjectId { get; set; }
    }
}

