using MySql.Data.MySqlClient;

namespace Veterinaria.Datos;

public class Conexion
{
    public static string CadenaConexion { get; set; } = string.Empty;

    public string ObtenerCadena() => CadenaConexion;

    public static MySqlConnection Crear() => new(CadenaConexion);
}