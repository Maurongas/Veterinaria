using MySql.Data.MySqlClient;

namespace Veterinaria.Datos;

public class Conexion
{
    public static string CadenaConexion { get; set; } = string.Empty;

    public static MySqlConnection Crear()
    {
        return new MySqlConnection(CadenaConexion);
    }

    public string ObtenerCadena() => CadenaConexion;
}
