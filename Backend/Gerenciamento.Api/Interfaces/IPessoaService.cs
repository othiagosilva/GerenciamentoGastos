using Gerenciamento.Api.DTOs;
using Gerenciamento.Api.Models;

namespace Gerenciamento.Api.Interfaces
{
    public interface IPessoaService
    {
        Task<Pessoa> CriarPessoa (PessoaDTO pessoa);
        Task<IEnumerable<PessoaResponseDTO>> ListarTodas();
        Task<PessoaResponseDTO> ListarPorID(Guid id);
        Task<Pessoa> AtualizarPessoa(Guid id, PessoaDTO pessoa);
        Task<bool> DeletarPessoa(Guid id);

    }
}
