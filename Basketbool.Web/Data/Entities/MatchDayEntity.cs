using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Basketbool.Web.Data.Entities
{
    public class MatchDayEntity
    {
        public int Id { get; set; }

        [MaxLength(30, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public SeasonEntity Season { get; set; }

        public ICollection<MatchDayDetailEntity> MatchDayDetails { get; set; }

        public ICollection<MatchEntity> Matches { get; set; }
        
    }
}
