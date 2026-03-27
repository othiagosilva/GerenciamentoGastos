namespace Gerenciamento.Api.DTOs
{
    public class RelatorioFinanceiroPessoaDTO
    {
        public List<PessoaResumoDTO> pessoas { get; set; } = new();
        public decimal totalGeralReceitas { get; set; }
        public decimal totalGeralDespesas { get; set; }
        public decimal saldoLiquidoGeral { get; set; }
    }

    public class PessoaResumoDTO
    {
        public Guid idPessoa { get; set; }
        public string nome { get; set; } = string.Empty;
        public decimal totalReceitas { get; set; }
        public decimal totalDespesas { get; set; }
        public decimal saldo { get; set; }
    }
}
