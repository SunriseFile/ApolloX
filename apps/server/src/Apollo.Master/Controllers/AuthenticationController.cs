using System.Threading.Tasks;

using Apollo.Core.Models;
using Apollo.Identity.Services;
using Apollo.Master.Controllers.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Apollo.Master.Controllers
{
    [Route("auth")]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJsonWebTokenGenerator _tokenGenerator;

        public AuthenticationController(UserManager<AppUser> userManager,
                                        SignInManager<AppUser> signInManager,
                                        IJsonWebTokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AuthLoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return BadRequest(new ErrorResponseModel("Invalid username or password"));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
            {
                return BadRequest(new ErrorResponseModel("Invalid username or password"));
            }

            var token = _tokenGenerator.Generate(user);

            return Ok(token);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AuthRegisterModel model)
        {
            var user = new AppUser
            {
                DisplayUserName = model.UserName,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ErrorResponseModel(result));
            }

            var token = _tokenGenerator.Generate(user);

            return Ok(token);
        }
    }
}
