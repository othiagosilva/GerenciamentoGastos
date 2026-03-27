namespace Gerenciamento.Api.DTOs
{
    public class CategoriaDTO
    {
        public string descricao { get; set; } = string.Empty;
        public string finalidade { get; set; } = string.Empty; // despesa/receita/ambas
    }
}
