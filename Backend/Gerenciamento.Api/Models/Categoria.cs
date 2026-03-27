namespace Gerenciamento.Api.Models
{
    public class Categoria
    {
        public Guid idCategoria { get; set; }
        public string descricao { get; set; } = string.Empty;
        public string finalidade { get; set; } = string.Empty; // despesa/receita/ambas
    }
}
