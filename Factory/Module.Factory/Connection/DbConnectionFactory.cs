using Module.Factory.Base;
using Module.Factory.Interface.Conexao;
using System.Data;
using System.Data.SqlClient;

namespace Module.Factory.Conexao
{
    /// <summary>
    /// Classe factory de conexão com banco de dados
    /// </summary>
    public class DbConnectionFactory : BaseFactory, IDbConnectionFactory, IDbTransactionFactory
    {
        private IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction;
        private int _contadorTransacao;

        /// <summary>
        /// Conexão do banco de dados
        /// </summary>
        public IDbConnection DbConnection
        {
            get
            {
                if (_dbConnection != null && _dbConnection.State == ConnectionState.Closed)
                    _dbConnection.Open();

                return _dbConnection;
            }
        }

        /// <summary>
        /// Transação atual aberta (utilizada tanto nos services como no repository)
        /// </summary>
        public IDbTransaction Transaction
        {
            get
            {
                return _dbTransaction;
            }
        }

        /// <summary>
        /// Inicializa a conexão e abre a mesma
        /// </summary>
        /// <param name="connectionString">String de conexão informada pelo construtor da injeção de dependencia</param>
        public DbConnectionFactory(string connectionString)
        {
            _dbTransaction = null;
            _dbConnection = new SqlConnection(connectionString);
            _dbConnection.Open();
        }

        /// <summary>
        /// Metodo para abrir transação
        /// </summary>
        public void Open()
        {
            if (_dbTransaction == null || _dbTransaction.Connection.State == ConnectionState.Closed)
                _dbTransaction = DbConnection.BeginTransaction();

            _contadorTransacao++;
        }

        /// <summary>
        /// Método para atualizar transação
        /// </summary>
        public void Commit()
        {
            _contadorTransacao--;

            if (_contadorTransacao <= 0)
                _dbTransaction.Commit();
        }

        /// <summary>
        /// Método para rollback da transação
        /// </summary>
        public void Rollback()
        {
            if (_dbTransaction != null && _dbTransaction.Connection != null)
                _dbTransaction.Rollback();
        }

        /// <summary>
        /// Dispose pattern
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Rollback();
            _dbConnection.Close();
            _dbConnection = null;
        }
    }
}