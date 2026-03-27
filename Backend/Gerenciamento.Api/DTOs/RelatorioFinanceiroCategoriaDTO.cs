namespace Gerenciamento.Api.DTOs
{
    public class RelatorioFinanceiroCategoriaDTO
    {
        public List<CategoriaResumoDTO> categorias { get; set; } = new();
        public decimal totalGeralReceitas { get; set; }
        public decimal totalGeralDespesas { get; set; }
        public decimal saldoLiquidoGeral { get; set; }
    }

    public class CategoriaResumoDTO
    {
        public Guid idCategoria { get; set; }
        public string descricao { get; set; } = string.Empty;
        public decimal totalReceitas { get; set; }
        public decimal totalDespesas { get; set; }
        public decimal saldo { get; set; }
    }
}
