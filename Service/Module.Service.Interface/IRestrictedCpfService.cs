using Module.Dto;
using Module.Service.Interface.Base;
using System;
using System.Collections.Generic;

namespace Module.Service.Interface
{
    public interface IRestrictedCpfService : IBaseEntityService<RestrictedCpfDto, Guid>
    {
        List<RestrictedCpfDto> GetAll();

        RestrictedCpfDto GetByCpf(string cpf);
    }
}