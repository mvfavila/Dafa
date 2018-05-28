using DAFA.Application.ViewModels;
using DAFA.Domain.Entities;
using System.Collections.Generic;

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
