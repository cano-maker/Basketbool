using Basketbool.Web.Data;
using Basketbool.Web.Data.Entities;
using Basketbool.Web.Interfaces;
using Basketbool.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Basketbool.Web.Controllers
{
    public class TeamsController : Controller
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly IBlobHelper _blobHelper;

        public TeamsController(DataContext context,
            IConverterHelper converterHelper,
            IBlobHelper blobHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
            _blobHelper = blobHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TeamEntity team = await _converterHelper.ToProductAsync(model, true);

                    if (model.LogoFile != null)
                    {
                        Guid imageId = await _blobHelper.UploadBlobAsync(model.LogoFile, "teams");
                        team.LogoId = imageId;
                    }

                    _context.Add(team);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            //model.Categories = _combosHelper.GetComboCategories();
            return View(model);
        }
    }
}
