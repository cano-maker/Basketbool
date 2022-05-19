using Basketbool.Web.Data;
using Basketbool.Web.Data.Entities;
using Basketbool.Web.Interfaces;
using Basketbool.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Basketbool.Web.Controllers
{
    public class SeasonsController : Controller
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;

        public SeasonsController(DataContext context, 
            IConverterHelper converterHelper,
            ICombosHelper combosHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
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

        public async Task<IActionResult> AddMatchDay(int? id)
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

            MatchDayViewModel model = new MatchDayViewModel
            {
                Season = seasonEntity,
                SeasonId = seasonEntity.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMatchDay(MatchDayViewModel model)
        {
            if (ModelState.IsValid)
            {
                MatchDayEntity matchDayEntity = await _converterHelper.ToMatchDayEntityAsync(model, true);
                _context.Add(matchDayEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}", new { id = model.SeasonId });
            }

            return View(model);
        }

        public async Task<IActionResult> EditMatchDay(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MatchDayEntity matchDayEntity = await _context.MatchDays
                .Include(g => g.Season)
                .FirstOrDefaultAsync(g => g.Id == id);
            if (matchDayEntity == null)
            {
                return NotFound();
            }

            MatchDayViewModel model = _converterHelper.ToMatchDayViewModel(matchDayEntity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMatchDay(MatchDayViewModel model)
        {
            if (ModelState.IsValid)
            {
                MatchDayEntity matchDayEntity = await _converterHelper.ToMatchDayEntityAsync(model, false);
                _context.Update(matchDayEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}", new { id = model.SeasonId });
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteMatchDay(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MatchDayEntity matchDayEntity = await _context.MatchDays
                .Include(g => g.Season)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchDayEntity == null)
            {
                return NotFound();
            }

            _context.MatchDays.Remove(matchDayEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(Details)}", new { id = matchDayEntity.Season.Id });
        }

        public async Task<IActionResult> DetailsMatchDay(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MatchDayEntity matchDayEntity = await _context.MatchDays
                .Include(g => g.Matches)
                .ThenInclude(g => g.Local)
                .Include(g => g.Matches)
                .ThenInclude(g => g.Visitor)
                .Include(g => g.Season)
                .Include(g => g.MatchDayDetails)
                .ThenInclude(gd => gd.Team)
                .FirstOrDefaultAsync(g => g.Id == id);
            if (matchDayEntity == null)
            {
                return NotFound();
            }

            return View(matchDayEntity);
        }

        public async Task<IActionResult> AddMatchDayDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MatchDayEntity matchDay = await _context.MatchDays.FindAsync(id);
            if (matchDay == null)
            {
                return NotFound();
            }

            MatchDayDetailViewModel model = new MatchDayDetailViewModel
            {
                MatchDay = matchDay,
                MatchDayId = matchDay.Id,
                Teams = _combosHelper.GetComboTeams()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMatchDayDetail(MatchDayDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                MatchDayDetailEntity matchDayDetailEntity = await _converterHelper.ToMatchDayDetailEntityAsync(model, true);
                _context.Add(matchDayDetailEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(DetailsMatchDay)}", new { id = model.MatchDayId });
            }

            model.MatchDay = await _context.MatchDays.FindAsync(model.MatchDayId);
            model.Teams = _combosHelper.GetComboTeams();
            return View(model);
        }

        public async Task<IActionResult> AddMatch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MatchDayEntity matchDayEntity = await _context.MatchDays.FindAsync(id);
            if (matchDayEntity == null)
            {
                return NotFound();
            }

            MatchViewModel model = new MatchViewModel
            {
                Date = DateTime.Today,
                MatchDay = matchDayEntity,
                MatchDayId = matchDayEntity.Id,
                Teams = _combosHelper.GetComboTeams(matchDayEntity.Id)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMatch(MatchViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.LocalId != model.VisitorId)
                {
                    MatchEntity matchEntity = await _converterHelper.ToMatchEntityAsync(model, true);
                    _context.Add(matchEntity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(DetailsMatchDay)}", new { id = model.MatchDayId });
                }

                ModelState.AddModelError(string.Empty, "The local and visitor must be differents teams.");
            }

            model.MatchDay = await _context.MatchDays.FindAsync(model.MatchDayId);
            model.Teams = _combosHelper.GetComboTeams(model.MatchDayId);
            return View(model);
        }
        
        public async Task<IActionResult> EditMatchDayDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MatchDayDetailEntity matchDayDetailEntity = await _context.MatchDayDetails
                .Include(gd => gd.MatchDay)
                .Include(gd => gd.Team)
                .FirstOrDefaultAsync(gd => gd.Id == id);
            if (matchDayDetailEntity == null)
            {
                return NotFound();
            }

            MatchDayDetailViewModel model = _converterHelper.ToMatchDayDetailViewModel(matchDayDetailEntity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMatchDayDetails(MatchDayDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                MatchDayDetailEntity matchDayDetailEntity = await _converterHelper.ToMatchDayDetailEntityAsync(model, false);
                _context.Update(matchDayDetailEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(DetailsMatchDay)}", new { id = model.MatchDayId });
            }

            return View(model);
        }

        public async Task<IActionResult> EditMatch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MatchEntity matchEntity = await _context.Matches
                .Include(m => m.MatchDay)
                .Include(m => m.Local)
                .Include(m => m.Visitor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchEntity == null)
            {
                return NotFound();
            }

            MatchViewModel model = _converterHelper.ToMatchViewModel(matchEntity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMatch(MatchViewModel model)
        {
            if (ModelState.IsValid)
            {
                MatchEntity matchEntity = await _converterHelper.ToMatchEntityAsync(model, false);
                _context.Update(matchEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(DetailsMatchDay)}", new { id = model.MatchDayId });
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteMatchDayDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MatchDayDetailEntity matchDayDetailEntity = await _context.MatchDayDetails
                .Include(gd => gd.MatchDay)
                .FirstOrDefaultAsync(gd => gd.Id == id);
            if (matchDayDetailEntity == null)
            {
                return NotFound();
            }

            _context.MatchDayDetails.Remove(matchDayDetailEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(DetailsMatchDay)}", new { id = matchDayDetailEntity.MatchDay.Id });
        }

        public async Task<IActionResult> DeleteMatch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MatchEntity matchEntity = await _context.Matches
                .Include(m => m.MatchDay)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchEntity == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(matchEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(DetailsMatchDay)}", new { id = matchEntity.MatchDay.Id });
        }


        public async Task<IActionResult> CloseMatch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchEntity = await _context.Matches
                .Include(m => m.MatchDay)
                .Include(m => m.Local)
                .Include(m => m.Visitor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchEntity == null)
            {
                return NotFound();
            }

            var model = new CloseMatchViewModel
            {
                MatchDay = matchEntity.MatchDay,
                MatchDayId = matchEntity.MatchDay.Id,
                Local = matchEntity.Local,
                LocalId = matchEntity.Local.Id,
                MatchId = matchEntity.Id,
                Visitor = matchEntity.Visitor,
                VisitorId = matchEntity.Visitor.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseMatch(CloseMatchViewModel model)
        {
            if (ModelState.IsValid)
            {
                MatchEntity matchEntity = await _context.Matches
                .Include(m => m.Local)
                .Include(m => m.Visitor)
                .Include(m => m.Predictions)
                .Include(m => m.MatchDay)
                .ThenInclude(g => g.MatchDayDetails)
                .ThenInclude(gd => gd.Team)
                .FirstOrDefaultAsync(m => m.Id == model.MatchId);

                matchEntity.PointsLocal = model.PointsLocal;
                matchEntity.PointsVisitor = model.PointsVisitor;
                matchEntity.IsClosed = true;

                await _context.SaveChangesAsync();

                return RedirectToAction($"{nameof(DetailsMatchDay)}", new { id = model.MatchDayId });

            }

            model.MatchDay = await _context.MatchDays.FindAsync(model.MatchDayId);
            model.Local = await _context.Teams.FindAsync(model.LocalId);
            model.Visitor = await _context.Teams.FindAsync(model.VisitorId);
            return View(model);
        }

    }
}
