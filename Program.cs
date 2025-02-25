using CoWorking.Controllers;
using CoWorking.Repositories;
using CoWorking.Service;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("cinema");

builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>(provider =>
        new UsuariosRepository(connectionString));

builder.Services.AddScoped<ITipoSalasRepository, TipoSalasRepository>(provider =>
    new TipoSalasRepository(connectionString));

builder.Services.AddScoped<ISedesRepository, SedesRepository>(provider =>
    new SedesRepository(connectionString));

builder.Services.AddScoped<ISalasRepository, SalasRepository>(provider =>
    new SalasRepository(connectionString));  

builder.Services.AddScoped<IRolesRepository, RolesRepository>(provider =>
    new RolesRepository(connectionString));    

builder.Services.AddScoped<ITramosHorariosRepository, TramosHorariosRepository>(provider =>
    new TramosHorariosRepository(connectionString));    


// Add services
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<ITipoSalasService, TipoSalasService>();
builder.Services.AddScoped<ISedesService, SedesService>();
builder.Services.AddScoped<ISalasService, SalasService>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<ITramosHorariosService, TramosHorariosService>();

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