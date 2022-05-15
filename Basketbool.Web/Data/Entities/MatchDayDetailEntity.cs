using System.ComponentModel.DataAnnotations;

namespace Basketbool.Web.Data.Entities
{
    public class MatchDayDetailEntity
    {
        public int Id { get; set; }

        public TeamEntity Team { get; set; }

        [Display(Name = "Matches Played")]
        public int MatchesPlayed { get; set; }

        [Display(Name = "Matches Won")]
        public int MatchesWon { get; set; }

        [Display(Name = "Matches Tied")]
        public int MatchesTied { get; set; }

        [Display(Name = "Matches Lost")]
        public int MatchesLost { get; set; }

        [Display(Name = "Points For")]
        public int PointsFor { get; set; }

        [Display(Name = "Points Against")]
        public int PointsAgainst { get; set; }

        [Display(Name = "Points Difference")]
        public int PointsDifference => PointsFor - PointsAgainst;

        public MatchDayEntity MatchDay { get; set; }
    }
}

