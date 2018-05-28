using System;
using System.ComponentModel.DataAnnotations;

namespace DAFA.Application.ViewModels
{
    public class FieldViewModel
    {
        public FieldViewModel()
        {
            FieldId = Guid.NewGuid();
        }

        [ScaffoldColumn(false)]
        public Guid FieldId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome é requerido")]
        [MaxLength(250, ErrorMessage = "O tamanho máximo do campo Nome é de 250 caracteres")]
        public string Name { get; set; }

        //[Display(Name = "Ativo")]
        [ScaffoldColumn(false)]
        public bool Active { get; set; }

        [ScaffoldColumn(false)]
        public Guid ClientId { get; set; }
    }
}
