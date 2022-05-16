using Basketbool.Web.Data.Entities;
using Basketbool.Web.Models;
using System.Threading.Tasks;

namespace Basketbool.Web.Interfaces
{
    public interface IConverterHelper
    {
        //Category ToCategory(CategoryViewModel model, Guid imageId, bool isNew);

        //CategoryViewModel ToCategoryViewModel(Category category);

        Task<TeamEntity> ToProductAsync(TeamViewModel model, bool isNew);

        // ProductViewModel ToProductViewModel(Product product);

    }

}
