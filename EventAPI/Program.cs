using Serilog;
using EventAPI.Services; // Ajuste para o namespace correto dos seus serviços

// 1. Configuração do Bootstrap Logger (captura erros antes da aplicação subir)
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .CreateBootstrapLogger();

try
{
    Log.Information("Iniciando a aplicação...");
    var builder = WebApplication.CreateBuilder(args);

    // 2. Configurando o Serilog como provedor de logs oficial do ASP.NET Core
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Seq("http://localhost:5341"));

    // 3. Adicionando os serviços da aplicação (Injeção de Dependência)
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Registre seu serviço de eventos (conforme a interface que você estava usando)
    builder.Services.AddScoped<IEventService, EventService>();

    var app = builder.Build();

    // 4. Configurando o Pipeline de Requisições HTTP
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    // 5. Mapeando seus Endpoints (ex: o arquivo de rotas que você criou)
    // app.MapEventsEndpoints(); // Caso tenha criado um método de extensão para mapear, ou cole suas rotas direto aqui

    Log.Information("Aplicação configurada com sucesso. Subindo o servidor...");

    // 6. COMANDO QUE FAZ A APLICAÇÃO SUBIR E FICAR ESCUTANDO REQUISIÇÕES
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "A aplicação encerrou inesperadamente durante a inicialização.");
}
finally
{
    Log.CloseAndFlush();
}