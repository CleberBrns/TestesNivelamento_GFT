using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentoRequest
    {
        /// <summary>
        /// Identificação única da Conta Corrente
        /// </summary>
        public string IdContaCorrente { get; set; }

        /// <summary>
        /// Tipo do movimento
        /// C = Crédito, D = Débito
        /// </summary>
        public string TipoMovimento { get; set; }

        /// <summary>
        /// Valor do movimento
        /// </summary>
        public decimal Valor { get; set; }
    }
}
