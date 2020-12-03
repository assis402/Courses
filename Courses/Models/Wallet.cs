using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class Wallet
    {
        public string WalletId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public double Balance { get; set; }
    }
}
