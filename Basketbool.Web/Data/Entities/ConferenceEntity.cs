using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Basketbool.Web.Data.Entities
{
    public class ConferenceEntity
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<DivisionEntity> Divisions { get; set; }

        [DisplayName("Division Number")]
        public int DivisionNumbers => Divisions == null ? 0 : Divisions.Count;
    }
}
