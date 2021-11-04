using Module.Dto.Base;

namespace Module.Service.Interface.Base
{
    public interface IBaseEntityValidationService<TModel, TDto, TKeyType, TValidation> : IBaseEntityService<TDto, TKeyType>
        where TDto : BaseDto
    {
    }
}