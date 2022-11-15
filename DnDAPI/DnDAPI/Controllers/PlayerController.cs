using DnDAPI.Models.Entities;
using DnDAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DnDAPI.Controllers;

//Api controller for the Player table. Allows interaction with the data gather from the database and for users to update or delete info. 
[EnableCors]
[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly IDnDRepository _repos;
    public PlayerController(IDnDRepository repository)
    {
        _repos = repository;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreatePlayer([FromForm] Player player)
    {
       await _repos.CreatePlayerAsync(player);
       return CreatedAtAction("Get", new { id = player.Id }, player);
    }


    [HttpGet("ReadAll")]
    public async Task<IActionResult> ReadAll()
    {
        var playerList = await _repos.ReadAllPlayersAsync();
        return Ok(playerList);
    }

    [HttpGet("Read/{id}")]
    public async Task<IActionResult> Read(int id)
    {
        var player = await _repos.ReadPlayerAsync(id);
        return Ok(player);

    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromForm] Player palyer)
    {
        await _repos.UpdatePlayerAsync(palyer);
        return NoContent();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repos.DeletePlayerAsync(id);
        return NoContent();
    }


}
