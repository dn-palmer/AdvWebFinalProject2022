using DnDAPI.Models.Entities;
using DnDAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DnDAPI.Controllers;

//Controler for the Dungeon Master Table. Allows interaction with the data held inside.
[EnableCors]
[Route("api/[controller]")]
[ApiController]
public class DungeonMasterController : ControllerBase
{
    private readonly IDnDRepository _repos;
    public DungeonMasterController(IDnDRepository repository)
    {
        _repos = repository;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateDM([FromForm] DungeonMaster dungeonMaster)
    {
        await _repos.CreateDungeonMasterAsync(dungeonMaster);
        return CreatedAtAction("Get", new {id = dungeonMaster.Id}, dungeonMaster);
    }


    [HttpGet("ReadAll")]
    public async Task<IActionResult> ReadAll()
    {
        var dmList = await _repos.ReadAllDMAsync();
        return Ok(dmList);
    }

    [HttpGet("Read/{id}")]
    public async Task<IActionResult> Read(int id)
    {
        var dungeonMaster = await _repos.ReadDMAsync(id);

        if(dungeonMaster == null)
        {
            return NotFound();
        }

        return Ok(dungeonMaster);

    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromForm] DungeonMaster dungeonMaster)
    {
        await _repos.UpdateGMAsync(dungeonMaster);
        return NoContent();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repos.DeleteGMAsync(id);
        return NoContent();
    }


}
