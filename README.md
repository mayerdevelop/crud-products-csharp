# CRUD de Produtos - API REST em C#

Este projeto demonstra como criar uma API REST completa em C# com operações CRUD (Create, Read, Update, Delete) para gerenciamento de produtos.

## 🏗️ Estrutura do Projeto

```
c#/
├── Models/
│   └── Produto.cs                 # Modelo de dados
├── Data/
│   └── ApplicationDbContext.cs    # Contexto do Entity Framework
├── Repositories/
│   ├── IProdutoRepository.cs      # Interface do repositório
│   └── ProdutoRepository.cs       # Implementação do repositório
├── Controllers/
│   └── ProdutosController.cs      # Controller da API
├── ClienteExemplo.cs              # Cliente para consumir a API
├── ExemploUso.cs                  # Exemplos de uso
└── Program.cs                     # Configuração da aplicação
```

## 🚀 Como Executar

### 1. Restaurar Dependências
```bash
dotnet restore
```

### 2. Executar a API
```bash
dotnet run
```

A API estará disponível em:
- **Swagger UI**: https://localhost:7001/swagger
- **API Base**: https://localhost:7001/api/produtos

## 📋 Endpoints da API

### GET /api/produtos
**Descrição**: Lista todos os produtos ativos
**Resposta**: Array de produtos

### GET /api/produtos/{id}
**Descrição**: Busca um produto específico por ID
**Parâmetros**: `id` (int) - ID do produto
**Resposta**: Produto ou 404 se não encontrado

### POST /api/produtos
**Descrição**: Cria um novo produto
**Body**: JSON com dados do produto
```json
{
  "nome": "Nome do Produto",
  "descricao": "Descrição do produto",
  "preco": 99.99,
  "quantidadeEstoque": 10
}
```

### PUT /api/produtos/{id}
**Descrição**: Atualiza um produto existente
**Parâmetros**: `id` (int) - ID do produto
**Body**: JSON com dados atualizados do produto

### DELETE /api/produtos/{id}
**Descrição**: Remove um produto (soft delete)
**Parâmetros**: `id` (int) - ID do produto

## 💻 Como Consumir a API

### Usando o Cliente Exemplo

```csharp
using c_.ClienteExemplo;

var cliente = new ClienteProdutosAPI();

// Criar produto
var produto = new Produto
{
    Nome = "Meu Produto",
    Descricao = "Descrição do produto",
    Preco = 99.99m,
    QuantidadeEstoque = 10
};

var produtoCriado = await cliente.CriarProdutoAsync(produto);

// Listar todos
var produtos = await cliente.ObterTodosProdutosAsync();

// Buscar por ID
var produtoEncontrado = await cliente.ObterProdutoPorIdAsync(1);

// Atualizar
produtoEncontrado.Preco = 89.99m;
var produtoAtualizado = await cliente.AtualizarProdutoAsync(1, produtoEncontrado);

// Deletar
var deletado = await cliente.DeletarProdutoAsync(1);
```

### Usando HTTP Client (Postman, Insomnia, etc.)

#### Criar Produto
```http
POST https://localhost:7001/api/produtos
Content-Type: application/json

{
  "nome": "Notebook Dell",
  "descricao": "Notebook com processador Intel i5",
  "preco": 3499.99,
  "quantidadeEstoque": 15
}
```

#### Listar Produtos
```http
GET https://localhost:7001/api/produtos
```

#### Buscar Produto
```http
GET https://localhost:7001/api/produtos/1
```

#### Atualizar Produto
```http
PUT https://localhost:7001/api/produtos/1
Content-Type: application/json

{
  "nome": "Notebook Dell Atualizado",
  "descricao": "Notebook com processador Intel i7",
  "preco": 3999.99,
  "quantidadeEstoque": 10
}
```

#### Deletar Produto
```http
DELETE https://localhost:7001/api/produtos/1
```

## 🛠️ Tecnologias Utilizadas

- **.NET 9.0** - Framework principal
- **ASP.NET Core Web API** - Para criar a API REST
- **Entity Framework Core** - ORM para acesso a dados
- **Entity Framework In-Memory** - Banco de dados em memória para desenvolvimento
- **Swagger/OpenAPI** - Documentação da API
- **Dependency Injection** - Injeção de dependências

## 📚 Conceitos Aplicados

### 1. **Padrão Repository**
- Separa a lógica de acesso a dados
- Facilita testes e manutenção
- Abstrai a implementação do banco de dados

### 2. **Dependency Injection**
- Injeção de dependências no construtor
- Facilita testes unitários
- Melhora a modularidade do código

### 3. **Async/Await**
- Operações assíncronas para melhor performance
- Não bloqueia threads durante operações I/O

### 4. **Validação de Dados**
- Validação no controller antes de salvar
- Retorna erros apropriados (400 Bad Request)

### 5. **Soft Delete**
- Produtos não são removidos fisicamente
- Campo `Ativo` controla visibilidade

## 🔧 Configurações

### Banco de Dados
- **Desenvolvimento**: Banco em memória (Entity Framework In-Memory)
- **Produção**: Pode ser alterado para SQL Server, PostgreSQL, etc.

### CORS
- Configurado para permitir todas as origens em desenvolvimento
- Em produção, configure adequadamente para segurança

## 🧪 Testando

1. Execute a API: `dotnet run`
2. Acesse o Swagger: https://localhost:7001/swagger
3. Teste os endpoints diretamente no navegador
4. Use o cliente exemplo para testes programáticos

## 🤝 Contribuição

Este é um projeto educacional. Sinta-se à vontade para:
- Fazer perguntas
- Sugerir melhorias
- Reportar problemas
- Contribuir com código
