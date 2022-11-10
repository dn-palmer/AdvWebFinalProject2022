using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DnDAPI.Controllers;

[EnableCors]
[Route("api/[controller]")]
[ApiController]
public class CampaignController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
