using Cinema.Controllers;
using Cinema.Repositories;
using Cinema.Service;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("cinema");

builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>(provider =>
new UsuariosRepository(connectionString));

<<<<<<< HEAD
builder.Services.AddScoped<ISalasRepository, SalasRepository>(provider =>
new SalasRepository(connectionString));
=======
builder.Services.AddScoped<IPeliculaRepository, PeliculasRepository>(provider =>
new PeliculasRepository(connectionString));
>>>>>>> 4899f256e24ae7d37dc2990bc3f450240de5032e

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add services
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
<<<<<<< HEAD
builder.Services.AddScoped<ISalasService, SalasService>();
=======
builder.Services.AddScoped<IPeliculaService, PeliculaService>();

>>>>>>> 4899f256e24ae7d37dc2990bc3f450240de5032e

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



//PlatoPrincipalController.InicializarDatos();
app.Run();