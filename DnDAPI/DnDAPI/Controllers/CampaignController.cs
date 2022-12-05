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
        var campaignList = await _repos.ReadAllCampaignsAsync();
        return Ok(campaignList);
    }

    [HttpGet("Read/{id}")]
    public async Task<IActionResult> Read(int id)
    {
        var campaign = await _repos.ReadCampaignAsync(id);
        return Ok(campaign);

    }


    [HttpPost("Create")]
    public async Task<IActionResult> CreateCampaign([FromForm] Campaign campaign)
    {



        var dungeonMaster = await _repos.ReadDMAsync(campaign.DungeonMasterId);
        if (dungeonMaster == null)
        {
            return NotFound("Dungeon Master Id Invalid");
        }

        var player = await _repos.ReadPlayerAsync(campaign.PlayerId);
        if (dungeonMaster == null)
        {
            return NotFound("Player Id Invalid");
        }
        var c = await _repos.CreateCampaignAsync(campaign);

        return Ok(c);
    }


    //public async Task<IActionResult> CreateCampaign([FromForm] int gameEdition, [FromForm] string campaignName, [FromForm] string campaignDescription, [FromForm]  int dungeonMasterId, [FromForm] int playerId)
    //{



    //    var dungeonMaster = await _repos.ReadDMAsync(dungeonMasterId);
    //    if (dungeonMaster == null)
    //    {
    //        return NotFound("Dungeon Master Id Invalid");
    //    }

    //    var player = await _repos.ReadPlayerAsync(playerId);
    //    if (dungeonMaster == null)
    //    {
    //        return NotFound("Player Id Invalid");
    //    }

    //    var campaign = new Campaign
    //    {
    //        CampaignDescription = campaignDescription,
    //        CampaignName = campaignName,
    //        GameEdition = (GameEdition)gameEdition,
    //        DungeonMasterId = dungeonMasterId,
    //        PlayerId = playerId
    //    };


    //    await _repos.CreateCampaignAsync(campaign);

    //    return CreatedAtAction("Get", new { id = campaign.Id }, campaign);
    //}


    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromForm]Campaign campaign)
    {
        await _repos.UpdateCampaignAsync(campaign);
        return NoContent();
    }


    [HttpDelete("DeletePlayer/{playerid}/{campainId}")]
    public async Task<IActionResult> DeletePlayer(int playerId, int campainId)
    {
        await _repos.RemoveCampaignFromPlayerAsync(playerId, campainId);
        return NoContent();
    }

    [HttpDelete("DeleteCampagin/{dungeonMasterid}/{campainId}")]
    public async Task<IActionResult> DeleteCampagin(int dungeonMasterId, int campainId)
    {
        await _repos.RemoveCampaignFromDungeonMasterAsync(dungeonMasterId, campainId);
        return NoContent();
    }


}
