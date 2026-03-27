using Gerenciamento.Api.DTOs;
using Gerenciamento.Api.Interfaces;
using Gerenciamento.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        /// <summary>
        /// Lista todas as categorias cadastradas
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Categoria>>> ListarTodas() => Ok(await _categoriaService.ListarTodas());


        /// <summary>
        /// Busca uma categoria específica pelo seu ID único (GUID).
        /// </summary>
        /// <param name="id">Identificador único da categoria.</param>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Categoria>> ListarPorID(Guid id)
        {
            var categoria = await _categoriaService.ListarPorID(id);
            if (categoria == null)
                return NotFound(new { message = "Categoria não encontrada." });
            return Ok(categoria);
        }

        /// <summary>
        /// Cadastra uma nova categoria no sistema.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Categoria>> Post([FromBody] CategoriaDTO categoria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novaCategoria = await _categoriaService.CriarCategoria(categoria);
            return CreatedAtAction(nameof(ListarPorID), new { id = novaCategoria.idCategoria }, novaCategoria);
        }
    }
}
