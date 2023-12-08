using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Services.BusinessLogic
{
    public interface ILogicContaCorrente
    {
        public Task<ResultResponse> RegistrarMovimento(MovimentoRequest movimentoRequest);

        public Task<ResultResponse> GetSaldoContaCorrente(string idContaCorrente);
    }
}
