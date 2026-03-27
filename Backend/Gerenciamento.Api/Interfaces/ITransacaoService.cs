using Gerenciamento.Api.DTOs;
using Gerenciamento.Api.Models;

namespace Gerenciamento.Api.Interfaces
{
    public interface ITransacaoService
    {
        Task<TransacaoResponseDTO> CriarTransacao(TransacaoDTO transacao);
        Task<IEnumerable<Transacao>> ListarTodas();
        Task<Transacao> ListarPorID(Guid id);
        Task<IEnumerable<Transacao>> ListarPorPessoa(Guid idPessoa);
        Task<IEnumerable<Transacao>> ListarPorCategoria(Guid idPessoa);
    }
}
