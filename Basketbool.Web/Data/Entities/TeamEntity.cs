using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Basketbool.Web.Data.Entities
{
    public class TeamEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public Guid LogoId { get; set; }

        //TODO: Pending to put the correct paths
        [Display(Name = "Image")]
        public string LogoFullPath => LogoId == Guid.Empty
        ? $"https://localhost:44390/images/noimage.png"
        : $"https://Basketbool.Web.blob.core.windows.net/products/{LogoId}";


        public ICollection<UserEntity> Users { get; set; }
    }
}
