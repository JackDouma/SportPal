using System.ComponentModel.DataAnnotations;
namespace SportPal.Models
{
    public class Standing
    {
        /**
         * create fields the standings table will have
         */

        // in the future this can also go much deeper. E.X -> players with a team FK -> player stats with a players fk and so on

        // id
        public int StandingId { get; set; }

        // team
        [Required]
        [MaxLength(25)]
        public string? Team { get; set; }

        // coach
        [Required]
        [MaxLength(25)]
        public string? Coach { get; set; }

        // points
        public int Points { get; set; }

        // wins
        [Range(0, 100, ErrorMessage = "Wins must be between 0-100")]
        public int Wins { get; set; }

        // losses
        [Range(0, 100, ErrorMessage = "Losses must be between 0-100")]
        public int Losses { get; set; }

        // ties
        [Range(0, 100, ErrorMessage = "Ties must be between 0-100")]
        public int Ties { get; set; }

        // forgien key
        [Display(Name = "League")]
        public int LeagueId { get; set; }

        // parent ref
        public League? League { get; set; }
    }
}
