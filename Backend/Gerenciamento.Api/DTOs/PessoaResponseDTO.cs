namespace Gerenciamento.Api.DTOs
{
    public class PessoaResponseDTO
    {
        public Guid id { get; set; }
        public string nome { get; set; } = string.Empty;
        public int idade { get; set; }
        public decimal saldo { get; set; }
        public List<TransacaoResponseDTO> transacoes { get; set; } = new();
    }
}
