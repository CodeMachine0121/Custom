using CustomBlockChainLab.Models.DataBases;
using CustomBlockChainLab.Repositories;
using CustomBlockChainLab.Repositories.Interfaces;
using CustomBlockChainLab.Services;
using CustomBlockChainLab.Services.Interfaces;
using EccSDK;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddTransient<IChainService, ChainService>();
builder.Services.AddTransient<IChainRepository, ChainRepository>();

builder.Services.AddDbContext<BlockchainDbContext>(options =>
{
    var connectionString = builder.Configuration.GetValue<string>("Sql");

    var environmentVariables = Environment.GetEnvironmentVariables();
    connectionString = connectionString!.Replace("${DB_SERVER}", "localhost");
    connectionString = connectionString.Replace("${DB_NAME}", "BlockChain");
    connectionString = connectionString.Replace("${DB_USER}", "root");
    connectionString = connectionString.Replace("${DB_PASS}", "1234qwer");
    
    options.UseMySQL(connectionString);
}, ServiceLifetime.Transient);

var keyPair = EccGenerator.GenerateKeyPair(256);
var sessionKey = SessionKeyGenerator.GenerateSessionKey();
builder.Services.AddSingleton(keyPair);
builder.Services.AddSingleton(sessionKey);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();
app.UseHttpsRedirection();


app.Run();