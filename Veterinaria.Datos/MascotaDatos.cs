using MySql.Data.MySqlClient;
using Veterinaria.Entidades;

namespace Veterinaria.Datos;

public class MascotaDatos
{
    private readonly Conexion _conexion;

    public MascotaDatos(Conexion conexion)
    {
        _conexion = conexion;
    }

    public List<Mascota> ObtenerPorCliente(int idCliente)
    {
        var lista = new List<Mascota>();
        using var conn = new MySqlConnection(_conexion.ObtenerCadena());
        conn.Open();

        string query = "SELECT id_mascota, id_cliente, nombre, especie, raza, sexo, fecha_nacimiento, color, senas_particulares FROM mascotas WHERE id_cliente = @idCliente;";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@idCliente", idCliente);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Mascota
            {
                IdMascota = reader.GetInt32("id_mascota"),
                IdCliente = reader.GetInt32("id_cliente"),
                Nombre = reader.GetString("nombre"),
                Especie = reader.GetString("especie"),
                Raza = reader.IsDBNull(reader.GetOrdinal("raza")) ? null : reader.GetString("raza"),
                Sexo = reader.IsDBNull(reader.GetOrdinal("sexo")) ? null : reader.GetString("sexo"),
                FechaNacimiento = reader.IsDBNull(reader.GetOrdinal("fecha_nacimiento")) ? null : reader.GetDateTime("fecha_nacimiento"),
                Color = reader.IsDBNull(reader.GetOrdinal("color")) ? null : reader.GetString("color"),
                SenasParticulares = reader.IsDBNull(reader.GetOrdinal("senas_particulares")) ? null : reader.GetString("senas_particulares")
            });
        }
        return lista;
    }

    public bool Agregar(Mascota mascota)
    {
        using var conn = new MySqlConnection(_conexion.ObtenerCadena());
        conn.Open();

        string query = @"INSERT INTO mascotas (id_cliente, nombre, especie, raza, sexo, fecha_nacimiento, color, senas_particulares) 
                        VALUES (@idCliente, @nombre, @especie, @raza, @sexo, @fechaNac, @color, @senas);";

        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@idCliente", mascota.IdCliente);
        cmd.Parameters.AddWithValue("@nombre", mascota.Nombre);
        cmd.Parameters.AddWithValue("@especie", mascota.Especie);
        cmd.Parameters.AddWithValue("@raza", (object?)mascota.Raza ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@sexo", (object?)mascota.Sexo ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@fechaNac", (object?)mascota.FechaNacimiento ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@color", (object?)mascota.Color ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@senas", (object?)mascota.SenasParticulares ?? DBNull.Value);

        return cmd.ExecuteNonQuery() > 0;
    }

    public Mascota? BuscarPorId(int idMascota)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT id_mascota, id_cliente, nombre, especie, raza, sexo,
                   fecha_nacimiento, color, senas_particulares
            FROM mascota
            WHERE id_mascota = @idMascota;
            """;

        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@idMascota", idMascota);

        using MySqlDataReader reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        return new Mascota
        {
            IdMascota = reader.GetInt32("id_mascota"),
            IdCliente = reader.GetInt32("id_cliente"),
            Nombre = reader.GetString("nombre"),
            Especie = reader.GetString("especie"),
            Raza = reader.IsDBNull(reader.GetOrdinal("raza")) ? null : reader.GetString("raza"),
            Sexo = reader.IsDBNull(reader.GetOrdinal("sexo")) ? null : reader.GetString("sexo"),
            FechaNacimiento = reader.IsDBNull(reader.GetOrdinal("fecha_nacimiento")) ? null : reader.GetDateTime("fecha_nacimiento"),
            Color = reader.IsDBNull(reader.GetOrdinal("color")) ? null : reader.GetString("color"),
            SenasParticulares = reader.IsDBNull(reader.GetOrdinal("senas_particulares")) ? null : reader.GetString("senas_particulares")
        };
    }

    public bool TieneConsultas(int idMascota)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT COUNT(*)
            FROM consulta
            WHERE id_mascota = @idMascota;
            """;

        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@idMascota", idMascota);

        int total = Convert.ToInt32(cmd.ExecuteScalar());
        return total > 0;
    }

    public bool Eliminar(int idMascota)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            DELETE FROM mascota
            WHERE id_mascota = @idMascota;
            """;

        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@idMascota", idMascota);

        return cmd.ExecuteNonQuery() > 0;
    }
}