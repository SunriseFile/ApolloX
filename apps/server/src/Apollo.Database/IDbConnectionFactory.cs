using System.Data;
using System.Threading.Tasks;

namespace Apollo.Database
{
    internal interface IDbConnectionFactory
    {
        Task<IDbConnection> OpenAsync();
    }
}
