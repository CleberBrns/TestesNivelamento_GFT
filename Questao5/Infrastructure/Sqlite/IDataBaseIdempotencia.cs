using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Sqlite
{
    public interface IDataBaseIdempotencia
    {
        public IEnumerable<Idempotencia> GetAll();
    }
}
