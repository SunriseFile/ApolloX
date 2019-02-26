using System.Threading.Tasks;

using Apollo.Core.Models;

namespace Apollo.Database.Repositories
{
    public interface IUserRepository
    {
        Task<int> InsertAsync(AppUser user);
        Task UpdateAsync(AppUser user);
        Task DeleteAsync(AppUser user);
        Task<AppUser> GetByIdAsync(int id);
        Task<AppUser> GetByUserNameAsync(string username);
    }
}
