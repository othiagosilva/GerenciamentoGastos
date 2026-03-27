using Gerenciamento.Api.Data;
using Gerenciamento.Api.DTOs;
using Gerenciamento.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.Api.Services
{
    public class RelatorioFinanceiroCategoriaService : IRelatorioFinanceiroCategoria
    {
        private readonly AppDbContext _context;

        public RelatorioFinanceiroCategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RelatorioFinanceiroCategoriaDTO> GerarRelatorioFinanceiro()
        {
            var resumoCategorias = await _context.Categorias
                .Select(c => new CategoriaResumoDTO
                {
                    idCategoria = c.idCategoria,
                    descricao = c.descricao,
                    totalReceitas = _context.Transacoes
                        .Where(t => t.idCategoria == c.idCategoria && t.tipo.ToLower() == "receita")
                        .Sum(t => (decimal?)t.valor) ?? 0,
                    totalDespesas = _context.Transacoes
                        .Where(t => t.idCategoria == c.idCategoria && t.tipo.ToLower() == "despesa")
                        .Sum(t => (decimal?)t.valor) ?? 0
                })
                .ToListAsync();

            resumoCategorias.ForEach(c => c.saldo = c.totalReceitas - c.totalDespesas);

            return new RelatorioFinanceiroCategoriaDTO
            {
                categorias = resumoCategorias,
                totalGeralReceitas = resumoCategorias.Sum(c => c.totalReceitas),
                totalGeralDespesas = resumoCategorias.Sum(c => c.totalDespesas),
                saldoLiquidoGeral = resumoCategorias.Sum(c => c.saldo)
            };
        }
    } 
}
