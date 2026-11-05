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
