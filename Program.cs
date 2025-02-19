using ChallengePolynomius.Configurations;
using ChallengePolynomius.Repositories.Interfaces;
using ChallengePolynomius.Repositories;
using ChallengePolynomius.Services.Interfaces;
using ChallengePolynomius.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

// ========================== Configuración de la Base de Datos ========================== //
// Se configura el contexto de la base de datos usando PostgreSQL con la cadena de conexión
// definida en appsettings.json.
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionDataBase")));

// ========================== Inyección de Dependencias ========================== //
// Se registran los servicios y repositorios con el ciclo de vida Scoped (una instancia por solicitud HTTP).
// Servicios: Contienen la lógica de negocio.
// Repositorios: Acceden directamente a la base de datos.
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// ========================== Configuración de AutoMapper ========================== //
// AutoMapper permite mapear automáticamente entidades y DTOs.
builder.Services.AddAutoMapper(typeof(Program));

// ========================== Configuración de Controladores y Swagger ========================== //
// Habilita el uso de controladores en la API.
builder.Services.AddControllers();

// Habilita la documentación de la API con Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ========================== Configuración de CORS ========================== //
// Permite que la API acepte solicitudes desde cualquier origen y método HTTP.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// ========================== Redirección a Swagger ========================== //
// Si el usuario accede a la raíz "/", se redirige automáticamente a "/swagger".
app.UseRewriter(new RewriteOptions()
    .AddRedirect("^$", "swagger"));

// ========================== Configuración del Middleware ========================== //
if (app.Environment.IsDevelopment())
{
    // Habilita Swagger solo en entorno de desarrollo.
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ========================== Inicialización de la Base de Datos ========================== //
// Se crea un alcance temporal para obtener el contexto de la base de datos y ejecutar la carga inicial de datos.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LibraryContext>();

    // Inserta datos iniciales en la base de datos.
    dbContext.Database.Migrate();
}

// ========================== Middleware Adicional ========================== //
// Fuerza el uso de HTTPS.
app.UseHttpsRedirection();

// Aplica la política de CORS definida anteriormente.
app.UseCors("AllowAll");

// Habilita la autorización en la API.
app.UseAuthorization();

// ========================== Mapeo de Controladores y Ejecución ========================== //
// Mapea todas las rutas de los controladores.
app.MapControllers();

// Inicia la ejecución de la aplicación.
app.Run();
