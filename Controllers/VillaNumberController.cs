using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MagicVilla_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villaNumberService;
        private readonly IMapper _mapper;
        private readonly IVillaService _villaService;

        public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper, IVillaService villaService)
        {
            _villaNumberService = villaNumberService;
            _mapper = mapper;
            _villaService = villaService;
        }

        public async Task<IActionResult> IndexVillaNumber()
        {
            var apiResponse = await _villaNumberService.GetAllAsync<APIResponse>();

            var villaNumberList = JsonConvert.DeserializeObject<List<VillaNumberReadDTO>>(apiResponse.Result.ToString());

            return View(villaNumberList);
        }

        public async Task<IActionResult> CreateVillaNumber()
        {
            var apiResponse = await _villaService.GetAllAsync<APIResponse>();

            var villaNameDropdown = new List<SelectListItem>();

            if (apiResponse != null && apiResponse.IsSuccess)
            {
                var villaList = JsonConvert.DeserializeObject<List<VillaReadDTO>>(apiResponse.Result.ToString());

                if (villaList != null)
                {
                    foreach (var villa in villaList)
                    {
                        villaNameDropdown.Add(new SelectListItem
                            {
                                Text = villa.Name,
                                Value = villa.Id.ToString(),
                            });
                    }
                }
            }

            ViewData["VillaNameDropdown"] = villaNameDropdown;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateDTO villaNumber)
        {
            APIResponse apiResponse = new();

            if (ModelState.IsValid)
            {
                apiResponse = await _villaNumberService.CreateAsync<APIResponse>(villaNumber);

                if (apiResponse != null && apiResponse.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
            }

            var villaNameDropdown = new List<SelectListItem>();

            apiResponse = await _villaService.GetAllAsync<APIResponse>();

            if (apiResponse != null && apiResponse.IsSuccess)
            {
                var villaList = JsonConvert.DeserializeObject<List<VillaReadDTO>>(apiResponse.Result.ToString());

                if (villaList != null)
                {
                    villaNameDropdown = new List<SelectListItem>();

                    foreach (var villa in villaList)
                    {
                        villaNameDropdown.Add(new SelectListItem
                            {
                                Text = villa.Name,
                                Value = villa.Id.ToString(),
                            });
                    }
                }
            }

            ViewData["VillaNameDropdown"] = villaNameDropdown;

            return View(villaNumber);
        }

        public async Task<IActionResult> UpdateVillaNumber(int villaNo)
        {
            var villaNameDropdown = new List<SelectListItem>();

            var apiResponse = await _villaNumberService.GetAsync<APIResponse>(villaNo);

            if (apiResponse != null && apiResponse.IsSuccess)
            {
                var villaNumber = JsonConvert.DeserializeObject<VillaNumberUpdateDTO>(apiResponse.Result.ToString());

                apiResponse = await _villaService.GetAllAsync<APIResponse>();

                if (apiResponse != null && apiResponse.IsSuccess)
                {
                    var villaList = JsonConvert.DeserializeObject<List<VillaReadDTO>>(apiResponse.Result.ToString());

                    if (villaList != null)
                    {
                        foreach (var villa in villaList)
                        {
                            villaNameDropdown.Add(new SelectListItem
                                {
                                    Text = villa.Name,
                                    Value = villa.Id.ToString(),
                                });
                        }
                    }
                }

                ViewData["VillaNameDropdown"] = villaNameDropdown;

                return View(villaNumber);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateDTO villaNumber)
        {
            APIResponse apiResponse = new();

            if (ModelState.IsValid)
            {
                apiResponse = await _villaNumberService.UpdateAsync<APIResponse>(villaNumber);

                if (apiResponse != null && apiResponse.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
            }

            var villaNameDropdown = new List<SelectListItem>();

            apiResponse = await _villaService.GetAllAsync<APIResponse>();

            if (apiResponse != null && apiResponse.IsSuccess)
            {
                var villaList = JsonConvert.DeserializeObject<List<VillaReadDTO>>(apiResponse.Result.ToString());

                if (villaList != null)
                {
                    villaNameDropdown = new List<SelectListItem>();

                    foreach (var villa in villaList)
                    {
                        villaNameDropdown.Add(new SelectListItem
                            {
                                Text = villa.Name,
                                Value = villa.Id.ToString(),
                            });
                    }
                }
            }

            ViewData["VillaNameDropdown"] = villaNameDropdown;

            return View(villaNumber);
        }

        public async Task<IActionResult> DeleteVillaNumber(int villaNo)
        {
            var apiResponse = await _villaNumberService.GetAsync<APIResponse>(villaNo);

            if(apiResponse != null && apiResponse.IsSuccess)
            {
                var villaNumber = JsonConvert.DeserializeObject<VillaNumberReadDTO>(apiResponse.Result.ToString());
                
                return View(villaNumber);
            }

            return RedirectToAction(nameof(IndexVillaNumber));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberReadDTO villaNumberReadDTO)
        {
            await _villaNumberService.DeleteAsync<APIResponse>(villaNumberReadDTO.VillaNo);

            return RedirectToAction(nameof(IndexVillaNumber));
        }
    }
}
