using System.ComponentModel.DataAnnotations;

namespace Gerenciamento.Api.DTOs
{
    public class CategoriaDTO
    {
        [Required(ErrorMessage = "A descrição da categoria é obrigatória.")]
        [StringLength(400, MinimumLength = 1, ErrorMessage = "A descrição deve ter entre 1 e 400 caracteres.")]
        public string descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "A finalidade é obrigatória.")]
        [RegularExpression("(?i)^(Receita|Despesa|Ambas)$",
            ErrorMessage = "Finalidade inválida. Use 'Receita', 'Despesa' ou 'Ambas'.")]
        public string finalidade { get; set; } = string.Empty;
    }
}
