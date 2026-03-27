namespace Gerenciamento.Api.Models
{
    public class Pessoa
    {
        public Guid idPessoa { get; set; }
        public string nome { get; set; } = string.Empty;
        public int idade { get; set; } 

        public List<Transacao> transacoes { get; set; } = new List<Transacao>();
    }
}
