using c_.ClienteExemplo;

namespace c_.ExemploUso
{
    public class ExemploUsoAPI
    {
        public static async Task ExecutarExemplos()
        {
            Console.WriteLine("=== Exemplo de Uso da API de Produtos ===\n");

            var cliente = new ClienteProdutosAPI();

            try
            {
                Console.WriteLine("1. Criando produtos...");
                
                var produto1 = new Produto
                {
                    Nome = "Notebook Dell Inspiron",
                    Descricao = "Notebook com processador Intel i5, 8GB RAM, 256GB SSD",
                    Preco = 3499.99m,
                    QuantidadeEstoque = 15
                };

                var produto2 = new Produto
                {
                    Nome = "Mouse Gamer Logitech",
                    Descricao = "Mouse com 6 botões programáveis e RGB",
                    Preco = 199.99m,
                    QuantidadeEstoque = 50
                };

                var produtoCriado1 = await cliente.CriarProdutoAsync(produto1);
                var produtoCriado2 = await cliente.CriarProdutoAsync(produto2);

                Console.WriteLine($"Produto criado: {produtoCriado1.Nome} - ID: {produtoCriado1.Id}");
                Console.WriteLine($"Produto criado: {produtoCriado2.Nome} - ID: {produtoCriado2.Id}\n");

                Console.WriteLine("2. Listando todos os produtos...");
                var todosProdutos = await cliente.ObterTodosProdutosAsync();
                
                foreach (var produto in todosProdutos)
                {
                    Console.WriteLine($"ID: {produto.Id} | Nome: {produto.Nome} | Preço: R$ {produto.Preco:F2} | Estoque: {produto.QuantidadeEstoque}");
                }
                Console.WriteLine();

                Console.WriteLine("3. Buscando produto por ID...");
                var produtoEncontrado = await cliente.ObterProdutoPorIdAsync(produtoCriado1.Id);
                
                if (produtoEncontrado != null)
                {
                    Console.WriteLine($"Produto encontrado: {produtoEncontrado.Nome} - Preço: R$ {produtoEncontrado.Preco:F2}");
                }
                Console.WriteLine();

                Console.WriteLine("4. Atualizando produto...");
                produtoEncontrado!.Preco = 3299.99m;
                produtoEncontrado.QuantidadeEstoque = 12;
                
                var produtoAtualizado = await cliente.AtualizarProdutoAsync(produtoEncontrado.Id, produtoEncontrado);
                
                if (produtoAtualizado != null)
                {
                    Console.WriteLine($"Produto atualizado: {produtoAtualizado.Nome} - Novo preço: R$ {produtoAtualizado.Preco:F2} - Novo estoque: {produtoAtualizado.QuantidadeEstoque}");
                }
                Console.WriteLine();

                Console.WriteLine("5. Deletando produto...");
                var deletado = await cliente.DeletarProdutoAsync(produtoCriado2.Id);
                
                if (deletado)
                {
                    Console.WriteLine($"Produto com ID {produtoCriado2.Id} foi deletado com sucesso!");
                }
                Console.WriteLine();

                Console.WriteLine("6. Listando produtos após deleção...");
                var produtosRestantes = await cliente.ObterTodosProdutosAsync();
                
                foreach (var produto in produtosRestantes)
                {
                    Console.WriteLine($"ID: {produto.Id} | Nome: {produto.Nome} | Preço: R$ {produto.Preco:F2} | Estoque: {produto.QuantidadeEstoque}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}
