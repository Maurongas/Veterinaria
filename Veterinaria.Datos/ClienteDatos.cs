using MySql.Data.MySqlClient;
using Veterinaria.Entidades;

namespace Veterinaria.Datos;

public class ClienteDatos
{
    public List<Cliente> Listar()
    {
        var lista = new List<Cliente>();

        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT id_cliente, nombre, apellido_paterno, apellido_materno,
                   telefono, correo, direccion, fecha_registro
            FROM cliente
            ORDER BY apellido_paterno, nombre;
            """;

        using MySqlCommand cmd = new(sql, cn);
        using MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
            lista.Add(Mapear(reader));

        return lista;
    }

    public List<Cliente> Buscar(string termino)
    {
        var lista = new List<Cliente>();

        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT id_cliente, nombre, apellido_paterno, apellido_materno,
                   telefono, correo, direccion, fecha_registro
            FROM cliente
            WHERE nombre LIKE @termino
               OR apellido_paterno LIKE @termino
               OR apellido_materno LIKE @termino
               OR telefono LIKE @termino
               OR correo LIKE @termino
            ORDER BY apellido_paterno, nombre;
            """;

        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@termino", $"%{termino}%");

        using MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
            lista.Add(Mapear(reader));

        return lista;
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

        return reader.Read() ? Mapear(reader) : null;
    }

    public int Agregar(Cliente cliente)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            INSERT INTO cliente
                (nombre, apellido_paterno, apellido_materno, telefono, correo, direccion)
            VALUES
                (@nombre, @paterno, @materno, @telefono, @correo, @direccion);
            """;

        using MySqlCommand cmd = new(sql, cn);
        AgregarParametros(cmd, cliente);
        cmd.ExecuteNonQuery();

        return Convert.ToInt32(cmd.LastInsertedId);
    }

    public bool Modificar(Cliente cliente)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            UPDATE cliente
            SET nombre = @nombre,
                apellido_paterno = @paterno,
                apellido_materno = @materno,
                telefono = @telefono,
                correo = @correo,
                direccion = @direccion
            WHERE id_cliente = @idCliente;
            """;

        using MySqlCommand cmd = new(sql, cn);
        AgregarParametros(cmd, cliente);
        cmd.Parameters.AddWithValue("@idCliente", cliente.IdCliente);

        return cmd.ExecuteNonQuery() > 0;
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

    private static void AgregarParametros(MySqlCommand cmd, Cliente cliente)
    {
        cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
        cmd.Parameters.AddWithValue("@paterno", cliente.ApellidoPaterno);
        cmd.Parameters.AddWithValue("@materno", (object?)cliente.ApellidoMaterno ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
        cmd.Parameters.AddWithValue("@correo", (object?)cliente.Correo ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@direccion", (object?)cliente.Direccion ?? DBNull.Value);
    }

    private static Cliente Mapear(MySqlDataReader reader)
    {
        return new Cliente
        {
            IdCliente = reader.GetInt32("id_cliente"),
            Nombre = reader.GetString("nombre"),
            ApellidoPaterno = reader.GetString("apellido_paterno"),
            ApellidoMaterno = reader.IsDBNull(reader.GetOrdinal("apellido_materno"))
                ? null
                : reader.GetString("apellido_materno"),
            Telefono = reader.GetString("telefono"),
            Correo = reader.IsDBNull(reader.GetOrdinal("correo"))
                ? null
                : reader.GetString("correo"),
            Direccion = reader.IsDBNull(reader.GetOrdinal("direccion"))
                ? null
                : reader.GetString("direccion"),
            FechaRegistro = reader.GetDateTime("fecha_registro")
        };
    }
}
