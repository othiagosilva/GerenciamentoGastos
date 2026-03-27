using Gerenciamento.Api.DTOs;

namespace Gerenciamento.Api.Interfaces
{
    public interface IRelatorioFinanceiroPessoa
    {
        Task<RelatorioFinanceiroPessoaDTO> GerarRelatorioFinanceiro();
    }
}
