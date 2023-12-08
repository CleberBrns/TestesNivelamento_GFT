namespace Questao5.Application.Commands.Responses
{
    public class ResultResponse
    {
        public bool HouveErro { get; set; }

        public string MensagemRetorno { get; set; }

        public ResultResponse(bool houveErro, string mensagemRetorno)
        {
            this.HouveErro = houveErro;
            this.MensagemRetorno = mensagemRetorno;
        }
    }
}
