using Basketbool.Web.Data;
using Basketbool.Web.Data.Entities;
using Basketbool.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Basketbool.Web.Controllers
{
    public class TeamsController : Controller
    {
        private readonly DataContext _context;

        public TeamsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(TeamViewModel teamViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string path = string.Empty;

        //        if (teamViewModel.LogoFile != null)
        //        {
        //            path = await _imageHelper.UploadImageAsync(teamViewModel.LogoFile, "Teams");
        //        }
        //        TeamEntity teamEntity = _converterHelper.ToTeamEntity(teamViewModel, path, true);
        //        _context.Add(teamEntity);
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch (Exception ex)
        //        {
        //            if (ex.InnerException.Message.Contains("duplicate"))
        //            {
        //                ModelState.AddModelError(string.Empty, $"Already exist the team: {teamEntity.Name}.");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError(string.Empty, ex.InnerException.Message);
        //            }

        //        }
        //    }
        //    return View(teamViewModel);
        //}
    }
}
