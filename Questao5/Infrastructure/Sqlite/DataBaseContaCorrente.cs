using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Questao5.Domain.Entities;
using System.Text;

namespace Questao5.Infrastructure.Sqlite
{
    public class DataBaseContaCorrente : IDataBaseContaCorrente
    {
        private readonly DatabaseConfig databaseConfig;

        public DataBaseContaCorrente(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<ContaCorrente> GetById(string idContaCorrente)
        {
            string query = $"SELECT * FROM contacorrente WHERE idContaCorrente = '{idContaCorrente}'";
            using (var connection = new SqliteConnection(databaseConfig.Name))
            {
                return await connection.QueryFirstOrDefaultAsync<ContaCorrente>(query);
            }
        }

        public async Task<string> RegistrarMovimento(Movimento movimento)
        {
            StringBuilder query = new();
            query.Append("INSERT INTO movimento ");
            query.Append("(idcontacorrente, datamovimento, tipomovimento, valor) ");
            query.Append("VALUES ");
            query.Append("(@IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor) ");
            query.Append("RETURNING idmovimento");

            using (var connection = new SqliteConnection(databaseConfig.Name))
            {
                return await connection.ExecuteScalarAsync<string>(query.ToString(), param: movimento);
            }
        }

        public async Task<IEnumerable<ValorMovimento>> GetValoresMovimentoConta(string idContaCorrente)
        {
            string query = $"SELECT tipomovimento, valor  FROM movimento WHERE idContaCorrente = '{idContaCorrente}'";
            using (var connection = new SqliteConnection(databaseConfig.Name))
            {
                return await connection.QueryAsync<ValorMovimento>(query);
            }
        }
    }
}
