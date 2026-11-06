using MySql.Data.MySqlClient;
using Veterinaria.Entidades;

namespace Veterinaria.Datos;

public class ClienteDatos
{
    public ClienteDatos()
    {
    }

    public int ObtenerTotalClientes()
    {
        using var conn = new MySqlConnection(Conexion.CadenaConexion);
        conn.Open();

        using var cmd = new MySqlCommand("SELECT COUNT(*) FROM Cliente;", conn);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public List<Cliente> ObtenerTodos()
    {
        var lista = new List<Cliente>();
        using var conn = new MySqlConnection(Conexion.CadenaConexion);
        conn.Open();

        string query = "SELECT id_cliente, nombre, apellido_paterno, apellido_materno, telefono, correo, direccion, fecha_registro FROM clientes ORDER BY apellido_paterno, nombre;";
        using var cmd = new MySqlCommand(query, conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Cliente
            {
                IdCliente = reader.GetInt32("id_cliente"),
                Nombre = reader.GetString("nombre"),
                ApellidoPaterno = reader.GetString("apellido_paterno"),
                ApellidoMaterno = reader.IsDBNull(reader.GetOrdinal("apellido_materno")) ? null : reader.GetString("apellido_materno"),
                Telefono = reader.GetString("telefono"),
                Correo = reader.IsDBNull(reader.GetOrdinal("correo")) ? null : reader.GetString("correo"),
                Direccion = reader.IsDBNull(reader.GetOrdinal("direccion")) ? null : reader.GetString("direccion"),
                FechaRegistro = reader.GetDateTime("fecha_registro")
            });
        }
        return lista;
    }

    public bool Agregar(Cliente cliente)
    {
        using var conn = new MySqlConnection(Conexion.CadenaConexion);
        conn.Open();

        string query = @"INSERT INTO clientes (nombre, apellido_paterno, apellido_materno, telefono, correo, direccion) 
                        VALUES (@nombre, @paterno, @materno, @telefono, @correo, @direccion);";

        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
        cmd.Parameters.AddWithValue("@paterno", cliente.ApellidoPaterno);
        cmd.Parameters.AddWithValue("@materno", (object?)cliente.ApellidoMaterno ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
        cmd.Parameters.AddWithValue("@correo", (object?)cliente.Correo ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@direccion", (object?)cliente.Direccion ?? DBNull.Value);

        return cmd.ExecuteNonQuery() > 0;
    }

    public Cliente? BuscarPorId(int idCliente)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT id_cliente, nombre, apellido_paterno, apellido_materno,
                   telefono, correo, direccion, fecha_registro
            FROM cliente
            WHERE id_cliente = @idCliente;
            """;

        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@idCliente", idCliente);

        using MySqlDataReader reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        return new Cliente
        {
            IdCliente = reader.GetInt32("id_cliente"),
            Nombre = reader.GetString("nombre"),
            ApellidoPaterno = reader.GetString("apellido_paterno"),
            ApellidoMaterno = reader.IsDBNull(reader.GetOrdinal("apellido_materno")) ? null : reader.GetString("apellido_materno"),
            Telefono = reader.GetString("telefono"),
            Correo = reader.IsDBNull(reader.GetOrdinal("correo")) ? null : reader.GetString("correo"),
            Direccion = reader.IsDBNull(reader.GetOrdinal("direccion")) ? null : reader.GetString("direccion"),
            FechaRegistro = reader.GetDateTime("fecha_registro")
        };
    }

    public bool TieneMascotas(int idCliente)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT COUNT(*)
            FROM mascota
            WHERE id_cliente = @idCliente;
            """;

        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@idCliente", idCliente);

        int total = Convert.ToInt32(cmd.ExecuteScalar());
        return total > 0;
    }

    public bool Eliminar(int idCliente)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            DELETE FROM cliente
            WHERE id_cliente = @idCliente;
            """;

        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@idCliente", idCliente);

        return cmd.ExecuteNonQuery() > 0;
    }
}