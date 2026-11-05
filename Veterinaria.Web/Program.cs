using Veterinaria.Datos;
using Veterinaria.Negocios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<Conexion>();
builder.Services.AddScoped<ClienteDatos>();
builder.Services.AddScoped<MascotaDatos>();
builder.Services.AddScoped<ClienteNegocio>();
builder.Services.AddScoped<MascotaNegocio>();

// --- INICIO DEL PASO B-08 ---
string? cadena = builder.Configuration.GetConnectionString("Veterinaria");

if (!string.IsNullOrWhiteSpace(cadena))
{
    ConfiguracionNegocio.Inicializar(cadena);

    if (builder.Environment.IsDevelopment())
    {
        try
        {
            int total = ConfiguracionNegocio.ProbarConexion();
            Console.WriteLine($"MYSQL OK. Clientes registrados: {total}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MYSQL ERROR: {ex.Message}");
        }
    }
}
else if (builder.Environment.IsDevelopment())
{
    Console.WriteLine(
        "MYSQL AVISO: no se encontró ConnectionStrings:Veterinaria. " +
        "Configure User Secrets antes de probar la conexión.");
}
// --- FIN DEL PASO B-08 ---

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();