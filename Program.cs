using CoWorking.Controllers;
using CoWorking.Repositories;
using CoWorking.Service;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("cinema");

builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>(provider =>
    new UsuariosRepository(connectionString));

builder.Services.AddScoped<ISalasRepository, SalasRepository>(provider =>
    new SalasRepository(connectionString));

// Add services to the container.
builder.Services.AddControllers();

// Configurar CORS para permitir cualquier origen
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<ISalasService, SalasService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Aplicar CORS a toda la API
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// PlatoPrincipalController.InicializarDatos();
app.Run();
