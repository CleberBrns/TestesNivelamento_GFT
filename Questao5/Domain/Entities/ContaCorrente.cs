namespace Questao5.Domain.Entities
{
    /// <summary>
    /// Conta Corrente
    /// </summary>
    public class ContaCorrente
    {
        /// <summary>
        /// Id da Conta Corrente
        /// </summary>
        public string IdContaCorrente { get; set; }

        /// <summary>
        /// Número da Conta Corrente
        /// </summary>
        public int Numero { get; set; }

        /// <summary>
        /// Nome do Títular da Conta Corrente
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Indicativo se Conta está Ativa.
        /// (0 = Inativa, 1 = Ativa)
        /// </summary>
        public bool Ativo { get; set; }
    }
}
