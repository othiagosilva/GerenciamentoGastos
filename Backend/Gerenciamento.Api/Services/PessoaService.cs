using Gerenciamento.Api.Data;
using Gerenciamento.Api.DTOs;
using Gerenciamento.Api.Interfaces;
using Gerenciamento.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.Api.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly AppDbContext _context;

        public PessoaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Pessoa> CriarPessoa(PessoaDTO pessoa)
        {
            var novaPessoa = new Pessoa
            {
                nome = pessoa.nome,
                idade = pessoa.idade
            };
            _context.Pessoas.Add(novaPessoa);
            await _context.SaveChangesAsync();
            return novaPessoa;
        }

        public async Task<IEnumerable<Pessoa>> ListarTodas()
        {
            return await _context.Pessoas
                .Include (p => p.transacoes)
                .ToListAsync();
        }

        public async Task<Pessoa> ListarPorID(Guid id)
        {
            return await _context.Pessoas
                        .Include(p => p.transacoes)
                        .FirstOrDefaultAsync(p => p.idPessoa == id);
        }

        public async Task<Pessoa> AtualizarPessoa(Guid id, PessoaDTO pessoa)
        {
            var pessoaExistente = await _context.Pessoas.FindAsync(id);

            if (pessoaExistente == null)
                throw new KeyNotFoundException("Pessoa não encontrada.");

            pessoaExistente.nome = pessoa.nome;
            pessoaExistente.idade = pessoa.idade;

            await _context.SaveChangesAsync();
            return pessoaExistente;
        }

        public async Task<bool> DeletarPessoa(Guid id)
        {
            var pessoaExistente = await _context.Pessoas.FindAsync(id);

            if (pessoaExistente == null) return false;

            _context.Pessoas.Remove(pessoaExistente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
