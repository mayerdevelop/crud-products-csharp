using c_.Models;

namespace c_.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ObterTodosAsync();
        Task<Produto?> ObterPorIdAsync(int id);
        Task<Produto> CriarAsync(Produto produto);
        Task<Produto?> AtualizarAsync(int id, Produto produto);
        Task<bool> DeletarAsync(int id);
    }
}
