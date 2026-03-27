namespace Gerenciamento.Api.Models
{
    public class Transacao
    {
        public Guid idTransacao { get; set; }
        public string descricao { get; set; } = string.Empty;
        public decimal valor { get; set; }
        public string tipo { get; set; } = string.Empty; //despesa ou receita

        public Guid idCategoria { get; set; }
        public Categoria categoria { get; set; } = null!;
        public Guid idPessoa { get; set; }
        public Pessoa pessoa { get; set; } = null!;
    }
}
