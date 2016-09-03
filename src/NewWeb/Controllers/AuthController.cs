using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewWeb.Models;
using NewWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWeb.Controllers
{
    public class AuthController : Controller
    {
        private IDoctorRepository _doctorRepository;
        private SignInManager<Doctor> _signInManager;
        private UserManager<Doctor> _userManager;

        public AuthController(SignInManager<Doctor> signInManager, 
            IDoctorRepository doctorRepository, 
            UserManager<Doctor> userManager)
        {
            _signInManager = signInManager;
            _doctorRepository = doctorRepository;
            _userManager = userManager;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Patients", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username,
                                                                      vm.Password,
                                                                      true, false);
                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Patients", "App");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect");
                }
            }

            return View();
        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "App");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("Auth/Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDoctorViewModel theDoctor)
        {
            if (ModelState.IsValid)
            {
                var password = theDoctor.Password;
                var newDoctor = Mapper.Map<Doctor>(theDoctor);

                var result = await _userManager.CreateAsync(newDoctor, password);
                

                return RedirectToAction("Index", "App");

            }
            return BadRequest("Failed to register");
            

        }

    }
}
