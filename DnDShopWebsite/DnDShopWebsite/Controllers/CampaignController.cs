using DnDShopWebsite.Models.Entities;
using DnDShopWebsite.Models.ViewModels;
using DnDShopWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DnDShopWebsite.Controllers
{
    //Controller for the campaign aspects of the site it manages creating/deleted/updating/Viewing all campaigns. 
    public class CampaignController : Controller
    {
        private readonly IDnDRepository _repo;

        public CampaignController(IDnDRepository repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {

            var campaigns = await _repo.ReadAllCampaignsAsync();

            var players = await _repo.ReadAllPlayersAsync();

            var dungeonMasters = await _repo.ReadAllDMAsync();

            var distinctCampaignNames = campaigns.Select(a => a.CampaignName).Distinct().ToList();

            var campaignPartial = new CampaignCardPartialVM
            {
                AllCampaigns = campaigns,
                Players = players,
                DungeonMasters = dungeonMasters,
                CurrentCampaignName = ""
            };

            var campaignCreationModal = new CreateCampaignModalVM
            {
                DungeonMasters = dungeonMasters,
                Players = players
            };

            var model = new CampaignIndexVM
            {
                DungeonMasters = dungeonMasters,
                Players = players,
                UniqeCampaignNames = distinctCampaignNames,
                AllCampaigns = campaigns,
                CampaignCreationModal = campaignCreationModal,
                CampaignPartialVM = campaignPartial

            };



            return View(model);
        }



        public async Task<IActionResult> Create(int id)
        {
            var dungeonMaster = await _repo.ReadDMAsync(id);


            if (dungeonMaster == null)
            {
                return RedirectToAction("Index", "DungeonMaster");
            }


            var playerList = await _repo.ReadAllPlayersAsync();

            var model = new CreateACampaignVM
            {
                DungeonMasterID = id,
                PlayerId = 0,
                Players = playerList,
                DungeonMaster = dungeonMaster,
                GameEdition = 0
            };

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] int playerId, [FromForm] int dungeonMasterId, [FromForm] string campaignDescription, [FromForm] string campaignName, [FromForm] GameEdition gameEdition)
        {
            var player = await _repo.ReadPlayerAsync(playerId);

            if (player == null)
            {
                return RedirectToAction("Index", "Player");
            }

            var dungeonMaster = await _repo.ReadDMAsync(dungeonMasterId);

            if (dungeonMaster == null)
            {
                return RedirectToAction("Index", "DungeonMaster");
            }

            var newCampaign = new Campaign
            {
                CampaignName = campaignName,
                CampaignDescription = campaignDescription,
                GameEdition = gameEdition,
                DungeonMasterId = dungeonMaster.Id,
                PlayerId = player.Id,
                DungeonMaster = dungeonMaster,
                Player = player
            };


            await _repo.CreateCampaignAsync(newCampaign);

            return RedirectToAction("Details", "DungeonMaster", new { id = dungeonMasterId });
        }


        //Creates the campaign from the modal in the campains index.


        [HttpPost]
        public async Task<IActionResult> CreateModal([FromForm] int playerId, [FromForm] int dungeonMasterId, [FromForm] string campaignDescription, [FromForm] string campaignName, [FromForm] GameEdition gameEdition)
        {
            var player = await _repo.ReadPlayerAsync(playerId);

            if (player == null)
            {
                return RedirectToAction("Index", "Player");
            }

            var dungeonMaster = await _repo.ReadDMAsync(dungeonMasterId);

            if (dungeonMaster == null)
            {
                return RedirectToAction("Index", "DungeonMaster");
            }

            var newCampaign = new Campaign
            {
                CampaignName = campaignName,
                CampaignDescription = campaignDescription,
                GameEdition = gameEdition,
                DungeonMasterId = dungeonMaster.Id,
                PlayerId = player.Id,
                DungeonMaster = dungeonMaster,
                Player = player
            };


            var c = await _repo.CreateCampaignAsync(newCampaign);

            return Json(new { campaignId = c.Id, message = "success" });
        }

        //Adds a card to the View after the creation with Ajax
        public async Task<IActionResult> AddCard(int id)
        {
            var campaign = await _repo.ReadCampaignAsync(id);

            var model = new CampaignCardPartialVM
            {
                CurrentCampaignName = campaign.CampaignName,
                AllCampaigns = await _repo.ReadAllCampaignsAsync(),
                DungeonMasters = await _repo.ReadAllDMAsync(),
                Players = await _repo.ReadAllPlayersAsync(),
            };

            return PartialView("/Views/Campaign/_CampaignCardsPartial.cshtml", model);
        }



        public async Task<IActionResult> AddPlayer(int dungeonMasterId, int campaignId)
        {
            var dungeonMaster = await _repo.ReadDMAsync(dungeonMasterId);


            if (dungeonMaster == null)
            {
                return RedirectToAction("Index", "DungeonMaster");
            }

            var campaign = await _repo.ReadCampaignAsync(campaignId);


            if (campaign == null)
            {
                return RedirectToAction("Index", "DungeonMaster");
            }

            var playerList = await _repo.ReadAllPlayersAsync();

            var allCampagins = await _repo.ReadAllCampaignsAsync();
            var currentPlayersCampaigns = dungeonMaster.Campaigns.Where(c => c.CampaignName == campaign.CampaignName).ToList();
            var nonPlayersCampaigns = allCampagins.Except(currentPlayersCampaigns).ToList();

            var nonPlayers = new List<Player>();

            var currentPlayers = new List<Player>();



            foreach (var player in playerList)
            {
                if (!dungeonMaster.Campaigns.Any(x=> x.PlayerId == player.Id))
                {
                    nonPlayers.Add(player);
                }

            }

            foreach (var player in currentPlayersCampaigns)
            {
                currentPlayers.Add(await _repo.ReadPlayerAsync(player.PlayerId));
            }


            var model = new AddAPlayerToCampaginVM
            {
                DungeonMasterID = dungeonMasterId,
                PlayerId = 0,
                NonPlayers = nonPlayers,
                CurrentPlayers = currentPlayers,
                DungeonMaster = dungeonMaster,
                GameEdition = 0,
                CampaignDescription = campaign.CampaignDescription,
                CampaignName = campaign.CampaignName
            };

            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromForm] int playerId, [FromForm] int dungeonMasterId, [FromForm] string campaignDescription, [FromForm] string campaignName, [FromForm] GameEdition gameEdition)
        {
            var player = await _repo.ReadPlayerAsync(playerId);

            if (player == null)
            {
                return RedirectToAction("Index", "Player");
            }

            var dungeonMaster = await _repo.ReadDMAsync(dungeonMasterId);

            if (dungeonMaster == null)
            {
                return RedirectToAction("Index", "DungeonMaster");
            }

            var newCampaign = new Campaign
            {
                CampaignName = campaignName,
                CampaignDescription = campaignDescription,
                GameEdition = gameEdition,
                DungeonMasterId = dungeonMaster.Id,
                PlayerId = player.Id,
                DungeonMaster = dungeonMaster,
                Player = player
            };


            await _repo.CreateCampaignAsync(newCampaign);

            return RedirectToAction("Details", "DungeonMaster", new { id = dungeonMasterId });
        }

        public async Task<IActionResult> DeletePlayer(int playerId, int campaignId)
        {
            var campaign = await _repo.ReadCampaignAsync(campaignId);

            if (campaign == null)
            {
                return RedirectToAction("Index", "Campaign");
            }

            var player = await _repo.ReadPlayerAsync(playerId);

            if (player == null)
            {
                return RedirectToAction("Index", "Player");
            }

            var model = new RemovePlayerVM
            {
                PlayerId = playerId,
                PlayerFullName = $"{player.FirstName} {player.LastName}",
                CampaignId = campaignId,
                CampaignName = campaign.CampaignName
            };


            return View(model);
        }


        public async Task<IActionResult> DeletePlayerPost(int playerId, int campaignId)
        {
            var campaign = await _repo.ReadCampaignAsync(campaignId);

            if (campaign == null)
            {
                return RedirectToAction("Index", "Campaign");
            }

            var player = await _repo.ReadPlayerAsync(playerId);

            if (player == null)
            {
                return RedirectToAction("Index", "Player");
            }

            await _repo.RemoveCampaignFromPlayerAsync(playerId, campaignId);

            return RedirectToAction("Details", "Player", new { Id = playerId });

        }

        public async Task<IActionResult> DeleteCampaign(int dungeonMasterId, int campaignId)
        {
            var campaign = await _repo.ReadCampaignAsync(campaignId);

            if (campaign == null)
            {
                return RedirectToAction("Index", "Campaign");
            }

            var dungeonMaster = await _repo.ReadDMAsync(dungeonMasterId);

            if (dungeonMaster == null)
            {
                return RedirectToAction("Index", "Player");
            }


            var allCampagins = await _repo.ReadAllCampaignsAsync();
            var currentPlayersCampaigns = allCampagins.Where(c => c.CampaignName == campaign.CampaignName).ToList();
            var currentPlayers = new List<Player>();

            foreach (var player in currentPlayersCampaigns)
            {
                currentPlayers.Add(await _repo.ReadPlayerAsync(player.PlayerId));
            }

            var model = new DeleteCampaignVM
            {
                DungeonMaster = dungeonMaster,
                DungeonMasterId = dungeonMasterId,
                Campaign = campaign,
                CampaignId = campaignId,
                AllPlayersInCampaign = currentPlayers
            };


            return View(model);
        }

        public async Task<IActionResult> DeleteCampaignPost(int dungeonMasterId, int campaignId)
        {
            var campaign = await _repo.ReadCampaignAsync(campaignId);

            if (campaign == null)
            {
                return RedirectToAction("Index", "Campaign");
            }

            var player = await _repo.ReadDMAsync(dungeonMasterId);

            if (player == null)
            {
                return RedirectToAction("Index", "Player");
            }

            await _repo.RemoveCampaignFromDungeonMasterAsync(dungeonMasterId, campaignId);


            return RedirectToAction("Details", "DungeonMaster", new { Id = dungeonMasterId });


        }


        
        public async Task<IActionResult> Edit(int id)
        {
            var campaign = await _repo.ReadCampaignAsync(id);

            if (campaign == null)
            {
                return RedirectToAction("Index", "Campaign");
            }

            var allCampagins = await _repo.ReadAllCampaignsAsync();
            var currentPlayersCampaigns = allCampagins.Where(c => c.CampaignName == campaign.CampaignName).ToList();
            var currentPlayers = new List<Player>();

            foreach (var player in currentPlayersCampaigns)
            {
                currentPlayers.Add(await _repo.ReadPlayerAsync(player.PlayerId));
            }

            var model = new EditCampaignVM
            {
                DungeonMaster = campaign.DungeonMaster,
                CampaignDescription = campaign.CampaignDescription,
                CampaignName = campaign.CampaignName,
                CurrentPlayers = currentPlayers,
                GameEdition = campaign.GameEdition,
                CampaignId = campaign.Id
            };

            return View(model);


        }


        //For some reason this deletes campaiggns instead of updating them. The process works entirly and I can even see it updating in the api but it erases the
        //entry none the less.
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] int campaignId, [FromForm] string campaignDescription, [FromForm] string campaignName, [FromForm] GameEdition gameEdition)
        {
            var orginalCampaign = await _repo.ReadCampaignAsync(campaignId);

            if (orginalCampaign == null)
            {
                return RedirectToAction("Index", "Campaign");
            }

            var dungeonMaster = await _repo.ReadDMAsync(orginalCampaign.DungeonMasterId);


            if (dungeonMaster == null)
            {
                return RedirectToAction("Index", "DungeonMaster");
            }


            var allCampagins = await _repo.ReadAllCampaignsAsync();
            var currentPlayers = allCampagins.Where(c => c.CampaignName == orginalCampaign.CampaignName && c.DungeonMasterId == orginalCampaign.DungeonMasterId).ToList();


            foreach (var campaignToUpdate in currentPlayers)
            {
                campaignToUpdate.CampaignDescription = campaignDescription;
                campaignToUpdate.CampaignName = campaignName;
                campaignToUpdate.GameEdition = gameEdition;
                await _repo.UpdateCampaignAsync(campaignToUpdate);
            }

            return RedirectToAction("Index", "Campaign");
        }

    }
}
