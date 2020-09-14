using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreApp.Models;
using Microsoft.AspNetCore.Http;

namespace CoreApp.Controllers
{
    public class MainController : Controller
    {
        private readonly UserDbContext _context;

        public MainController(UserDbContext context)
        {
            _context = context;
        }
        
        // GET: Main/Index
        public IActionResult Index()
        {
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Clear();
            return View();
        }

        // POST: Main/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Id,Name,PhoneNumber")] User user)
        {
            if (ModelState.IsValid)
            {
                var userData = _context.User.Where(u => u.PhoneNumber == user.PhoneNumber).ToList();
                if (userData.Count > 0)
                {
                    ModelState.AddModelError("", "Data already exists.");
                    return View(user);
                }
                _context.Add(user);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("userId", user.Name);
                return RedirectToAction("Index","Home");
            }
            return View(user);
        }

        // POST: Main/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var userData = _context.User.Where(u => u.Name == user.Name && u.PhoneNumber == user.PhoneNumber).ToList();
                if (userData.Count > 0)
                {
                    HttpContext.Session.SetString("userId", user.Name);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login.");
                    return View(user);
                }
            }
            return View(user);
        }

        // GET: Main/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Main");
        }
    }
}
