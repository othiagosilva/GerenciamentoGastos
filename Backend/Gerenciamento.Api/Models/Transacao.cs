using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciamento.Api.Models
{
    public class Transacao
    {
        public Guid idTransacao { get; set; }
        public string descricao { get; set; } = string.Empty;
        public decimal valor { get; set; }
        public string tipo { get; set; } = string.Empty; //despesa ou receita

        public Guid idCategoria { get; set; }
        [ForeignKey("idCategoria")]
        public virtual Categoria categoria { get; set; }
        
        public Guid idPessoa { get; set; }
        [ForeignKey("idPessoa")]
        public virtual Pessoa pessoa { get; set; }
    }
}
