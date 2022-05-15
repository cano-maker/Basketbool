using System.ComponentModel.DataAnnotations;

namespace Basketbool.Web.Data.Entities
{
    public class PredictionEntity
    {
        public int Id { get; set; }

        public MatchEntity Match { get; set; }

        public UserEntity User { get; set; }

        [Display(Name = "Points Local")]
        public int? PointsLocal { get; set; }

        [Display(Name = "Points Visitor")]
        public int? PointsVisitor { get; set; }

        public int Points { get; set; }
    }
}
