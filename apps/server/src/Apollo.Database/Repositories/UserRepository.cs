using System.Threading.Tasks;

using Apollo.Core.Models;
using Apollo.Database.Query;

using Dapper;

namespace Apollo.Database.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _factory;
        private readonly AppUserQueryObject _queryObject;

        public UserRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
            _queryObject = new AppUserQueryObject();
        }

        public async Task<int> InsertAsync(AppUser user)
        {
            var sql = _queryObject.Insert(user);

            using (var connection = await _factory.OpenAsync())
            {
                return await connection.QuerySingleAsync<int>(sql);
            }
        }

        public async Task UpdateAsync(AppUser user)
        {
            var sql = _queryObject.Update(user);

            using (var connection = await _factory.OpenAsync())
            {
                await connection.ExecuteAsync(sql);
            }
        }

        public async Task DeleteAsync(AppUser user)
        {
            var sql = _queryObject.Delete(user);

            using (var connection = await _factory.OpenAsync())
            {
                await connection.ExecuteAsync(sql);
            }
        }

        public async Task<AppUser> GetByIdAsync(int id)
        {
            var sql = _queryObject.ById(id);

            using (var connection = await _factory.OpenAsync())
            {
                return await connection.QuerySingleOrDefaultAsync<AppUser>(sql);
            }
        }

        public async Task<AppUser> GetByUserNameAsync(string username)
        {
            var sql = _queryObject.ByUserName(username);

            using (var connection = await _factory.OpenAsync())
            {
                return await connection.QuerySingleOrDefaultAsync<AppUser>(sql);
            }
        }
    }
}
