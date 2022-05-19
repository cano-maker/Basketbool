using Microsoft.AspNetCore.Mvc.Rendering;
using Basketbool.Web.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Basketbool.Web.Models
{
    public class MatchDayDetailViewModel : MatchDayDetailEntity
    {
        public int MatchDayId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Team")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a team.")]
        public int TeamId { get; set; }

        public IEnumerable<SelectListItem> Teams { get; set; }

    }
}
