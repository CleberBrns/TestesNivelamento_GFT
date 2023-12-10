using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Sqlite
{
    public class DataBaseIdempotencia : IDataBaseIdempotencia
    {
        private readonly DatabaseConfig databaseConfig;

        public DataBaseIdempotencia(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public IEnumerable<Idempotencia> GetByTipoRequisicao(TipoRequisicao tipoRequisicao)
        {
            string query = $"SELECT chave_idempotencia as ChaveIdempotencia, requisicao, resultado FROM idempotencia WHERE requisicao = '{tipoRequisicao}'";
            using (var connection = new SqliteConnection(databaseConfig.Name))
            {
                return connection.Query<Idempotencia>(query);
            }
        }
    }
}
