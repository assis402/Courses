using Courses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.AccessData.Interfaces
{
    public interface IMatriculationRepository : IGenericRepository<Matriculation>
    {
        Task<bool> AlreadyMatriculate(string userId, string courseId);
    }
}
