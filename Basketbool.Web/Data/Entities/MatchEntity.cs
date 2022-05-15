using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Basketbool.Web.Data.Entities
{
    public class MatchEntity
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime DateLocal => Date.ToLocalTime();

        public TeamEntity Local { get; set; }

        public TeamEntity Visitor { get; set; }

        [Display(Name = "Points Local")]
        public int? PointsLocal { get; set; }

        [Display(Name = "Points Visitor")]
        public int? PointsVisitor { get; set; }

        [Display(Name = "Is Closed?")]
        public bool IsClosed { get; set; }

        public MatchDayEntity MatchDay { get; set; }

        public ICollection<PredictionEntity> Predictions { get; set; }

    }
}

