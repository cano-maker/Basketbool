using System.ComponentModel.DataAnnotations;

namespace Basketbool.Web.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }


}
