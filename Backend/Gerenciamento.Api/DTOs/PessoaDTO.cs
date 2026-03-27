using System.ComponentModel.DataAnnotations;

namespace Gerenciamento.Api.DTOs
{
    public class PessoaDTO
    {
        [Required(ErrorMessage = "O nome da pessoa é obrigatório.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "O nome deve ter entre 1 e 200 caracteres.")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$", ErrorMessage = "O nome deve conter apenas letras.")]
        public string nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A idade é obrigatória.")]
        [Range(1, 120, ErrorMessage = "A idade deve estar entre 1 e 120 anos.")]
        public int idade { get; set; }
    }
}
