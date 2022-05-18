using Basketbool.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Basketbool.Web.Controllers
{
    public class SeasonsController : Controller
    {
        private readonly DataContext _context;

        public SeasonsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context
                .Seasons
                .Include(t => t.MatchDays)
                .OrderBy(t => t.StartDate)
                .ToListAsync());
        }

    }
}
