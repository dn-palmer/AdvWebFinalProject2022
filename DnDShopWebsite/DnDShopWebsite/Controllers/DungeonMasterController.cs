using DnDShopWebsite.Models.Entities;
using DnDShopWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using System.Drawing;

namespace DnDShopWebsite.Controllers
{
    //Controller for the dungeon master aspects of the site it manages creating/deleted/updating all DM's. 
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

        public  IActionResult Create()        
        {

            return View();

        }


        [HttpPost]
        public async Task<IActionResult> Create(DungeonMaster dungeonMaster)
        {
            if (ModelState.IsValid)
            {
                 var newDm = await _repo.CreateDungeonMasterAsync(dungeonMaster);
                return RedirectToAction("Details", "DungeonMaster", new  { Id = newDm.Id });
            }
            return View(dungeonMaster);
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

        public async Task<IActionResult> Edit(int id) 
        {
            var dungeonMaster = await _repo.ReadDMAsync(id);
            if (dungeonMaster == null)
            {
                return RedirectToAction("Index");
            }
            return View(dungeonMaster);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DungeonMaster dungeonMaster)
        {
            if (ModelState.IsValid)
            {
                await _repo.UpdateGMAsync(dungeonMaster);
                return RedirectToAction("Details", "DungeonMaster", new {Id = dungeonMaster.Id});
            }
            return View(dungeonMaster);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dungeonMaster = await _repo.ReadDMAsync(id);

            return View(dungeonMaster);

        }

        public async Task<IActionResult> DeleteDM(int id)
        {
             await _repo.DeleteGMAsync(id);

            return RedirectToAction("Index", "DungeonMaster");

        }
    }
}
