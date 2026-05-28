using Microsoft.EntityFrameworkCore;
using Serviteca.Backend.Data;
using Serviteca.Backend.Repositories.Implementations;
using Serviteca.Backend.Repositories.Interface;
using Serviteca.Backend.UnitsOfWork.Implementations;
using Serviteca.Backend.UnitsOfWork.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//evite ciclo en cascada
builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection"));

builder.Services.AddTransient<SeedDb>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));

builder.Services.AddScoped<IBrandsRepository, BrandsRepository>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<IVehiclesRepository, VehiclesRepository>();
builder.Services.AddScoped<ITypesRepository, VehicleTypesRepository>();
builder.Services.AddScoped<ITypesRepository, VehicleTypesRepository>();
builder.Services.AddScoped<IDocumentTypesRepository, DocumentTypesRepository>();
builder.Services.AddScoped<IUsesRepository, UsesRepository>();
builder.Services.AddScoped<IInsurersRepository, InsurersRepository>();

var app = builder.Build();

// inyection manual Tutorial 72 - Parte 19 - Alimentador de base de datos https://www.youtube.com/watch?v=VD1b8yAMC7o&list=PLuEZQoW9bRnRBThyGs208ZMrCYBRTvIg2&index=19
SeedData(app);

void SeedData(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory!.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedDb>();
        service!.SeedAsync().Wait();
    }
}

//para seguridad
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();