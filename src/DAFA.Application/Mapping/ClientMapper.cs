using DAFA.Application.ViewModels;
using DAFA.Domain.Entities;
using System.Collections.Generic;
using System;

namespace DAFA.Application.Mapping
{
    public static class ClientMapper
    {
        // ClientViewModel
        internal static Client FromViewModelToDomain(ClientViewModel clientViewModel)
        {
            return Client.FactoryMap(
                clientViewModel.ClientId,
                clientViewModel.Name,
                clientViewModel.Email,
                clientViewModel.Phone,
                clientViewModel.Active
                );
        }

        internal static ClientViewModel FromDomainToViewModel(Client clientObj)
        {
            return new ClientViewModel
            {
                ClientId = clientObj.ClientId,
                Name = clientObj.Name,
                Email = clientObj.Email,
                Phone = clientObj.Phone,
                Active = clientObj.Active
            };
        }

        internal static IEnumerable<ClientViewModel> FromDomainToViewModel(IEnumerable<Client> clients)
        {
            var result = new List<ClientViewModel>();
            foreach (var client in clients)
            {
                result.Add(FromDomainToViewModel(client));
            }
            return result;
        }

        // ClientFieldViewModel

        internal static ClientFieldViewModel FromDomainToViewModel(Client clientObj, IEnumerable<Field> fields)
        {
            var fieldViewModels = new List<FieldViewModel>();
            foreach (var field in fields)
            {
                fieldViewModels.Add(FieldMapper.FromDomainToViewModel(field));
            }

            return new ClientFieldViewModel
            {
                ClientId = clientObj.ClientId,
                Name = clientObj.Name,
                Email = clientObj.Email,
                Phone = clientObj.Phone,
                Active = clientObj.Active,
                Fields = fieldViewModels
            };
        }
    }
}
