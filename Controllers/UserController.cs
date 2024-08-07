using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace library_management_1.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly LibraryFacade _libraryFacade;

        public UserController(LibraryFacade libraryFacade)
        {
            _libraryFacade = libraryFacade;
        }

        public IActionResult Index(string searchId)
        {
            var users = _libraryFacade.GetAllUsers();

            if (!string.IsNullOrEmpty(searchId) && int.TryParse(searchId, out int id))
            {
                users = users.Where(u => u.Id == id).ToList();
            }

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            var users = _libraryFacade.GetAllUsers();
            user.Id = GenerateNewUserId(users);
            var newUser = _libraryFacade.CreateUser(user.Id, user.FullName, user.Email, user.Username, user.Password);
            users.Add(newUser);
            _libraryFacade.SaveUsers(users);
            return RedirectToAction("Index");
        }

        private int GenerateNewUserId(List<User> users)
        {
            int maxId = users.Count > 0 ? users.Max(u => u.Id) : 0;
            return maxId + 1;
        }

        public IActionResult Edit(int id)
        {
            var user = _libraryFacade.GetAllUsers().FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            var users = _libraryFacade.GetAllUsers();
            var existingUser = users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.FullName = user.FullName;
                existingUser.Email = user.Email;
                existingUser.Username = user.Username;
                existingUser.Password = user.Password;
                _libraryFacade.SaveUsers(users);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var users = _libraryFacade.GetAllUsers();
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                users.Remove(user);
                _libraryFacade.SaveUsers(users);
            }
            return RedirectToAction("Index");
        }
    }
}