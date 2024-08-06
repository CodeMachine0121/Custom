using CustomBlockChainLab.Repositories;
using CustomBlockChainLab.Repositories.Interfaces;
using CustomBlockChainLab.Services;
using CustomBlockChainLab.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddTransient<IChainService, ChainService>();
builder.Services.AddTransient<IChainRepository, ChainRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();


app.Run();