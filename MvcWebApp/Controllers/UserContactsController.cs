using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MvcWebApp.Entities;
using MvcWebApp.Interfaces;
using MvcWebApp.Models.ViewModels;
using MvcWebApp.Repositories;

namespace MvcWebApp.Controllers
{
    public class UserContactsController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IFriendRepository _friendRepository;

        public UserContactsController(IUserRepository userRepository, IMapper mapper, IFriendRepository friendRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _friendRepository = friendRepository;
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

            List<ContactViewModel> listaContactsViewModel = new List<ContactViewModel>();

            var list = await _friendRepository.GetAllByUserIdAsync(id);

            foreach (var item in list)
            {
                var contact = await _userRepository.GetByIdAsync(item.FriendId);
                ContactViewModel contactViewModel = new ContactViewModel()
                {
                    Id = contact.Id,
                    Name = contact.FirstName + " " + contact.LastName,
                    PhoneNumber = contact.PhoneNumber,
                    Email = contact.Email,
                };

                listaContactsViewModel.Add(contactViewModel);
            }

            detailsUser.userContacts = listaContactsViewModel;

            return View(detailsUser);
        }


        public async Task<IActionResult> Create(int id)
        {
            UserViewModel userViewModel = await GetUserViewModel(id);

            if (userViewModel == null)
            {
                return NotFound();
            }

            List<ContactViewModel> contactsToAdd = await GetContactsToAddViewModel(id);

            AddContact addContactViewModel = new AddContact()
            {
                UserId = id,
                User = userViewModel,
                userContacts = contactsToAdd
            };


            return View(addContactViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int UserId, int FriendId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.GetByIdAsync(UserId);

                FriendFriends userContact = new FriendFriends(user.Id, FriendId);

                await _friendRepository.AddAsync(userContact);
                return RedirectToAction("Details", new { id = UserId });

            }
            return View();
        }

        #region
        private async Task<UserViewModel> GetUserViewModel(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }

        public async Task<IActionResult> Delete(int UserId, int FriendId)
        {
            FriendFriends userContact = new FriendFriends(UserId, FriendId);
            await _friendRepository.DeleteAsync(userContact);
            return RedirectToAction("Details", new { id = UserId });
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            await _friendRepository.GetAllLinkedUsers(id);

            await _userRepository.DeleteAsync(id);


            return RedirectToAction("Index", "Users");
        }

            private async Task<List<ContactViewModel>> GetContactsToAddViewModel(int userId)
        {
            var userContact = await _friendRepository.GetAllByUserIdAsync(userId);
            var userContacts = userContact.ToList();

            var alUsers = await _userRepository.GetAllAsync();
            var allUsers = alUsers.ToList();

            List<ContactViewModel> contacts = new List<ContactViewModel>();

            List<int> contactsId = new List<int>();
            foreach (var contact in userContacts)
            {
                contactsId.Add(contact.FriendId);
            }
            foreach (var user in allUsers)
            {
                if (user.Id != userId && !contactsId.Contains(user.Id))
                {
                    ContactViewModel contactViewModel = new ContactViewModel()
                    {
                        Id = user.Id,
                        Name = user.FirstName + " " + user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                    };
                    contacts.Add(contactViewModel);
                }

            }

            return contacts;
        }
        #endregion


    }
}
