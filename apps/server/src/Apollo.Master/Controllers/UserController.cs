using System.Threading.Tasks;

using Apollo.Core.Models;
using Apollo.Master.DTO;

using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Apollo.Master.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(Request.HttpContext.User.Identity.Name);
            var userDTO = _mapper.Map<AppUserDTO>(user);

            return Ok(userDTO);
        }
    }
}
