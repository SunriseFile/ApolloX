using Apollo.Core.Models;
using Apollo.Identity.Models;

namespace Apollo.Identity.Services
{
    public interface IJsonWebTokenGenerator
    {
        JsonWebToken Generate(AppUser user);
    }
}
