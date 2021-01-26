using Courses.AccessData.Interfaces;
using System;
using System.Linq;

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
            if (_context.AccessLevels.Any() && _context.Courses.Any() && _context.Users.Any())
            {
                return;
            }

            User primaryUser = new User
                (
                    "0e6ba977-4c54-4c41-a9c2-fa9599e1db50",
                    "assis402",
                    "ASSIS402",
                    "AQAAAAEAACcQAAAAEFthlsY8BuKY537xrIM3bNl2BpOykHrYvQTbASk9c/oWady5+IGa8HpuKpW1jtPvRQ==",
                    "L4YSQ77XGZGCHNGL2M7RUOH6MRUXPXFP",
                    "Matheus de Assis",
                    "05955331328",
                    "85989477381",
                    "assis4002@gmail.com",
                    "2021059",
                    DateTime.Now,
                    DateTime.Now,
                    null
                );

            AccessLevels levelAluno = new AccessLevels("Aluno", "Possui acesso ao portal de cursos, podendo estar matriculado em apenas um curso", "ALUNO");
            AccessLevels levelProfessor = new AccessLevels("Professor", " ", "PROFESSOR");
            AccessLevels levelAdministrador = new AccessLevels("Administrador", " ", "ADMINISTRADOR");

            Course course01 = new Course
                (
                    "C# Completo", "~/imagesCourses/a809bbeb-e4f7-4c8a-a6f6-749db1440897.jpg",
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla ultrices rhoncus turpis. Donec pretium quis risus tincidunt convallis. In finibus eu nunc ac molestie. Sed porttitor augue sed metus condimentum pellentesque. Mauris et urna consectetur, efficitur ligula et, scelerisque dolor. Pellentesque mi leo, facilisis sed nibh non, suscipit maximus magna.",
                    80, DateTime.Now, DateTime.Now, 30, "0e6ba977-4c54-4c41-a9c2-fa9599e1db50"
                );

            Course course02 = new Course
                (
                    "Java Completo", "~/imagesCourses/c0b5a08e-6287-46cd-a0fa-7a94b613567b.jpg",
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla ultrices rhoncus turpis. Donec pretium quis risus tincidunt convallis. In finibus eu nunc ac molestie. Sed porttitor augue sed metus condimentum pellentesque. Mauris et urna consectetur, efficitur ligula et, scelerisque dolor. Pellentesque mi leo, facilisis sed nibh non, suscipit maximus magna.",
                    80, DateTime.Now, DateTime.Now, 50, "0e6ba977-4c54-4c41-a9c2-fa9599e1db50"
                );

            Course course03 = new Course
                (
                    "Python Completo", "~/imagesCourses/87da01de-a94e-418a-89ab-4fc0f272ef77.jpg",
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla ultrices rhoncus turpis. Donec pretium quis risus tincidunt convallis. In finibus eu nunc ac molestie. Sed porttitor augue sed metus condimentum pellentesque. Mauris et urna consectetur, efficitur ligula et, scelerisque dolor. Pellentesque mi leo, facilisis sed nibh non, suscipit maximus magna.",
                    80, DateTime.Now, DateTime.Now, 70, "0e6ba977-4c54-4c41-a9c2-fa9599e1db50"
                );

            _context.Users.AddRange(primaryUser);
            _context.AccessLevels.AddRange(levelAluno, levelProfessor, levelAdministrador);
            _context.Courses.AddRange(course01, course02, course03);
            _context.SaveChanges();
        }
    }
}
