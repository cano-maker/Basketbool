using Basketbool.Web.Data;
using Basketbool.Web.Data.Entities;
using Basketbool.Web.Interfaces;
using Basketbool.Web.Models;
using System;
using System.Threading.Tasks;

namespace Basketbool.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {


        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        public ConverterHelper(DataContext context,
        ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        public async Task<TeamEntity> ToTeamAsync(TeamViewModel model, bool isNew)
        {
            return new TeamEntity
            {
                Id = isNew ? 0 : model.Id,
                Name = model.Name,
                LogoId = model.LogoId,
            };
        }

        public TeamEntity ToTeamEntity(TeamViewModel model, Guid id, bool isNew)
        {
            return new TeamEntity
            {
                Id = isNew ? 0 : model.Id,
                LogoId = id,
                Name = model.Name
            };
        }

        public TeamViewModel ToTeamViewModel(TeamEntity team)
        {
            return new TeamViewModel
            {
                Id = team.Id,
                Name = team.Name,
                LogoId = team.LogoId
            };
        }


        public async Task<MatchDayEntity> ToMatchDayEntityAsync(MatchDayViewModel model, bool isNew)
        {
            return new MatchDayEntity
            {
                MatchDayDetails = model.MatchDayDetails,
                Id = isNew ? 0 : model.Id,
                Matches = model.Matches,
                Name = model.Name,
                Season = await _context.Seasons.FindAsync(model.SeasonId)
            };
        }

        public MatchDayViewModel ToMatchDayViewModel(MatchDayEntity entity)
        {
            return new MatchDayViewModel
            {
                MatchDayDetails = entity.MatchDayDetails,
                Id = entity.Id,
                Matches = entity.Matches,
                Name = entity.Name,
                Season = entity.Season,
                SeasonId = entity.Season.Id
            };
        }

        public async Task<MatchDayDetailEntity> ToMatchDayDetailEntityAsync(MatchDayDetailViewModel model, bool isNew)
        {
            return new MatchDayDetailEntity
            {
                PointsAgainst = model.PointsAgainst,
                PointsFor = model.PointsFor,
                MatchDay = await _context.MatchDays.FindAsync(model.MatchDayId),
                Id = isNew ? 0 : model.Id,
                MatchesLost = model.MatchesLost,
                MatchesPlayed = model.MatchesPlayed,
                MatchesTied = model.MatchesTied,
                MatchesWon = model.MatchesWon,
                Team = await _context.Teams.FindAsync(model.TeamId)
            };
        }

        public async Task<MatchEntity> ToMatchEntityAsync(MatchViewModel model, bool isNew)
        {
            return new MatchEntity
            {
                Date = model.Date.ToUniversalTime(),
                PointsLocal = model.PointsLocal,
                PointsVisitor = model.PointsVisitor,
                MatchDay = await _context.MatchDays.FindAsync(model.MatchDayId),
                Id = isNew ? 0 : model.Id,
                IsClosed = model.IsClosed,
                Local = await _context.Teams.FindAsync(model.LocalId),
                Visitor = await _context.Teams.FindAsync(model.VisitorId)
            };
        }

        public MatchDayDetailViewModel ToMatchDayDetailViewModel(MatchDayDetailEntity matchDayDetailEntity)
        {
            return new MatchDayDetailViewModel
            {
                PointsAgainst = matchDayDetailEntity.PointsAgainst,
                PointsFor = matchDayDetailEntity.PointsFor,
                MatchDay = matchDayDetailEntity.MatchDay,
                MatchDayId = matchDayDetailEntity.MatchDay.Id,
                Id = matchDayDetailEntity.Id,
                MatchesLost = matchDayDetailEntity.MatchesLost,
                MatchesPlayed = matchDayDetailEntity.MatchesPlayed,
                MatchesTied = matchDayDetailEntity.MatchesTied,
                MatchesWon = matchDayDetailEntity.MatchesWon,
                Team = matchDayDetailEntity.Team,
                TeamId = matchDayDetailEntity.Team.Id,
                Teams = _combosHelper.GetComboTeams()
            };
        }

        public MatchViewModel ToMatchViewModel(MatchEntity matchEntity)
        {
            return new MatchViewModel
            {
                Date = matchEntity.Date.ToLocalTime(),
                PointsLocal = matchEntity.PointsLocal,
                PointsVisitor = matchEntity.PointsVisitor,
                MatchDay = matchEntity.MatchDay,
                MatchDayId = matchEntity.MatchDay.Id,
                Id = matchEntity.Id,
                IsClosed = matchEntity.IsClosed,
                Local = matchEntity.Local,
                LocalId = matchEntity.Local.Id,
                Teams = _combosHelper.GetComboTeams(matchEntity.MatchDay.Id),
                Visitor = matchEntity.Visitor,
                VisitorId = matchEntity.Visitor.Id
            };
        }
    }

}
