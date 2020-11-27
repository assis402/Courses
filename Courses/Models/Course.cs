using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class Course
    {
        public string CourseId { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public int CargaHoraria { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public double Preco { get; set; }
        public ICollection<UserCourse> Alunos { get; set; }
    }
}
