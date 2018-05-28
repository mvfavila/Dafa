using System;
using System.ComponentModel.DataAnnotations;

namespace DAFA.Application.ViewModels
{
    /// <summary>
    /// Class that represents an Event Type<br/>
    /// e.g. Warning 60 days before.
    /// </summary>
    public class EventTypeViewModel
    {
        public EventTypeViewModel()
        {
            EventTypeId = Guid.NewGuid();
            Active = true;
        }

        [ScaffoldColumn(false)]
        public Guid EventTypeId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome é requerido")]
        [MaxLength(150, ErrorMessage = "O tamanho máximo do campo Nome é de 250 caracteres")]
        public string Name { get; set; }

        [Display(Name = "Período para Aviso")]
        [Required(ErrorMessage = "O campo Período para Aviso é requerido")]
        public int NumberOfDaysToWarning { get; set; }

        [Display(Name = "Ativo")]
        [ScaffoldColumn(false)]
        public bool Active { get; set; }
    }
}
