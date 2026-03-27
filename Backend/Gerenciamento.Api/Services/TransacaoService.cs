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

        public async Task<TransacaoResponseDTO> CriarTransacao(TransacaoDTO transacao)
        {
            var pessoa = await _context.Pessoas.FindAsync(transacao.idPessoa);
            if (pessoa == null)
                throw new KeyNotFoundException("A pessoa informada não existe.");

            if (pessoa.idade < 18 && string.Equals(transacao.tipo, "Receita", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Menores de 18 anos não podem registrar receitas, apenas despesas.");

            var categoria = await _context.Categorias.FindAsync(transacao.idCategoria);
            if (categoria == null)
                throw new KeyNotFoundException("A categoria informada não existe.");

            bool tipoTransacaoAmbas = string.Equals(categoria.finalidade, "Ambas", StringComparison.OrdinalIgnoreCase);

            if (!string.Equals(transacao.tipo, categoria.finalidade, StringComparison.OrdinalIgnoreCase) && !tipoTransacaoAmbas)
            {
                throw new InvalidOperationException($"Não é possível usar uma categoria de '{categoria.finalidade}' para uma transação do tipo '{transacao.tipo}'.");
            }

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


            return new TransacaoResponseDTO
            {
                id = novaTransacao.idTransacao,
                descricao = novaTransacao.descricao,
                valor = novaTransacao.valor,
                tipo = novaTransacao.tipo,
                idCategoria = novaTransacao.idCategoria,
                idPessoa = novaTransacao.idPessoa
            };
        }

        public async Task<IEnumerable<Transacao>> ListarTodas()
        {
            return await _context.Transacoes
                .OrderBy(t => t.descricao)
                .ToListAsync();
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
