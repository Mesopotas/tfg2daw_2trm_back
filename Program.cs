using Cinema.Controllers;
using Cinema.Repositories;
using Cinema.Service;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("cinema");

builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>(provider =>
new UsuariosRepository(connectionString));

builder.Services.AddScoped<IPeliculaRepository, PeliculasRepository>(provider =>
new PeliculasRepository(connectionString));

builder.Services.AddScoped<ISalasRepository, SalasRepository>(provider =>
new SalasRepository(connectionString));

builder.Services.AddScoped<IFechasHorasRepository, FechasHorasRepository>(provider =>
new FechasHorasRepository(connectionString));

builder.Services.AddScoped<IOpinionesRepository, OpinionesRepository>(provider =>
new OpinionesRepository(connectionString));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add services
builder.Services.AddScoped<IUsuariosService, UsuariosService>();

builder.Services.AddScoped<IPeliculaService, PeliculaService>();


builder.Services.AddScoped<ISalasService, SalasService>();

builder.Services.AddScoped<IFechasHorasService, FechasHorasService>();

builder.Services.AddScoped<IOpinionesService, OpinionesService>();


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
