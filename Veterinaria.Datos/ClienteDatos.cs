using MySql.Data.MySqlClient;
using Veterinaria.Entidades;

namespace Veterinaria.Datos;

public class ClienteDatos
{
    private readonly Conexion _conexion;

    public ClienteDatos(Conexion conexion)
    {
        _conexion = conexion;
    }

    public List<Cliente> ObtenerTodos()
    {
        var lista = new List<Cliente>();
        using var conn = new MySqlConnection(_conexion.ObtenerCadena());
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
        using var conn = new MySqlConnection(_conexion.ObtenerCadena());
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
}