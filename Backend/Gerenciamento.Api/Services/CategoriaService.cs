using Gerenciamento.Api.Data;
using Gerenciamento.Api.DTOs;
using Gerenciamento.Api.Interfaces;
using Gerenciamento.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.Api.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _context;
        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Categoria> CriarCategoria(CategoriaDTO categoria)
        {
            var categoriaExistente = await _context.Categorias
                .FirstOrDefaultAsync(c => c.descricao.ToLower() == categoria.descricao.ToLower());

            if(categoriaExistente != null)
            {
                throw new InvalidOperationException("Já existe uma categoria com essa descrição.");
            }

            var novaCategoria = new Categoria
            {
                descricao = categoria.descricao,
                finalidade = categoria.finalidade
            };

            _context.Categorias.Add(novaCategoria);
            await _context.SaveChangesAsync();
            return novaCategoria;
        }

        public async Task<IEnumerable<Categoria>> ListarTodas()
        {
            return await _context.Categorias
                .OrderBy(c => c.descricao)
                .ToListAsync();
        }

        public async Task<Categoria> ListarPorID(Guid id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(c => c.idCategoria == id);
        }
    }
}
