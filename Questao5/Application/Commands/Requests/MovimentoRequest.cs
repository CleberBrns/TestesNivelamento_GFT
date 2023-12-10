using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    /// <summary>
    /// Dados para inclusão de um Movimento de Conta Corrente
    /// </summary>
    public class MovimentoRequest
    {
        /// <summary>
        /// Identificação única da Conta Corrente
        /// </summary>
        /// <example>FA99D033-7067-ED11-96C6-7C5DFA4A16C9</example>
        public string IdContaCorrente { get; set; }

        /// <summary>
        /// Tipo do movimento
        /// C = crédito, D = débito
        /// </summary>
        /// <example>crédito</example>
        public string TipoMovimento { get; set; }

        /// <summary>
        /// Valor do movimento
        /// </summary>
        /// <example>375.91</example>
        public decimal Valor { get; set; }
    }
}
