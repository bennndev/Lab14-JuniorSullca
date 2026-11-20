using Lab10_JuniorSullca.Api.Configuration;
using Lab10_JuniorSullca.Infrastructure.Configuration;

// Npgsql: allow writing DateTime.UtcNow to 'timestamp without time zone' columns
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Servicios de Infraestructura (DbContext, Repositorios, UnitOfWork)
builder.Services.AddInfrastructureServices(builder.Configuration);

// Servicios de Aplicación (UseCases, JWT Auth, Swagger)
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Laboratorio 10 - JuniorSullca");
    c.RoutePrefix = string.Empty; // Swagger en la raíz (http://localhost:PUERTO/)
});

app.UseHttpsRedirection();
app.UseRouting();

// Middlewares de autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
