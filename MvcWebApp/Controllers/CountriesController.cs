using Microsoft.AspNetCore.Mvc;
using MvcWebApp.Entities;
using MvcWebApp.Interfaces;
using MvcWebApp.Models.ViewModels;
using MvcWebApp.Repositories;
using MvcWebApp.BlobAzure;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Azure;

namespace MvcWebApp.Controllers
{
    public class CountriesController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICountryRepository _countryRepository;
        public CountriesController(ICountryRepository countryRepository, IUserRepository userRepository)
        {
            _countryRepository = countryRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _countryRepository.GetAllAsync();
            return View(list);
        }
        public ActionResult Details(int id)
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Photo")] AddCountry addCountry, IFormFile Photo)
        {
            if (ModelState.IsValid && Photo != null)
            {
                addCountry.Photo = await BlobAzure.BlobAzure.UploadImage(Photo);

                var country = new Country()
                {
                    Name = addCountry.Name,
                    imageUrl = addCountry.Photo
                };

                await _countryRepository.AddAsync(country);
                return RedirectToAction(nameof(Index));
            }
            return View(addCountry);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _userRepository.GetByCountryIdAsync(id);
            var country = await _countryRepository.DeleteAsync(id);
            BlobAzure.BlobAzure.DeletePhoto(country.imageUrl);           

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var country = await _countryRepository.GetByIdAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            var editCountry = new EditCountry()
            {
                Id = country.Id,
                Name = country.Name,
                Photo = country.imageUrl
            };

            return View(editCountry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Photo")] EditCountry editCountry, IFormFile Photo)
        {
            if (id != editCountry.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid && Photo != null)
            {
                var imageToDelete = (await _countryRepository.GetByIdAsync(id)).imageUrl;
                BlobAzure.BlobAzure.DeletePhoto(imageToDelete);

                editCountry.Photo = await BlobAzure.BlobAzure.UploadImage(Photo);

                var country = new Country()
                {
                    Name = editCountry.Name,
                    imageUrl = editCountry.Photo
                };

                var response = await _countryRepository.UpdateAsync(id, country);

                if (response == null) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            return View(editCountry);
        }
    }
}

