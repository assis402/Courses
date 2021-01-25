using System;
using System.Collections.Generic;

namespace Courses.Models
{
    public class Course
    {
        public string CourseId { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public string Description { get; set; }
        public int CargaHoraria { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public double Preco { get; set; }
        public string UserId { get; set; }
        public ICollection<Matriculation> Alunos { get; set; }
    }
}
