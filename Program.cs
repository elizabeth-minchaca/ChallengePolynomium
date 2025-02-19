


using ChallengePolynomius.Configurations;
using ChallengePolynomius.Repositories.Interfaces;
using ChallengePolynomius.Repositories;
using ChallengePolynomius.Services.Interfaces;
using ChallengePolynomius.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionDataBase")));

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();


// Redirigir la raíz hacia Swagger
app.UseRewriter(new RewriteOptions()
    .AddRedirect("^$", "swagger"));

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Llama al inicializador de la base de datos
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    //Inserta datos iniciales en la base de datos
    DbInitializer.Seed(dbContext);
}


app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
