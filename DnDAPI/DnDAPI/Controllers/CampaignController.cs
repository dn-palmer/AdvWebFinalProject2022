using DnDAPI.Models.Entities;
using DnDAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DnDAPI.Controllers;

[EnableCors]
[Route("api/[controller]")]
[ApiController]
public class CampaignController : Controller
{
    private readonly IDnDRepository _repos;
    public CampaignController(IDnDRepository repository)
    {
        _repos = repository;
    }

    [HttpGet("ReadAll")]
    public async Task<IActionResult> ReadAll()
    {
        var playerList = await _repos.ReadAllDMAsync();
        return Ok(playerList);
    }

    [HttpGet("Read/{id}")]
    public async Task<IActionResult> Read(int id)
    {
        var player = await _repos.ReadPlayerAsync(id);
        return Ok(player);

    }


    [HttpPost("Create")]
    public async Task<IActionResult> CreateCampaign(int dungeonMasterId, int playerId, string campaignName, string campaignDesc, GameEdition gameEdition)
    {
        var dungeonMaster = await _repos.ReadDMAsync(dungeonMasterId);
        if(dungeonMaster == null)
        {
            return NotFound("Dungeon Master Id Invalid");
        }
        
        var player = await _repos.ReadPlayerAsync(playerId);
        if (dungeonMaster == null)
        {
            return NotFound("Player Id Invalid");
        }

        var campaign = new Campaign
        {
            CampaignName = campaignName,
            CampaignDescription = campaignDesc,
            GameEdition = gameEdition,
            Player = player,
            DungeonMaster = dungeonMaster
        };

        await _repos.CreateCampaignAsync(campaign);

        return Ok();
    }

   
    [HttpPut("Update")]
    public async Task<IActionResult> Update(Campaign campaign)
    {
        await _repos.UpdateCampaignAsync(campaign);
        return NoContent();
    }


    [HttpDelete("DeletePlayer/{id}")]
    public async Task<IActionResult> DeletePlayer(int playerId, int campainId)
    {
        await _repos.RemoveCampaignFromPlayerAsync(playerId, campainId);
        return NoContent();
    }

    [HttpDelete("DeleteCampagin/{id}")]
    public async Task<IActionResult> DeleteCampagin(int dungeonMasterId, int campainId)
    {
        await _repos.RemoveCampaignFromPlayerAsync(dungeonMasterId, campainId);
        return NoContent();
    }


}
