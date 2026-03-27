using Gerenciamento.Api.Controllers;
using Gerenciamento.Api.Data;
using Gerenciamento.Api.DTOs;
using Gerenciamento.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.Api.Services
{
    public class RelatorioFinanceiroPessoaService : IRelatorioFinanceiroPessoa
    {
        private readonly AppDbContext _context;

        public RelatorioFinanceiroPessoaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RelatorioFinanceiroPessoaDTO> GerarRelatorioFinanceiro()
        {
            var resumoPessoas = await _context.Pessoas
                .Select(p => new PessoaResumoDTO
                {
                    idPessoa = p.idPessoa,
                    nome = p.nome,
                    totalReceitas = p.transacoes
                        .Where(t => t.tipo.ToLower() == "receita")
                        .Sum(t => (decimal?)t.valor) ?? 0,
                    totalDespesas = p.transacoes
                        .Where(t => t.tipo.ToLower() == "despesa")
                        .Sum(t => (decimal?)t.valor) ?? 0
                })
                .ToListAsync();

            resumoPessoas.ForEach(p => p.saldo = p.totalReceitas - p.totalDespesas);

            return new RelatorioFinanceiroPessoaDTO
            {
                pessoas = resumoPessoas,
                totalGeralReceitas = resumoPessoas.Sum(p => p.totalReceitas),
                totalGeralDespesas = resumoPessoas.Sum(p => p.totalDespesas),
                saldoLiquidoGeral = resumoPessoas.Sum(p => p.totalReceitas) - resumoPessoas.Sum(p => p.totalDespesas)
            };
        }
    }
}
