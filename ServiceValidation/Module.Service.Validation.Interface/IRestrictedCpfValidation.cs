using Module.Repository.Model;
using Module.Service.Validation.Interface.Base;

namespace Module.Service.Validation.Interface
{
    public interface IRestrictedCpfValidation : IBaseCrudValidation<RestrictedCpf>
    {
        void ValidateSearch(RestrictedCpf restrictedCpf);
    }
}