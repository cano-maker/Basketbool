using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Basketbool.Web.Data.Entities
{
    public class DivisionEntity
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<TeamEntity> Teams { get; set; }

        [DisplayName("Teams Number")]
        public int TeamsNumber => Teams == null ? 0 : Teams.Count;

        [JsonIgnore]
        public ConferenceEntity Conference { get; set; }

    }
}
