using Gerenciamento.Api.DTOs;
using Gerenciamento.Api.Models;

namespace Gerenciamento.Api.Interfaces
{
    public interface ICategoriaService
    {
        Task<Categoria> CriarCategoria (CategoriaDTO categoria);
        Task<IEnumerable<Categoria>> ListarTodas();
        Task<Categoria> ListarPorID(Guid id);
    }
}
