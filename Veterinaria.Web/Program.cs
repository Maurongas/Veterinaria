using Veterinaria.Negocios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string? cadena = builder.Configuration.GetConnectionString("Veterinaria");

if (!string.IsNullOrWhiteSpace(cadena))
{
    ConfiguracionNegocio.Inicializar(cadena);
}
else
{
    System.Diagnostics.Debug.WriteLine(
        "No se encontró ConnectionStrings:Veterinaria. " +
        "Revise User Secrets.");
}

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