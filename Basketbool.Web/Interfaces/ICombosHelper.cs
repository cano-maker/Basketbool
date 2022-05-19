using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Basketbool.Web.Interfaces
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboTeams();

        IEnumerable<SelectListItem> GetComboTeams(int id);

    }
}
