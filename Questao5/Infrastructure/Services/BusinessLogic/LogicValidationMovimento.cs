using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Services.BusinessLogic
{
    public class LogicValidationMovimento
    {
        readonly ContaCorrente contaCorrente;
        readonly IEnumerable<Idempotencia> listIdempotencia;

        public LogicValidationMovimento(ContaCorrente contaCorrente, IEnumerable<Idempotencia> listIdempotencia)
        {
            this.contaCorrente = contaCorrente;
            this.listIdempotencia = listIdempotencia;
        }

        public ResultResponse ValidarMovimentacao(MovimentoRequest movimentoRequest)
        {
            bool houveErro = false;
            string msgRetorno = string.Empty;

            if (contaCorrente == null)
            {
                houveErro = true;
                msgRetorno = GetIdempotenciaByTipo(TipoIdempotencia.INVALID_ACCOUNT_POST);
            }
            else
            {
                if (!contaCorrente.Ativo)
                {
                    houveErro = true;
                    msgRetorno = GetIdempotenciaByTipo(TipoIdempotencia.INACTIVE_ACCOUNT_POST);
                }
                else if (movimentoRequest.Valor == 0 || movimentoRequest.Valor < 0)
                {
                    houveErro = true;
                    msgRetorno = GetIdempotenciaByTipo(TipoIdempotencia.INVALID_VALUE_POST);
                }
                else if (movimentoRequest.TipoMovimento != "crédito" && movimentoRequest.TipoMovimento != "débito")
                {
                    houveErro = true;
                    msgRetorno = GetIdempotenciaByTipo(TipoIdempotencia.INVALID_TYPE_POST);
                }
            }

            return new ResultResponse(houveErro, msgRetorno);
        }

        public ResultResponse ValidarConsultaSaldo()
        {
            bool houveErro = false;
            string msgRetorno = string.Empty;

            if (contaCorrente == null)
            {
                houveErro = true;
                msgRetorno = GetIdempotenciaByTipo(TipoIdempotencia.INVALID_ACCOUNT_GET);
            }
            else
            {
                if (!contaCorrente.Ativo)
                {
                    houveErro = true;
                    msgRetorno = GetIdempotenciaByTipo(TipoIdempotencia.INACTIVE_ACCOUNT_GET);
                }
            }

            return new ResultResponse(houveErro, msgRetorno);
        }

        private string GetIdempotenciaByTipo(TipoIdempotencia tipoIdempotencia)
        {
            if (listIdempotencia != null && listIdempotencia.Any())
            {
                Idempotencia? idempotencia = listIdempotencia.FirstOrDefault(x => x.ChaveIdempotencia == tipoIdempotencia.ToString());

                return idempotencia != null ? 
                        $"Tipo de Falha: {idempotencia.ChaveIdempotencia}, Descrição da Falha: {idempotencia.Resultado}" : 
                        string.Empty;
            }

            return string.Empty;
        }
    }
}
