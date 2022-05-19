using Basketbool.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Basketbool.Web.Models
{
    public class CloseMatchViewModel
    {
        public int MatchId { get; set; }

        public int MatchDayId { get; set; }

        public int LocalId { get; set; }

        public int VisitorId { get; set; }

        [Display(Name = "Points Local")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int? PointsLocal { get; set; }

        [Display(Name = "Points Visitor")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int? PointsVisitor { get; set; }

        public MatchDayEntity MatchDay { get; set; }

        public TeamEntity Local { get; set; }

        public TeamEntity Visitor { get; set; }
    }
}
