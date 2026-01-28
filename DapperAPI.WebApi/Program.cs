using DapperAPI.Infrastructure.Interfaces;
using DapperAPI.Service.Extensions;
using DapperAPI.Service.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomDependencies(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbConnectionFactory>();

    DatabaseInitializer.Initialize(factory);
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
