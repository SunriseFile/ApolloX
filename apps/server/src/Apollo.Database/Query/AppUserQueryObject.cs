using Apollo.Core;
using Apollo.Core.Models;

namespace Apollo.Database.Query
{
    internal class AppUserQueryObject
    {
        public string Insert(AppUser user)
        {
            return new SqlKata.Query(TableNames.Users)
                   .AsInsert(user, true)
                   .CompileQuery();
        }

        public string Update(AppUser user)
        {
            return new SqlKata.Query(TableNames.Users)
                   .AsUpdate(user)
                   .Where(nameof(AppUser.Id), "=", user.Id)
                   .CompileQuery();
        }

        public string Delete(AppUser user)
        {
            return new SqlKata.Query(TableNames.Users)
                   .AsDelete()
                   .Where(nameof(AppUser.Id), "=", user.Id)
                   .CompileQuery();
        }

        public string ById(int id)
        {
            return new SqlKata.Query(TableNames.Users)
                   .Where(nameof(AppUser.Id), "=", id)
                   .CompileQuery();
        }

        public string ByUserName(string username)
        {
            return new SqlKata.Query(TableNames.Users)
                   .Where(nameof(AppUser.UserName), "=", username)
                   .CompileQuery();
        }
    }
}
