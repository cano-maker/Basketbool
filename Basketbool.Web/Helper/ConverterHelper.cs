using Basketbool.Web.Data;
using Basketbool.Web.Data.Entities;
using Basketbool.Web.Interfaces;
using Basketbool.Web.Models;
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

        public async Task<TeamEntity> ToProductAsync(TeamViewModel model, bool isNew)
        {
            return new TeamEntity
            {
                //Category = await _context.Categories.FindAsync(model.CategoryId),
                //Description = model.Description,
                Id = isNew ? 0 : model.Id,
                //IsActive = model.IsActive,
                //IsStarred = model.IsStarred,
                Name = model.Name
                //Price = model.Price,
                //ProductImages = model.ProductImages
            };
        }

        //public ProductViewModel ToProductViewModel(Product product)
        //{
        //    return new ProductViewModel
        //    {
        //        Categories = _combosHelper.GetComboCategories(),
        //        Category = product.Category,
        //        CategoryId = product.Category.Id,
        //        Description = product.Description,
        //        Id = product.Id,
        //        IsActive = product.IsActive,
        //        IsStarred = product.IsStarred,
        //        Name = product.Name,
        //        Price = product.Price,
        //        ProductImages = product.ProductImages
        //    };
        //}

    }

}
