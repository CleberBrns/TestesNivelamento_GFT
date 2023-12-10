namespace Questao5.Application.Commands.Responses
{
    /// <summary>
    /// Objeto de Retorno de uma execução
    /// </summary>
    public class ResultResponse
    {
        /// <summary>
        /// Indica se ocorreu algum erro durante uma execução
        /// </summary>
        public bool HouveErro { get; set; }

        /// <summary>
        /// Exibe uma mensagem de acordo com o retorno de uma execução
        /// </summary>
        public string MensagemRetorno { get; set; }

        public ResultResponse(bool houveErro, string mensagemRetorno)
        {
            this.HouveErro = houveErro;
            this.MensagemRetorno = mensagemRetorno;
        }
    }
}
