using ChallengePolynomius.Configurations;
using ChallengePolynomius.Repositories.Interfaces;
using ChallengePolynomius.Repositories;
using ChallengePolynomius.Services.Interfaces;
using ChallengePolynomius.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

// ========================== Configuraci�n de la Base de Datos ========================== //
// Se configura el contexto de la base de datos usando PostgreSQL con la cadena de conexi�n
// definida en appsettings.json.
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionDataBase")));

// ========================== Inyecci�n de Dependencias ========================== //
// Se registran los servicios y repositorios con el ciclo de vida Scoped (una instancia por solicitud HTTP).
// Servicios: Contienen la l�gica de negocio.
// Repositorios: Acceden directamente a la base de datos.
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// ========================== Configuraci�n de AutoMapper ========================== //
// AutoMapper permite mapear autom�ticamente entidades y DTOs.
builder.Services.AddAutoMapper(typeof(Program));

// ========================== Configuraci�n de Controladores y Swagger ========================== //
// Habilita el uso de controladores en la API.
builder.Services.AddControllers();

// Habilita la documentaci�n de la API con Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ========================== Configuraci�n de CORS ========================== //
// Permite que la API acepte solicitudes desde cualquier origen y m�todo HTTP.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// ========================== Redirecci�n a Swagger ========================== //
// Si el usuario accede a la ra�z "/", se redirige autom�ticamente a "/swagger".
app.UseRewriter(new RewriteOptions()
    .AddRedirect("^$", "swagger"));

// ========================== Configuraci�n del Middleware ========================== //
if (app.Environment.IsDevelopment())
{
    // Habilita Swagger solo en entorno de desarrollo.
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ========================== Inicializaci�n de la Base de Datos ========================== //
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

// Aplica la pol�tica de CORS definida anteriormente.
app.UseCors("AllowAll");

// Habilita la autorizaci�n en la API.
app.UseAuthorization();

// ========================== Mapeo de Controladores y Ejecuci�n ========================== //
// Mapea todas las rutas de los controladores.
app.MapControllers();

// Inicia la ejecuci�n de la aplicaci�n.
app.Run();
