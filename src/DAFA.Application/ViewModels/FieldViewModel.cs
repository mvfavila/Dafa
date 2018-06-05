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

            Events = new List<EventViewModel>
            {
                new EventViewModel
                {
                    EventId = Guid.NewGuid(),
                    Name = "Event #1",
                    Description = "Description of event #1",
                    Date = DateTime.Now,
                    EventTypeId = Guid.Parse("7a1a3567-9cd5-41ff-8f17-e76bdc207f69")
                },
                new EventViewModel
                {
                    EventId = Guid.NewGuid(),
                    Name = "Event #2",
                    Description = "Description of event #2",
                    Date = DateTime.Now.AddDays(2),
                    EventTypeId = Guid.Parse("7a1a3567-9cd5-41ff-8f17-e76bdc207f69")
                }
            };
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
