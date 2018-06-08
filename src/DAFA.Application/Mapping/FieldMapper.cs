using DAFA.Application.ViewModels;
using DAFA.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DAFA.Application.Mapping
{
    public static class FieldMapper
    {
        // FieldViewModel

        internal static Field FromViewModelToDomain(FieldViewModel fieldViewModel)
        {
            return Field.FactoryMap(
                fieldViewModel.FieldId,
                fieldViewModel.Name,
                EventMapper.FromViewModelToDomain(fieldViewModel.Events),
                fieldViewModel.ClientId,
                fieldViewModel.Active
                );
        }

        internal static FieldViewModel FromDomainToViewModel(Field fieldObj)
        {
            return new FieldViewModel
            {
                FieldId = fieldObj.FieldId,
                Name = fieldObj.Name,
                Events = EventMapper.FromDomainToViewModel(fieldObj.Events).ToList(),
                ClientId = fieldObj.ClientId,
                Active = fieldObj.Active
            };
        }

        internal static IEnumerable<FieldViewModel> FromDomainToViewModel(IEnumerable<Field> fields)
        {
            var viewModels = new List<FieldViewModel>();
            foreach (var field in fields)
            {
                viewModels.Add(FromDomainToViewModel(field));
            }
            return viewModels;
        }
    }
}
