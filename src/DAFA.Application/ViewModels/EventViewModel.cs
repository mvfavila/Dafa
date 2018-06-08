using System;
using System.ComponentModel.DataAnnotations;

namespace DAFA.Application.ViewModels
{

    public class EventViewModel
    {
        public EventViewModel()
        {
            EventId = Guid.NewGuid();
            FieldId = Guid.NewGuid();
            EventTypeId = Guid.NewGuid();
        }

        [ScaffoldColumn(false)]
        public Guid EventId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome é requerido")]
        [MaxLength(150, ErrorMessage = "O tamanho máximo do campo Nome é de 200 caracteres")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo Descrição é requerido")]
        [MaxLength(150, ErrorMessage = "O tamanho máximo do campo Descrição é de 250 caracteres")]
        public string Description { get; set; }

        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [ScaffoldColumn(false)]
        public Guid FieldId { get; set; }

        [ScaffoldColumn(false)]
        public Guid EventTypeId { get; set; }
    }
}
