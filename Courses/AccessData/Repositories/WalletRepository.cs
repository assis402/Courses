using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.AccessData.Interfaces;
using Courses.Models;

namespace Courses.AccessData.Repositories
{
    public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
    {

        private readonly Context _context;

        public WalletRepository(Context context) : base(context)
        {
            _context = context;
        }

        public double GetBalanceById(string Id)
        {
            return _context.Wallets.FirstOrDefault(c => c.WalletId == Id).Balance;
        }

        public new async Task<IEnumerable<Wallet>> GetAll()
        {
            return await _context.Wallets.Include(c => c.User).ToListAsync();
        }

        public async Task<Wallet> GetWalletByUserId(string userId)
        {
            return await _context.Wallets.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public double GetBalanceByUserId(string id)
        {
            return _context.Wallets.FirstOrDefault(c => c.UserId == id).Balance;
        }

    }
}
