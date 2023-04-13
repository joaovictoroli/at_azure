using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MvcWebApp.Entities;
using MvcWebApp.Interfaces;
using MvcWebApp.Models.ViewModels;
using MvcWebApp.Repositories;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace MvcWebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _userRepository.GetAllAsync();
            var listVM = list.Select(_mapper.Map<Friend, UserViewModel>);

            foreach (var item in listVM)
            {
                if (item.CountryId != null)
                {
                    var country = await _countryRepository.GetByIdAsync(item.CountryId.Value);
                    item.CountryImageUrl = country.imageUrl;
                } else
                {
                    item.CountryImageUrl = null;
                }
            }

            return View(listVM);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = _mapper.Map<UserViewModel>(user);

            DetailsUser detailsUser = new DetailsUser()
            {
                User = userViewModel
            };

            var country = await _countryRepository.GetByIdAsync(userViewModel.CountryId.Value);
            userViewModel.CountryImageUrl = country.imageUrl;

            Console.WriteLine(country.imageUrl);
            return View(detailsUser);
        }

        public async Task<IActionResult> Create()
        {
            AddUser viewmodel = new AddUser();
            viewmodel.Countries = await _countryRepository.GetAllAsync();
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUser addfriend)
        {
            if (ModelState.IsValid)
            {
                Friend user = new Friend()
                {
                    FirstName = addfriend.FirstName,
                    LastName = addfriend.LastName,
                    Email = addfriend.Email,
                    PhoneNumber = addfriend.PhoneNumber,
                    BirthDate = addfriend.BirthDate,
                    CountryId = addfriend.CountryId,
                };
                await _userRepository.AddAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(addfriend);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            UserViewModel userViewModel = new UserViewModel()
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
                CountryId = user.CountryId,
            };

            EditUser editUser = new EditUser()
            {
                User = userViewModel,
            };
            var countries = await _countryRepository.GetAllAsync();

            editUser.Countries = countries.OrderBy(c => c.Id == editUser.User.CountryId).ThenBy(c => c.Name).ToList();
            return View(editUser);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditUser editFriend)
        {
            if (ModelState.IsValid)
            {
                Friend user = new Friend()
                {
                    Id = editFriend.User.Id,
                    FirstName = editFriend.User.FirstName,
                    LastName = editFriend.User.LastName,
                    Email = editFriend.User.Email,
                    PhoneNumber = editFriend.User.PhoneNumber,
                    BirthDate = editFriend.User.BirthDate,
                    CountryId = editFriend.User.CountryId,
                };
                await _userRepository.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


    }
}

