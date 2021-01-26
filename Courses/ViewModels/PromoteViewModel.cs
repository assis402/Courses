using System;
using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels
{
    public class PromoteViewModel
    {
        public string Id { get; set; }

        public DateTime UpdateDate { get; set; }

        public string AccessLevel { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string NewAccessLevel { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string AuthorizationKey { get; set; }

    }
}
