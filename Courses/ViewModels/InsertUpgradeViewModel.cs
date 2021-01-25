using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels
{
    public class InsertUpgradeViewModel
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(30, ErrorMessage = "Use até 30 caracteres")]
        public string Title { get; set; }
        public IList<string> FeaturesId { get; set; }
    }
}
