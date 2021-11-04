using AutoMapper;
using Module.Dto;
using Module.Repository.Model;

namespace Module.IoC.Mapper.Configuracoes
{
    public class RestrictedCpfProfile : Profile
    {
        public RestrictedCpfProfile()
        {
            this.CreateMap<RestrictedCpf, RestrictedCpfDto>()
                .ReverseMap();
        }
    }
}