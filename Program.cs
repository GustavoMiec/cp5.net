using MongoDB.Driver;
using MyMongoAPI.Repositories; // Ajuste para o seu namespace

var builder = WebApplication.CreateBuilder(args);

// Configuração do MongoDB
var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings");
var connectionString = mongoDbSettings["ConnectionString"];
var databaseName = mongoDbSettings["DatabaseName"];

// Adicionar o cliente MongoDB ao contêiner de serviços
builder.Services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

// Aqui estamos registrando IMongoDatabase como um serviço
builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(databaseName);
});

// Adicionar o repositório
builder.Services.AddScoped<ProductRepository>();

var app = builder.Build();

// Configurações do middleware (se houver)
// ...

app.Run();
