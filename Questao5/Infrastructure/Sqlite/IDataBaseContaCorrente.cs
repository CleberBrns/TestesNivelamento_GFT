using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Sqlite
{
    public interface IDataBaseContaCorrente
    {
        public Task<ContaCorrente> GetById(string idContaCorrente);

        public Task<string> RegistrarMovimento(Movimento movimento);

        public Task<IEnumerable<ValorMovimento>> GetValoresMovimentoConta(string idContaCorrente);
    }
}
