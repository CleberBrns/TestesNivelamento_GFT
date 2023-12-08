using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;

namespace Questao5.Infrastructure.Services.BusinessLogic
{
    public interface ILogicValidationMovimento
    {
        public ResultResponse ValidarMovimentacao(MovimentoRequest movimentoRequest);
    }
}
