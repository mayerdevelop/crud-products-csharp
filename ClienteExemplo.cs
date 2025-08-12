using System.Text;
using System.Text.Json;

namespace c_.ClienteExemplo
{
    public class ClienteProdutosAPI
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ClienteProdutosAPI(string baseUrl = "https://localhost:7001")
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }

        public async Task<List<Produto>> ObterTodosProdutosAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/produtos");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Produto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Produto>();
        }

        public async Task<Produto?> ObterProdutoPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/produtos/{id}");
            
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;
                
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Produto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<Produto> CriarProdutoAsync(Produto produto)
        {
            var json = JsonSerializer.Serialize(produto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync($"{_baseUrl}/api/produtos", content);
            response.EnsureSuccessStatusCode();
            
            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Produto>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? produto;
        }

        public async Task<Produto?> AtualizarProdutoAsync(int id, Produto produto)
        {
            var json = JsonSerializer.Serialize(produto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PutAsync($"{_baseUrl}/api/produtos/{id}", content);
            
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;
                
            response.EnsureSuccessStatusCode();
            
            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Produto>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<bool> DeletarProdutoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/produtos/{id}");
            
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return false;
                
            response.EnsureSuccessStatusCode();
            return true;
        }
    }

    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
