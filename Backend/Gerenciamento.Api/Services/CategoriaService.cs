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
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria> ListarPorID(Guid id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(c => c.idCategoria == id);
        }
    }
}
