using System.ComponentModel.DataAnnotations.Schema;

namespace Questao5.Domain.Entities
{
    [Table("movimento")]
    public class ValorMovimento
    {
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
