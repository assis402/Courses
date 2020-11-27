using Courses.AccessData.Repositories;
using Courses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.AccessData.Interfaces
{
    public class AccessLevelsRepository : GenericRepository<AccessLevels>, IAccessLevels
    {
        public AccessLevelsRepository(Context context) : base(context)
        {
        }


    }
}
