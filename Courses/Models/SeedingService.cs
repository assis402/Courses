using Courses.AccessData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class SeedingService
    {
        private readonly IAccessLevelsRepository _accessLevelsRepository;
        private readonly Context _context;

        public SeedingService(IAccessLevelsRepository accessLevelsRepository, Context context)
        {
            _context = context;
            _accessLevelsRepository = accessLevelsRepository;
        }

        public void Seed()
        {
            if (_context.AccessLevels.Any())
            {
                return;
            }

            AccessLevels levelAluno = new AccessLevels("Aluno", "Possui acesso ao portal de cursos, podendo estar matriculado em apenas um curso", "ALUNO");
            AccessLevels levelProfessor = new AccessLevels("Professor", " ", "PROFESSOR");

            _context.AccessLevels.AddRange(levelAluno, levelProfessor);

            _context.SaveChanges();

            //_accessLevelsRepository.Insert(levelAluno);
            //_accessLevelsRepository.Insert(levelProfessor);
        } 
    }
}
