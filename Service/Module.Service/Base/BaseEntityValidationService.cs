using Module.Dto.Base;
using Module.Repository.Interface.Base;
using Module.Repository.Model.Base;
using Module.Service.Interface.Base;
using Module.Service.Validation.Interface.Base;

namespace Module.Service.Base
{
    public abstract class BaseEntityValidationService<TModel, TDto, TKeyType, TValidation> : BaseEntityService<TModel, TDto, TKeyType>
        where TModel : BaseModel
        where TValidation : IBaseCrudValidation<TModel>
        where TDto : BaseDto
    {
        public virtual TValidation CrudValidation { get; set; }

        /// <summary>
        /// Efetua a chamada para validação de deleção de dados da entidade
        /// </summary>
        /// <param name="model">Entidade de modelo </param>
        public override void ValidateDeletion(TModel model)
        {
            CrudValidation.ValidateDeletion(model);
        }

        /// <summary>
        /// Efetua a chamada para validação de atualização de dados da entidade
        /// </summary>
        /// <param name="model">Entidade de modelo </param>
        public override void ValidateUpdate(TModel model)
        {
            CrudValidation.ValidateUpdate(model);
        }

        /// <summary>
        /// Efetua a chamada para validação de inserção de dados da entidade
        /// </summary>
        /// <param name="model">Entidade de modelo </param>
        public override void ValidateInsert(TModel model)
        {
            CrudValidation.ValidateInsert(model);
        }
    }
}