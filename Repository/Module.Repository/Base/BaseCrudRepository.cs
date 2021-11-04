using Dapper;
using Module.Factory.Interface.Conexao;
using Module.Repository.Interface.Base;
using Module.Repository.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.Repository.Base
{
    /// <summary>
    /// Classe base para repositorios que implementa métodos de CRUD utilizando o DAPPER com um modelo de dados (entity) já informado
    /// </summary>
    /// <typeparam name="TModel">Modelo de dados</typeparam>
    public class BaseCrudRepository<TModel> : BaseRepository, IBaseCrudRepository<TModel>
        where TModel : BaseModel
    {
        public BaseCrudRepository(IDbConnectionFactory dbService)
                   : base(dbService)
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLServer);
        }

        /// <summary>
        /// Remove entidade em banco
        /// </summary>
        /// <param name="entity">Modelo informado</param>
        /// <returns>Resultado da ação</returns>
        public virtual int Delete(TModel entity)
        {
            return _dbService.DbConnection.Delete<TModel>(entity, transaction: _dbService.Transaction);
        }

        /// <summary>
        /// Obtem lista com todos os objetos da entidade salvos em banco
        /// </summary>
        /// <returns>Lista de entidades</returns>
        public virtual List<TModel> GetAll()
        {
            var resultadoEnumerado = _dbService.DbConnection.GetList<TModel>();

            return resultadoEnumerado.ToList();
        }

        /// <summary>
        /// Retorna uma lista de acordo com o filtro dinamico informado
        /// </summary>
        /// <param name="whereConditions">Objeto dinamico para filtro</param>
        /// <returns>Lista de acordo com o filtro dinamico</returns>
        public virtual List<TModel> GetByDynamicFilter(object whereConditions)
        {
            var resultadoEnumerado = _dbService.DbConnection.GetList<TModel>(whereConditions, transaction: _dbService.Transaction);

            return resultadoEnumerado.ToList();
        }

        /// <summary>
        /// Obtem modelo pelo id informado
        /// </summary>
        /// <param name="id">Identificador do modelo</param>
        /// <returns>Modelo do id informado</returns>
        public virtual TModel GetEntityById(Guid id)
        {
            return GetEntityById<Guid>(id);
        }

        /// <summary>
        /// Obtem modelo pelo id informado por tipo dinamico
        /// </summary>
        /// <typeparam name="IdType">Tipo do id</typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TModel GetEntityById<IdType>(IdType id)
        {
            return _dbService.DbConnection.Get<TModel>(id, transaction: _dbService.Transaction);
        }

        /// <summary>
        /// Retorna o primeiro retorno de acordo com filtro dinamico
        /// </summary>
        /// <param name="whereConditions">Objeto dinamico para filtro</param>
        /// <returns>Lista de acordo com o filtro dinamico</returns>
        public TModel GetFirstEntityByDynamicFilter(object whereConditions)
        {
            var modeloLista = _dbService.DbConnection.GetList<TModel>(whereConditions, transaction: _dbService.Transaction);

            return modeloLista.FirstOrDefault();
        }

        /// <summary>
        /// Insere entidade em banco
        /// </summary>
        /// <param name="entity">Modelo informado</param>
        /// <returns>Resultado da ação</returns>
        public virtual TKeyType Insert<TKeyType>(TModel entity)
        {
            return _dbService.DbConnection.Insert<TKeyType, TModel>(entity, transaction: _dbService.Transaction);
        }

        /// <summary>
        /// Atualiza um modelo em banco
        /// </summary>
        /// <param name="entity">Modelo informado</param>
        /// <returns>Resultado da ação</returns>
        public virtual int Update(TModel entity)
        {
            return _dbService.DbConnection.Update<TModel>(entity, transaction: _dbService.Transaction);
        }
    }
}