using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexVilla()
        {
            var villaReadDTOList = new List<VillaReadDTO>();

            var response = await _villaService.GetAllAsync<APIResponse>();

            if (response != null && response.IsSuccess)
            {
                villaReadDTOList = JsonConvert.DeserializeObject<List<VillaReadDTO>>(response.Result.ToString());
            }

            return View(villaReadDTOList);
        }

        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDTO villaCreateDTO)
        {
            if (ModelState.IsValid) // The ModelState.IsValid checks consistency against our defined model VillaCreateDTO.cs. Such us ensuring the keys are populated, max length are followed and so on.
            {
                var response = await _villaService.CreateAsync<APIResponse>(villaCreateDTO);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            }

            return View(villaCreateDTO);
        }

        //public async Task<ActionResult<VillaReadDTO>> UpdateVilla(VillaUpdateDTO villaUpdateDTO)
        //{

        //}
    }
}
