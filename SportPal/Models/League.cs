using System.ComponentModel.DataAnnotations;
namespace SportPal.Models
{
    public class League
    {
        /**
         * create fields the league table will have
         */

        // id
        public int LeagueId { get; set; }

        // name
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        // sport
        [Required]
        [MaxLength(25)]
        public string? Sport { get; set; }

        // organizer
        [Required]
        [MaxLength(25)]
        public string? Organizer { get; set; }
    }
}
