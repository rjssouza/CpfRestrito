﻿using Module.Dto.Base;
using Module.Repository.Interface.Base;
using Module.Repository.Model.Base;
using Module.Service.Interface.Base;
using System;

namespace Module.Service.Base
{
    public class BaseEntityService<TModel, TDto, TKeyType> : BaseService, IBaseEntityService<TDto, TKeyType>
        where TModel : BaseModel
        where TDto : BaseDto
    {
        public virtual IBaseCrudRepository<TModel> CrudRepository { get; set; }

        /// <summary>
        /// Remove o entidade efetuando as validações necessarias
        /// </summary>
        /// <param name="id">Identificador do anuncio de locação de imoveis</param>
        public virtual void Delete(TKeyType id)
        {
            var model = CrudRepository.GetEntityById(id);
            ValidateDeletion(model);

            OpenTransaction();
            CrudRepository.Delete(model);

            Commit();
        }

        /// <summary>
        /// Obtem pelo identificador
        /// </summary>
        /// <param name="id">Identificador da entidade</param>
        /// <returns>Objeto de dados da entidade</returns>
        public virtual TDto GetById(TKeyType id)
        {
            var model = CrudRepository.GetEntityById<TKeyType>(id);
            var resultado = Mapper.Map<TDto>(model);

            return resultado;
        }

        /// <summary>
        /// Insere os dados da entidade efetuando as validaões necessarias
        /// </summary>
        /// <param name="dtoObject">Objeto dto </param>
        public virtual TKeyType Insert(TDto dtoObject)
        {
            var model = Mapper.Map<TModel>(dtoObject);
            ValidateInsert(model);

            OpenTransaction();
            var result = CrudRepository.Insert<TKeyType>(model);

            Commit();

            return result;
        }

        /// <summary>
        /// Atualiza as informações da entidade efetuando as validações necessarias
        /// </summary>
        /// <param name="dtoObject">Objeto dto </param>
        public virtual void Update(TDto dtoObject)
        {
            var model = Mapper.Map<TModel>(dtoObject);
            ValidateUpdate(model);

            OpenTransaction();
            CrudRepository.Update(model);

            Commit();
        }

        /// <summary>
        /// Efetua a chamada para validação de atualização de dados da entidade
        /// </summary>
        /// <param name="model">Entidade de modelo </param>
        public virtual void ValidateDeletion(TModel model)
        {
        }

        /// <summary>
        /// Efetua a chamada para validação de inserção de dados da entidade
        /// </summary>
        /// <param name="model">Entidade de modelo </param>
        public virtual void ValidateInsert(TModel model)
        {
        }

        /// <summary>
        /// Efetua a chamada para validação de deleção de dados da entidade
        /// </summary>
        /// <param name="model">Entidade de modelo </param>
        public virtual void ValidateUpdate(TModel model)
        {
        }
    }
}