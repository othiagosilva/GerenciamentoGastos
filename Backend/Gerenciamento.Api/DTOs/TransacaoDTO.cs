using System.ComponentModel.DataAnnotations;

namespace Gerenciamento.Api.DTOs
{
    public class TransacaoDTO
    {
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(400, MinimumLength = 1, ErrorMessage = "A descrição deve ter entre 1 e 400 caracteres.")]
        public string descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O valor da transação é obrigatório.")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal valor{ get; set; }

        [Required(ErrorMessage = "O tipo (Receita/Despesa) é obrigatório.")]
        [RegularExpression("(?i)^(Receita|Despesa)$", ErrorMessage = "O tipo deve ser 'Receita' ou 'Despesa'.")]
        public string tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ID da categoria é obrigatório.")]
        public Guid idCategoria { get; set; }

        [Required(ErrorMessage = "O ID da pessoa é obrigatório.")]
        public Guid idPessoa { get; set; }
    }
}
