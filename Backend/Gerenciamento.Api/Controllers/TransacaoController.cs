using Gerenciamento.Api.DTOs;
using Gerenciamento.Api.Interfaces;
using Gerenciamento.Api.Models;
using Gerenciamento.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public TransacaoController(ITransacaoService transacaoService) { 
            _transacaoService = transacaoService;
        }

        /// <summary>
        /// Lista todas as transações cadastradas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Transacao>>> ListarTodas() => Ok(await _transacaoService.ListarTodas());

        /// <summary>
        /// Busca uma transação específica pelo seu ID único (GUID).
        /// </summary>
        /// <param name="id">Identificador único da transação.</param>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Transacao>> ListarPorID(Guid id)
        {
            var transacao = await _transacaoService.ListarPorID(id);
            if (transacao == null)
                return NotFound(new { message = "Transação não encontrada." });
            return Ok(transacao);
        }

        /// <summary>
        /// Busca todas transações de uma pessoa específica pelo seu ID único (GUID).
        /// </summary>
        /// <param name="id">Identificador único da pessoa.</param>
        [HttpGet("PorPessoa/{idPessoa:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Transacao>>> ListarPorPessoa(Guid idPessoa)
        {
            var transacoes = await _transacaoService.ListarPorPessoa(idPessoa);
            if (transacoes == null)
                return NotFound(new { message = "Nenhuma transação encontrada para essa pessoa." });
            return Ok(transacoes);
        }

        /// <summary>
        /// Busca todas transações de uma categoria específica pelo seu ID único (GUID).
        /// </summary>
        /// <param name="id">Identificador único da categoria.</param>
        [HttpGet("PorCategoria/{idCategoria:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Transacao>>> ListarPorCategoria(Guid idCategoria)
        {
            var transacoes = await _transacaoService.ListarPorCategoria(idCategoria);
            if (transacoes == null)
                return NotFound(new { message = "Nenhuma transação encontrada para essa categoria." });
            return Ok(transacoes);
        }

        /// <summary>
        /// Cadastra uma nova transação no sistema.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Transacao>> Post([FromBody] TransacaoDTO transacao) { 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var novaTransacao = await _transacaoService.CriarTransacao(transacao);

            return CreatedAtAction(nameof(ListarPorID), new { id = novaTransacao.idTransacao }, novaTransacao);
        }

    }
}
