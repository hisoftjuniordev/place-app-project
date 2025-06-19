using Microsoft.EntityFrameworkCore; // Uvozimo Entity Framework Core
using PlaceApp.Api.Data; // Uvozimo mapo z našim DbContext-om

var builder = WebApplication.CreateBuilder(args);

// --- ZACETEK NOVE KODE ---

// 1. Preberemo connection string iz appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Registriramo ApplicationDbContext in mu povemo, naj uporabi SQL Server
//    ter connection string, ki smo ga pravkar prebrali.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// --- KONEC NOVE KODE ---


// Add services to the container.
builder.Services.AddControllers();

// --- CORS REŠITEV: KORAK 1 ---
// Definiramo in registriramo CORS politiko.
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
                      policy =>
                      {
                          // Dovolimo klice izključno iz naslova vaše Angular aplikacije.
                          policy.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});
// --- KONEC CORS REŠITVE: KORAK 1 ---


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

// --- CORS REŠITEV: KORAK 2 ---
// Aplikaciji naročimo, naj uporablja zgoraj definirano CORS politiko.
// Ta klic mora biti pred app.UseAuthorization().
app.UseCors(myAllowSpecificOrigins);
// --- KONEC CORS REŠITVE: KORAK 2 ---

app.UseAuthorization();

app.MapControllers();

app.Run();