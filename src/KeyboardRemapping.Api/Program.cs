using KeyboardRemapping.Api.Data;
using KeyboardRemapping.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Déclaration des controlleurs de l'api 
builder.Services.AddControllers();              

// On déclare le swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// On précise qu'on utilise une Bdd, ici sqlite
builder.Services.AddDbContext<KeyboardDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("KeyboardDatabase")));

// On déclare les services utilisés par l'api
builder.Services.AddScoped<IKeyboardMappingService, KeyboardMappingService>();

var app = builder.Build();

// On déclare l'utilisation d'un swagger pour tester l'api en mode graphique
app.UseSwagger();
app.UseSwaggerUI();

// Initialisation de la base de données avec le clavier "Apex Pro Gen 3" 
// avec ses codes HID de touches
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<KeyboardDbContext>();

    await KeyboardSeeder.SeedAsync(db);
}

app.MapControllers();

app.Run();

