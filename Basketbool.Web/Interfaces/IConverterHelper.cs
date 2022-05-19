using Basketbool.Web.Data.Entities;
using Basketbool.Web.Models;
using System;
using System.Threading.Tasks;

namespace Basketbool.Web.Interfaces
{
    public interface IConverterHelper
    {
        Task<TeamEntity> ToTeamAsync(TeamViewModel model, bool isNew);

        TeamViewModel ToTeamViewModel(TeamEntity entity);

        TeamEntity ToTeamEntity(TeamViewModel model, Guid id, bool isNew);

        Task<MatchDayEntity> ToMatchDayEntityAsync(MatchDayViewModel model, bool isNew);

        MatchDayViewModel ToMatchDayViewModel(MatchDayEntity entity);

        Task<MatchDayDetailEntity>  ToMatchDayDetailEntityAsync(MatchDayDetailViewModel model, bool isNew);

        Task<MatchEntity> ToMatchEntityAsync(MatchViewModel model, bool isNew);

        MatchDayDetailViewModel ToMatchDayDetailViewModel(MatchDayDetailEntity entity);

        MatchViewModel ToMatchViewModel(MatchEntity entity);

    }

}
