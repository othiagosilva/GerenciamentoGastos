namespace Gerenciamento.Api.DTOs
{
    public class TransacaoResponseDTO
    {
        public Guid id { get; set; }
        public string descricao { get; set; } = string.Empty;
        public decimal valor { get; set; }
        public string tipo { get; set; } = string.Empty; // "Receita" ou "Despesa"
        public Guid idPessoa { get; set; }
        public Guid idCategoria { get; set; }
    }
}
