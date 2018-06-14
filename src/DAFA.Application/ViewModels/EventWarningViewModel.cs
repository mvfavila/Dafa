using System;
using System.ComponentModel.DataAnnotations;

namespace DAFA.Application.ViewModels
{
    public class EventWarningViewModel
    {
        [ScaffoldColumn(false)]
        public Guid EventWarningId { get; set; }

        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public bool Solved { get; set; }

        [ScaffoldColumn(false)]
        public Guid EventId { get; set; }

        public EventViewModel Event { get; set; }

        [ScaffoldColumn(false)]
        public Guid FieldId { get; set; }

        public FieldViewModel Field { get; set; }
    }
}
