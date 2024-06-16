using BOOKSTATION.Server.Class;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BOOKSTATION API", Version = "v1" });
});

// Configuracion DataMemory
builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseInMemoryDatabase("InMemoryDatabase");
});

// Registro de Servicio
builder.Services.AddScoped<ILibrosService, LibrosService>();

// Politica CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configuracion HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BOOKSTATION API V1");
    });
}

app.UseHttpsRedirection();
app.UseCors("NuevaPolitica");
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
