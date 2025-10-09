using System.Diagnostics;
using System.Text.Json.Serialization;
using KanbanBackend.Data;
using KanbanBackend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
}); ;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? DbConnection = builder.Configuration["KanbanBackend:Database"];

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseNpgsql(DbConnection)
);

builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddScoped<IColumnService, ColumnService>();

builder.Services.AddScoped<Dataseed>();

var app = builder.Build();


// Terminal Commands

// dotnet run --seed
if (args.Contains("--seed") && app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<Dataseed>();
    await seeder.Seed();
    return;
}


// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
