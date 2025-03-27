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
        throw new InvalidOperationException("IServiceScopeFactory n�o foi encontrado. Verifique a configura��o dos servi�os.");
    }

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        if (service == null)
        {
            throw new InvalidOperationException("Seed n�o foi registrado no cont�iner de servi�os. Verifique se ele foi adicionado no Startup/Program.cs.");
        }

        try
        {
            service.SeedContextoData(); // Ajuste o nome do m�todo se necess�rio
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao semear dados: {ex.Message}");
            throw; // Opcional: decide se quer interromper ou continuar a execu��o.
        }
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
