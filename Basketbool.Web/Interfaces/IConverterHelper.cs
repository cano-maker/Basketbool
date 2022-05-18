using Basketbool.Web.Data.Entities;
using Basketbool.Web.Models;
using System;
using System.Threading.Tasks;

namespace Basketbool.Web.Interfaces
{
    public interface IConverterHelper
    {
        //Category ToCategory(CategoryViewModel model, Guid imageId, bool isNew);

        //CategoryViewModel ToCategoryViewModel(Category category);

        Task<TeamEntity> ToTeamAsync(TeamViewModel model, bool isNew);

        TeamViewModel ToTeamViewModel(TeamEntity entity);

        TeamEntity ToTeamEntity(TeamViewModel model, Guid id, bool isNew);

        Task<MatchDayEntity> ToMatchDayEntityAsync(MatchDayViewModel model, bool isNew);

        MatchDayViewModel ToMatchDayViewModel(MatchDayEntity entity);

    }

}
