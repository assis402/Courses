using Courses.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courses.AccessData.Interfaces
{
    public interface IWalletRepository : IGenericRepository<Wallet>
    {
        new Task<IEnumerable<Wallet>> GetAll();
        Task<Wallet> GetWalletByUserId(string Id);
        double GetBalanceById(string Id);
        double GetBalanceByUserId(string id);
    }
}
