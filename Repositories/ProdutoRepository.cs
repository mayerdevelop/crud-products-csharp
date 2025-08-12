using Microsoft.EntityFrameworkCore;
using c_.Data;
using c_.Models;

namespace c_.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            return await _context.Produtos.Where(p => p.Ativo).ToListAsync();
        }

        public async Task<Produto?> ObterPorIdAsync(int id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id && p.Ativo);
        }

        public async Task<Produto> CriarAsync(Produto produto)
        {
            produto.DataCadastro = DateTime.UtcNow;
            produto.Ativo = true;
            
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            
            return produto;
        }

        public async Task<Produto?> AtualizarAsync(int id, Produto produto)
        {
            var produtoExistente = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id && p.Ativo);
            
            if (produtoExistente == null)
                return null;

            produtoExistente.Nome = produto.Nome;
            produtoExistente.Descricao = produto.Descricao;
            produtoExistente.Preco = produto.Preco;
            produtoExistente.QuantidadeEstoque = produto.QuantidadeEstoque;

            await _context.SaveChangesAsync();
            
            return produtoExistente;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id && p.Ativo);
            
            if (produto == null)
                return false;

            produto.Ativo = false;
            await _context.SaveChangesAsync();
            
            return true;
        }
    }
}
