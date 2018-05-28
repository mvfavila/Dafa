using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAFA.Application.ViewModels
{
    public class ClientFieldViewModel
    {
        public ClientFieldViewModel()
        {
            ClientId = Guid.NewGuid();
            Fields = new List<FieldViewModel>();
            Active = true;
        }

        [ScaffoldColumn(false)]
        public Guid ClientId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome é requerido")]
        [MaxLength(200, ErrorMessage = "O tamanho máximo do campo Nome é de 200 caracteres")]
        public string Name { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Required(ErrorMessage = "O campo E-mail é requerido")]
        [MaxLength(100, ErrorMessage = "O tamanho máximo do campo E-mail é de 100 caracteres")]
        public string Email { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "O campo Telefone é requerido")]
        [MaxLength(25, ErrorMessage = "O tamanho máximo do campo Telefone é de 25 caracteres")]
        public string Phone { get; set; }

        [Display(Name = "Ativo")]
        [ScaffoldColumn(false)]
        public bool Active { get; set; }

        [ScaffoldColumn(false)]
        public IEnumerable<FieldViewModel> Fields { get; set; }
    }
}
