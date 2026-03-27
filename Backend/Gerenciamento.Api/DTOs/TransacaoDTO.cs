namespace Gerenciamento.Api.DTOs
{
    public class TransacaoDTO
    {
        public string descricao { get; set; } = string.Empty;
        public decimal valor{ get; set; }
        public string tipo { get; set; } = string.Empty;
        public Guid idCategoria { get; set; }
        public Guid idPessoa { get; set; }
    }
}
