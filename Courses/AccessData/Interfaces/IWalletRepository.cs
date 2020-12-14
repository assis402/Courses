using Courses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.AccessData.Interfaces
{
    public interface IWalletRepository : IGenericRepository<Wallet>
    {
        new Task<IEnumerable<Wallet>> PickAll();
        double PickBalanceById(string Id);
        Task<Wallet> PickBalanceByUserId(string id);
    }
}
