using DnDShopWebsite.Models.Entities;
using DnDShopWebsite.Services;
using Microsoft.AspNetCore.Mvc;

namespace DnDShopWebsite.Controllers
{
    //feeds data to the player views.
    public class PlayerController : Controller
    {
        private readonly IDnDRepository _repo;

        public PlayerController(IDnDRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var players = await _repo.ReadAllPlayersAsync();
            return View(players);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Player player)
        {
            if (ModelState.IsValid)
            {
                var newPlayer = await _repo.CreatePlayerAsync(player);
                return RedirectToAction("Index", "Player");
            }
            return View(player);
        }


        public async Task<IActionResult> Details(int id)
        {
            var player = await _repo.ReadPlayerAsync(id);

            if (player == null)
            {
                return RedirectToAction("Index", "Player");
            }

            return View(player);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var player = await _repo.ReadPlayerAsync(id);
            if (player == null)
            {
                return RedirectToAction("Index");
            }
            return View(player);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Player player)
        {
            if (ModelState.IsValid)
            {
                await _repo.UpdatePlayerAsync(player);
                return RedirectToAction("Details", "Player", new { Id = player.Id });
            }
            return View(player);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var player = await _repo.ReadPlayerAsync(id);

            return View(player);

        }

        public async Task<IActionResult> DeletePlayer(int id)
        {
            await _repo.DeletePlayerAsync(id);

            return RedirectToAction("Index", "Player");

        }
    }
}
