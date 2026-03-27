using Gerenciamento.Api.Data;
using Gerenciamento.Api.DTOs;
using Gerenciamento.Api.Interfaces;
using Gerenciamento.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.Api.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly AppDbContext _context;

        public TransacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Transacao> CriarTransacao(TransacaoDTO transacao)
        {
            var pessoaExiste = await _context.Pessoas.AnyAsync(p => p.idPessoa == transacao.idPessoa);
            if (!pessoaExiste)
                throw new Exception("A pessoa informada não existe.");
            var categoriaExiste = await _context.Categorias.AnyAsync(c => c.idCategoria == transacao.idCategoria);
            if (!categoriaExiste)
                throw new Exception("A categoria informada não existe.");

            var novaTransacao = new Transacao
            {
                descricao = transacao.descricao,
                valor = transacao.valor,
                tipo = transacao.tipo,
                idCategoria = transacao.idCategoria,
                idPessoa = transacao.idPessoa
            };
            _context.Transacoes.Add(novaTransacao);
            await _context.SaveChangesAsync();
            return novaTransacao;
        }

        public async Task<IEnumerable<Transacao>> ListarTodas()
        {
            return await _context.Transacoes.ToListAsync();
        }

        public async Task<Transacao> ListarPorID(Guid id)
        {
            return await _context.Transacoes.FirstOrDefaultAsync(t => t.idTransacao == id);
        }

        public async Task<IEnumerable<Transacao>> ListarPorPessoa(Guid idPessoa)
        {
            return await _context.Transacoes.Where(t => t.idPessoa == idPessoa).ToListAsync();
        }

        public async Task<IEnumerable<Transacao>> ListarPorCategoria(Guid idCategoria)
        {
            return await _context.Transacoes.Where(t => t.idCategoria == idCategoria).ToListAsync();
        }
    }
}
