using Ciizo.Restful.Onion.Api;
using Ciizo.Restful.Onion.Domain.Business;
using Ciizo.Restful.Onion.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuthentication();
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.RegisterBusinessServices();
builder.Services.RegisterPersistence(builder.Configuration);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.InitDatabase(builder.Configuration);
}

app.RegisterMiddlewares();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();