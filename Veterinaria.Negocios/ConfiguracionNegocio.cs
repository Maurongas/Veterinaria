using Veterinaria.Datos;

namespace Veterinaria.Negocios;

public static class ConfiguracionNegocio
{
    public static void Inicializar(string cadenaConexion)
    {
        Conexion.CadenaConexion = cadenaConexion;
    }

    public static int ProbarConexion()
    {
        var datos = new ClienteDatos();
        return datos.ObtenerTotalClientes();
    }
}
