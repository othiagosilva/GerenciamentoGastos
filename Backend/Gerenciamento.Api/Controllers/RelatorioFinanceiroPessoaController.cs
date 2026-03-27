using Gerenciamento.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioFinanceiroPessoaController : ControllerBase
    {
        private readonly IRelatorioFinanceiroPessoa _relatorioService;
        public RelatorioFinanceiroPessoaController(IRelatorioFinanceiroPessoa relatorioFinanceiroPessoaService)
        {
            _relatorioService = relatorioFinanceiroPessoaService;
        }

        /// <summary>
        /// Gera um relatório financeiro consolidado para todas as pessoas cadastradas, incluindo total de receitas, despesas e saldo líquido.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GerarRelatorioFinanceiro()
        {
            var relatorio = await _relatorioService.GerarRelatorioFinanceiro();
            return Ok(relatorio);
        }
    }
}