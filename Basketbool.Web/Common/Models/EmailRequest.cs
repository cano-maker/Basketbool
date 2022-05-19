using System.ComponentModel.DataAnnotations;

namespace Basketbool.Web.Common.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }
}
