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
                    TempData["success"] = "Villa created successfully";
                    return RedirectToAction(nameof(IndexVilla));
                }
                else
                {
                    if (!response.IsSuccess || response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            TempData["error"] = "Villa could not be created";
            return View(villaCreateDTO);
        }

        public async Task<IActionResult> UpdateVilla(int villaId)
        {
            var response = await _villaService.GetAsync<APIResponse>(villaId);

            if (response != null && response.IsSuccess)
            {
                VillaReadDTO villaReadDTO = JsonConvert.DeserializeObject<VillaReadDTO>(response.Result.ToString());

                return View(_mapper.Map<VillaUpdateDTO>(villaReadDTO));
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDTO villaUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.UpdateAsync<APIResponse>(villaUpdateDTO);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa updated successfully";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }

            TempData["error"] = "Villa could not be updated";
            return View(villaUpdateDTO);
        }

        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            var response = await _villaService.GetAsync<APIResponse>(villaId);

            if (response != null && response.IsSuccess)
            {
                return View(JsonConvert.DeserializeObject<VillaReadDTO>(response.Result.ToString()));
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaReadDTO villaReadDTO)
        {
            var response = await _villaService.DeleteAsync<APIResponse>(villaReadDTO.Id);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa deleted successfully";
            }
            else
            {
                TempData["error"] = "Villa could not be deleted";
            }

            return RedirectToAction(nameof(IndexVilla));
        }
    }
}
