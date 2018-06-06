using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAFA.Application.ViewModels
{
    public class FieldViewModel
    {
        public FieldViewModel()
        {
            FieldId = Guid.NewGuid();
            Events = new List<EventViewModel>();
        }

        [ScaffoldColumn(false)]
        public Guid FieldId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome é requerido")]
        [MaxLength(250, ErrorMessage = "O tamanho máximo do campo Nome é de 250 caracteres")]
        public string Name { get; set; }

        public IList<EventViewModel> Events { get; set; }

        //[Display(Name = "Ativo")]
        [ScaffoldColumn(false)]
        public bool Active { get; set; }

        [ScaffoldColumn(false)]
        public Guid ClientId { get; set; }
    }
}
