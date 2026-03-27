using Gerenciamento.Api.DTOs;

namespace Gerenciamento.Api.Interfaces
{
    public interface IRelatorioFinanceiroCategoria
    {
        Task<RelatorioFinanceiroCategoriaDTO> GerarRelatorioFinanceiro();
    }
}
