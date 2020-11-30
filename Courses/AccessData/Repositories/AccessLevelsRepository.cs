using Courses.AccessData.Interfaces;
using Courses.AccessData.Repositories;
using Courses.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.AccessData.Repositories
{
    public class AccessLevelsRepository : GenericRepository<AccessLevels>, IAccessLevelsRepository
    {
        private readonly RoleManager<AccessLevels> _accessLevelsManager;
        private readonly Context _context;

        public AccessLevelsRepository(Context context, RoleManager<AccessLevels> accessLevelsManager) : base(context)
        {
            _context = context;
            _accessLevelsManager = accessLevelsManager;

        }

        public async Task<bool> AccessLevelExists(string accessLevel)
        {
            return await _accessLevelsManager.RoleExistsAsync(accessLevel);
        }

        public new async Task Update(AccessLevels accessLevels)
        {
            var nv = await _context.AccessLevels.FindAsync(accessLevels.Id);
            nv.Name = accessLevels.Name;
            nv.NormalizedName = accessLevels.NormalizedName;
            nv.Description = accessLevels.Description;
            await _accessLevelsManager.UpdateAsync(nv);
        }
    }
}
