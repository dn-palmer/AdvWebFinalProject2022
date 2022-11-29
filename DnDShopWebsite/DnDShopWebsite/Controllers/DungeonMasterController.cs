using DnDShopWebsite.Services;
using Microsoft.AspNetCore.Mvc;

namespace DnDShopWebsite.Controllers
{
    public class DungeonMasterController : Controller
    {
        private readonly IDnDRepository _repo;

        public DungeonMasterController(IDnDRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var dungeonMasters = await _repo.ReadAllDMAsync();
            return View(dungeonMasters);
        }

        public async Task<IActionResult> Details(int id)
        {
            var dungeonMaster = await _repo.ReadDMAsync(id);

            if(dungeonMaster == null)
            {
                return RedirectToAction("Index", "DungeonMaster");
            }

            return View(dungeonMaster);
        }
    }
}
