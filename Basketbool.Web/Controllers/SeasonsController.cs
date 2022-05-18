using Basketbool.Web.Data;
using Basketbool.Web.Data.Entities;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SeasonEntity model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SeasonEntity seasonEntity = await _context.Seasons.FindAsync(id);
            if (seasonEntity == null)
            {
                return NotFound();
            }

            return View(seasonEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SeasonEntity model)
        {
            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SeasonEntity seasonEntity = await _context.Seasons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seasonEntity == null)
            {
                return NotFound();
            }

            _context.Seasons.Remove(seasonEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SeasonEntity seasonEntity = await _context.Seasons
                .Include(t => t.MatchDays)
                .ThenInclude(t => t.Matches)
                .ThenInclude(t => t.Local)
                .Include(t => t.MatchDays)
                .ThenInclude(t => t.Matches)
                .ThenInclude(t => t.Visitor)
                .Include(t => t.MatchDays)
                .ThenInclude(t => t.MatchDayDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seasonEntity == null)
            {
                return NotFound();
            }

            return View(seasonEntity);
        }

    }
}
