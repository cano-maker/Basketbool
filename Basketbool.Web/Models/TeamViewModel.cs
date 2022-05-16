using Basketbool.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Basketbool.Web.Models
{
    public class TeamViewModel : TeamEntity
    {

        [Display(Name = "Logo")]
        public IFormFile LogoFile { get; set; }

    }
}
