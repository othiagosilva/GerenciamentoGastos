using Gerenciamento.Api.DTOs;
using Gerenciamento.Api.Interfaces;
using Gerenciamento.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;
        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        /// <summary>
        /// Lista todas as pessoas cadastradas e seus saldos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Pessoa>>> ListarTodas() => Ok(await _pessoaService.ListarTodas());

        /// <summary>
        /// Busca uma pessoa específica pelo seu ID único (GUID).
        /// </summary>
        /// <param name="id">Identificador único da pessoa.</param>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pessoa>> ListarPorID(Guid id)
        {
            var pessoa = await _pessoaService.ListarPorID(id);

            if (pessoa == null)
                return NotFound(new { message = "Pessoa não encontrada." });

            return Ok(pessoa);
        }

        /// <summary>
        /// Cadastra uma nova pessoa no sistema.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pessoa>> Post([FromBody] PessoaDTO pessoa)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novaPessoa = await _pessoaService.CriarPessoa(pessoa);

            return CreatedAtAction(nameof(ListarPorID), new { id = novaPessoa.idPessoa }, novaPessoa);
        }

        /// <summary>
        /// Atualiza os dados de uma pessoa existente.
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pessoa>> Put(Guid id, [FromBody] PessoaDTO pessoa)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var pessoaAtualizada = await _pessoaService.AtualizarPessoa(id, pessoa);
                return Ok(pessoaAtualizada);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Pessoa não encontrada." });
            }
        }

        /// <summary>
        /// Remove uma pessoa e todas as suas transações vinculadas.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deletado = await _pessoaService.DeletarPessoa(id);
            if (!deletado)
                return NotFound(new { message = "Pessoa não encontrada." });
            return NoContent();
        }
    }
}
