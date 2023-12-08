using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Services.BusinessLogic
{
    public class LogicContaCorrente : ILogicContaCorrente
    {
        readonly IDataBaseContaCorrente dataBaseContaCorrente;
        readonly IDataBaseIdempotencia dataBaseIdempotencia;

        IEnumerable<Idempotencia> ListIdempotencia;

        public LogicContaCorrente(IDataBaseContaCorrente dataBaseContaCorrente, IDataBaseIdempotencia dataBaseIdempotencia)
        {
            this.dataBaseContaCorrente = dataBaseContaCorrente;
            this.dataBaseIdempotencia = dataBaseIdempotencia;

            ListIdempotencia = dataBaseIdempotencia.GetAll();
        }

        /// <summary>
        /// Registrar um movimente de uma Conta Corrente
        /// </summary>
        /// <param name="movimentoRequest"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultResponse> RegistrarMovimento(MovimentoRequest movimentoRequest)
        {
            bool houveErro = false;
            string msgRetorno = string.Empty;
            ListIdempotencia = GetIdempotenciaByTipoRequisicao(TipoRequisicao.MovimentacaoContaCorrente);

            try
            {
                ContaCorrente contaCorrente = await dataBaseContaCorrente.GetById(movimentoRequest.IdContaCorrente);

                LogicValidationMovimento validacao = new(contaCorrente, ListIdempotencia);
                ResultResponse resultResponse = validacao.ValidarMovimentacao(movimentoRequest);

                if (!resultResponse.HouveErro)
                {
                    Movimento movimento = new Movimento
                    {
                        IdContaCorrente = movimentoRequest.IdContaCorrente,
                        TipoMovimento = movimentoRequest.TipoMovimento == "crédito" ? 'C' : 'D',
                        Valor = movimentoRequest.Valor,
                        DataMovimento = DateTime.Now.ToString("dd/MM/yyyy")
                    };

                    string idMovimentoGerado = await dataBaseContaCorrente.RegistrarMovimento(movimento);
                    msgRetorno = $"IdMovimento gerado: {idMovimentoGerado}";
                }
                else
                {
                    houveErro = resultResponse.HouveErro;
                    msgRetorno = resultResponse.MensagemRetorno;
                }
            }
            catch (Exception ex)
            {
                houveErro = true;
                msgRetorno = $"Exceção não tratada durante o processamento. {ex.Message}";
            }

            return new ResultResponse(houveErro, msgRetorno);
        }

        public async Task<ResultResponse> GetSaldoContaCorrente(string idContaCorrente)
        {
            bool houveErro = false;
            string msgRetorno = string.Empty;

            try
            {
                ContaCorrente contaCorrente = await dataBaseContaCorrente.GetById(idContaCorrente);

                LogicValidationMovimento validacao = new(contaCorrente, ListIdempotencia);
                ResultResponse resultResponse = validacao.ValidarConsultaSaldo();

                if (!resultResponse.HouveErro)
                {
                    IEnumerable<ValorMovimento> valoresMovimentoConta = await dataBaseContaCorrente.GetValoresMovimentoConta(idContaCorrente);

                    if (valoresMovimentoConta.Any())
                    {
                        decimal saldoContaCorrente = CalcularSaldosContaCorrente(valoresMovimentoConta);

                        msgRetorno = $"O Saldo da Conta informada é {saldoContaCorrente}";
                    }
                    else
                    {
                        msgRetorno = "O Saldo da Conta informada é 0.00.";
                    }
                }
                else
                {
                    houveErro = resultResponse.HouveErro;
                    msgRetorno = resultResponse.MensagemRetorno;
                }

            }
            catch (Exception ex)
            {
                houveErro = true;
                msgRetorno = $"Exceção não tratada durante o processamento. {ex.Message}";
            }

            return new ResultResponse(houveErro, msgRetorno);
        }

        private IEnumerable<Idempotencia> GetIdempotenciaByTipoRequisicao(TipoRequisicao tipoRequisicao)
        {
            if (ListIdempotencia != null)
            {
                return ListIdempotencia.Where(x => x.Requisicao == tipoRequisicao.ToString());
            }

            return new List<Idempotencia>();
        }

        private decimal CalcularSaldosContaCorrente(IEnumerable<ValorMovimento> valoresMovimentoConta)
        {
            decimal saldoTotalDebitos = 0;
            decimal saldoTotalCreditos = 0;

            saldoTotalCreditos = valoresMovimentoConta.Where(x => x.TipoMovimento == 'C').Select(x => x.Valor).Sum();
            saldoTotalDebitos = valoresMovimentoConta.Where(x => x.TipoMovimento == 'D').Select(x => x.Valor).Sum();

            return saldoTotalCreditos - saldoTotalDebitos;
        }
    }
}
