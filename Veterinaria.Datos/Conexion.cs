using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Veterinaria.Datos;

public class Conexion
{
    private readonly string _cadenaConexion;

    public Conexion(IConfiguration configuration)
    {
        _cadenaConexion = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no está configurada.");
    }

    public string ObtenerCadena() => _cadenaConexion;
}