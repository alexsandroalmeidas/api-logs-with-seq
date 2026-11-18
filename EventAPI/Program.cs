using Serilog;
using Serilog.Events;

class Program
{
    static void Main()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose() // Definindo o nível mínimo de log como Verbose
            .WriteTo.Console() // Escrevendo os logs no console
            .CreateLogger();

        Log.Verbose("Esta é uma mensagem de log Verbose."); // Este log será exibido
        Log.Debug("Esta é uma mensagem de log Debug."); // Este log será exibido
        Log.Information("Esta é uma mensagem de log Information."); // Este log será exibido
        Log.Warning("Esta é uma mensagem de log Warning."); // Este log será exibido
        Log.Error("Esta é uma mensagem de log Error."); // Este log será exibido
        Log.Fatal("Esta é uma mensagem de log Fatal."); // Este log será exibido

        Log.CloseAndFlush(); // Encerrando o logger
    }
}