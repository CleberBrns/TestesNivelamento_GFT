namespace Questao5.Domain.Entities
{
    /// <summary>
    /// Movimento Conta Corrente
    /// </summary>
    public class Movimento
    {
        /// <summary>
        /// Identificação única da Conta Corrente
        /// </summary>
        public string IdContaCorrente { get; set; }

        /// <summary>
        /// Data do movimento no formato DD/MM/YYYY
        /// </summary>
        public string DataMovimento { get; set; }

        /// <summary>
        /// Tipo do movimento
        /// C = Crédito, D = Débito
        /// </summary>
        public char TipoMovimento { get; set; }

        /// <summary>
        /// Valor do movimento
        /// </summary>
        public decimal Valor { get; set; }
    }
}
