using DominicanPingPong.Data;
using DominicanPingPong.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DominicanPingPong.Controllers
{
    public class PlayersController : Controller
    {
        private readonly DominicanDbContext DominicanDbContext;

        public PlayersController(DominicanDbContext DominicanDbContext)
        {
            this.DominicanDbContext = DominicanDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var players = await DominicanDbContext.Players.ToListAsync();
            return View(players);
        }

        public IActionResult AddPlayer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer(PlayerViewModel playerViewModel)
        {
            var player = new PlayerModel()
            {
                FirstName = playerViewModel.FirstName,
                LastName = playerViewModel.LastName,
                Position = playerViewModel.Position,
                HandPosition = playerViewModel.HandPosition,
                Wins = playerViewModel.Wins
            };

            await DominicanDbContext.Players.AddAsync(player);
            await DominicanDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var player = await DominicanDbContext.Players.FirstOrDefaultAsync(x => x.Id == id);
            return View(player);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var player = await DominicanDbContext.Players.FirstOrDefaultAsync(x => x.Id == id);

            return View(player);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PlayerModel playerModel)
        {
            var player = await DominicanDbContext.Players.FirstOrDefaultAsync(x => x.Id == playerModel.Id);
            if (player != null)
            {
                player.FirstName = playerModel.FirstName;
                player.LastName = playerModel.LastName;
                player.Position = playerModel.Position;
                player.HandPosition = playerModel.HandPosition;
                player.Wins = playerModel.Wins;
                await DominicanDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Index");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var player = await DominicanDbContext.Players.FirstOrDefaultAsync(x => x.Id == id);
            return View(player);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PlayerModel employeeModel)
        {
            var player = await DominicanDbContext.Players.FirstOrDefaultAsync(x => x.Id == employeeModel.Id);
            if (player != null)
            {
                DominicanDbContext.Players.Remove(player);
                await DominicanDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

    }
}

