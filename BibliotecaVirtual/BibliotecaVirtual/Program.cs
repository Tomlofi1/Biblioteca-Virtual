using BibliotecaVirtual;
using BibliotecaVirtual.Data;
using BibliotecaVirtual.Interface;
using BibliotecaVirtual.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ILivrosRepository, LivrosRepository>();
builder.Services.AddTransient<ILivrosRepository,LivrosRepository>();
builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ContextoData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    SeedData(app);
}

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    if (scopedFactory == null)
    {
        throw new InvalidOperationException("IServiceScopeFactory não foi encontrado. Verifique a configuração dos serviços.");
    }

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        if (service == null)
        {
            throw new InvalidOperationException("Seed não foi registrado no contêiner de serviços. Verifique se ele foi adicionado no Startup/Program.cs.");
        }

        try
        {
            service.SeedContextoData(); // Ajuste o nome do método se necessário
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao semear dados: {ex.Message}");
            throw; // Opcional: decide se quer interromper ou continuar a execução.
        }
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
