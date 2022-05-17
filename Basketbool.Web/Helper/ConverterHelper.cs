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
        //private readonly ICombosHelper _combosHelper;
        public ConverterHelper(DataContext context)
        //ICombosHelper combosHelper)
        {
            _context = context;
            //_combosHelper = combosHelper;
        }

        //public Category ToCategory(CategoryViewModel model, Guid imageId, bool isNew)
        //{
        //    return new Category
        //    {
        //        Id = isNew ? 0 : model.Id,
        //        ImageId = imageId,
        //        Name = model.Name
        //    };
        //}

        //public CategoryViewModel ToCategoryViewModel(Category category)
        //{
        //    return new CategoryViewModel
        //    {
        //        Id = category.Id,
        //        ImageId = category.ImageId,
        //        Name = category.Name
        //    };
        //}

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

    }

}
