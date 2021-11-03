using Module.Factory.Base;
using Module.Factory.Interface.Conexao;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

namespace Module.Factory.Conexao
{
    /// <summary>
    /// Classe factory de conexão com banco de dados
    /// </summary>
    public class DbConnectionFactory : BaseFactory, IDbConnectionFactory, IDbTransactionFactory
    {
        private int _contadorTransacao;
        private IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction;

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
        public DbConnectionFactory(string sqliteDirectory, string rootPath)
        {
            var connectionString = TryBoostrapDataBase(sqliteDirectory, rootPath);

            _dbTransaction = null;
            _dbConnection = new SQLiteConnection(connectionString);
            _dbConnection.Open();
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
        /// Metodo para abrir transação
        /// </summary>
        public void Open()
        {
            if (_dbTransaction == null || _dbTransaction.Connection.State == ConnectionState.Closed)
                _dbTransaction = DbConnection.BeginTransaction();

            _contadorTransacao++;
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
            if (_dbConnection == null)
                return;

            Rollback();
            _dbConnection.Close();
            _dbConnection = null;
        }

        private static void CreateCaptureTable(SQLiteConnection tempConnection)
        {
            using var createCaptureTable = tempConnection.CreateCommand();
            createCaptureTable.CommandText = "CREATE TABLE IF NOT EXISTS PokemonCapture(id text primary key, pokemon_id int, pokemon_name varchar(50), trainer_cpf varchar(11), trainer_name varchar(50))";
            createCaptureTable.ExecuteNonQuery();
        }

        private static string TryBoostrapDataBase(string sqliteDirectory, string rootPath)
        {
            var sqliteFile = string.Concat(rootPath, sqliteDirectory);
            var connectionString = $"Data Source={sqliteFile}; Version=3;";
            if (!File.Exists(sqliteFile))
            {
                var dirInfo = new DirectoryInfo(sqliteFile).Parent;
                if (!(dirInfo.Exists))
                    dirInfo.Create();

                SQLiteConnection.CreateFile(sqliteFile);

                using var tempConnection = new SQLiteConnection(connectionString);
                tempConnection.Open();
                CreateCaptureTable(tempConnection);
            }

            return connectionString;
        }
    }
}