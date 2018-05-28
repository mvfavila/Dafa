using DAFA.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations;

namespace DAFA.Application.ViewModels
{
    public class PeriodicityViewModel
    {
        public PeriodicityViewModel()
        {
            PeriodicityId = Guid.NewGuid();
        }

        [ScaffoldColumn(false)]
        public Guid PeriodicityId { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo Descrição é requerido")]
        [MaxLength(150, ErrorMessage = "O tamanho máximo do campo Descrição é de 100 caracteres")]
        public string Description { get; set; }

        [Display(Name = "Prazo")]
        [Required(ErrorMessage = "O campo Prazo é requerido")]
        public int Quantity { get; set; }

        [Display(Name = "Unidade de Medida")]
        [Required(ErrorMessage = "O campo Unidade de Medida é requerido")]
        public byte Unit { get; set; }

        [Display(Name = "Ativo")]
        [ScaffoldColumn(false)]
        public bool Active { get; set; }
    }

    public class UnitViewModel
    {
        public string Description { get; set; }

        public byte Value { get; set; }
    }
}
