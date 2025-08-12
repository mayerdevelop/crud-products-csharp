using c_.ClienteExemplo;
using c_.ExemploUso;

namespace c_
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== CRUD de Produtos - Exemplo Prático ===\n");
            
            await ExemploUsoAPI.ExecutarExemplos();
            
            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}
