using Basketbool.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace Basketbool.Web.Controllers
{
    public class PredictionsController : Controller
    {
        private readonly DataContext _context;

        public PredictionsController(DataContext context)
        {
            _context = context;
        }



    }
}
