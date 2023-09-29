using LojaDeGames.Model;

namespace LojaDeGames.Service
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>>GetAll();
        Task<Produto?> GetById(long id);
        Task<IEnumerable<Produto>> GetByNomeOuConsole(string nome, string console);
        Task<IEnumerable<Produto>> GetByPrecoIntervalo(decimal min, decimal max);
        Task<Produto?> Create(Produto produto);
        Task<Produto?> Update(Produto produto);
        Task Delete(Produto produto);
    }
}
