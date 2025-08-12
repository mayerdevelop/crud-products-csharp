using Microsoft.AspNetCore.Mvc;
using c_.Models;
using c_.Repositories;

namespace c_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> ObterTodos()
        {
            var produtos = await _produtoRepository.ObterTodosAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> ObterPorId(int id)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(id);
            
            if (produto == null)
                return NotFound($"Produto com ID {id} não encontrado.");

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Criar(Produto produto)
        {
            if (string.IsNullOrWhiteSpace(produto.Nome))
                return BadRequest("Nome do produto é obrigatório.");

            if (produto.Preco <= 0)
                return BadRequest("Preço deve ser maior que zero.");

            if (produto.QuantidadeEstoque < 0)
                return BadRequest("Quantidade em estoque não pode ser negativa.");

            var produtoCriado = await _produtoRepository.CriarAsync(produto);
            
            return CreatedAtAction(nameof(ObterPorId), new { id = produtoCriado.Id }, produtoCriado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Produto>> Atualizar(int id, Produto produto)
        {
            if (string.IsNullOrWhiteSpace(produto.Nome))
                return BadRequest("Nome do produto é obrigatório.");

            if (produto.Preco <= 0)
                return BadRequest("Preço deve ser maior que zero.");

            if (produto.QuantidadeEstoque < 0)
                return BadRequest("Quantidade em estoque não pode ser negativa.");

            var produtoAtualizado = await _produtoRepository.AtualizarAsync(id, produto);
            
            if (produtoAtualizado == null)
                return NotFound($"Produto com ID {id} não encontrado.");

            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletar(int id)
        {
            var resultado = await _produtoRepository.DeletarAsync(id);
            
            if (!resultado)
                return NotFound($"Produto com ID {id} não encontrado.");

            return NoContent();
        }
    }
}
