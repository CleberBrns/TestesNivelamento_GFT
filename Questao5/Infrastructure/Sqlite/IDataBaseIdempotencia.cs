using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Sqlite
{
    public interface IDataBaseIdempotencia
    {
        public IEnumerable<Idempotencia> GetByTipoRequisicao(TipoRequisicao tipoRequisicao);
    }
}
