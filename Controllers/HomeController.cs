using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Utility;

namespace MagicVilla_Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IVillaService _villaService;

    public HomeController(ILogger<HomeController> logger, IVillaService villaService)
    {
        _logger = logger;
        _villaService = villaService;
    }

    public async Task<IActionResult> Index()
    {
        var apiResponse = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetails.SessionToken));

        if (apiResponse != null && apiResponse.IsSuccess)
        {
            var result = JsonConvert.DeserializeObject<List<VillaReadDTO>>(apiResponse.Result.ToString());

            return View(result);
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
