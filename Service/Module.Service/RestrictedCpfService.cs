using Module.Dto;
using Module.Repository.Model;
using Module.Service.Base;
using Module.Service.Interface;
using Module.Service.Validation.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.Service
{
    public class RestrictedCpfService : BaseEntityValidationService<RestrictedCpf, RestrictedCpfDto, string, IRestrictedCpfValidation>, IRestrictedCpfService
    {
        public void DeleteByCpf(string cpf)
        {
            var restrictedCpf = CrudRepository.GetAll().Where(t => t.Cpf == cpf).FirstOrDefault();
            ValidateDeletion(restrictedCpf);

            CrudRepository.Delete(restrictedCpf);
        }

        public List<RestrictedCpfDto> GetAll()
        {
            var restrictedCpfList = CrudRepository.GetAll();

            return Mapper.Map<List<RestrictedCpfDto>>(restrictedCpfList);
        }

        public RestrictedCpfDto GetByCpf(string cpf)
        {
            var restrictedCpf = CrudRepository
                .GetAll()
                .Where(t => t.Cpf == cpf)
                .FirstOrDefault();
            CrudValidation.ValidateSearch(restrictedCpf);

            return Mapper.Map<RestrictedCpfDto>(restrictedCpf);
        }
    }
}