using System;
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

        public double PickBalanceById(string Id)
        {
            return _context.Wallets.FirstOrDefault(c => c.UserId == Id).Balance;
        }

        public new async Task<IEnumerable<Wallet>> PickAll()
        {
            return await _context.Wallets.Include(c => c.User).ToListAsync();
        }

        public async Task<Wallet> PickBalanceByUserId(string id)
        {
            return await _context.Wallets.FirstOrDefaultAsync(c => c.UserId == id);
        }

    }
}
