using MySql.Data.MySqlClient;
using Veterinaria.Entidades;

namespace Veterinaria.Datos;

public class MascotaDatos
{
    public List<Mascota> Listar()
    {
        var lista = new List<Mascota>();

        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT id_mascota, id_cliente, nombre, especie, raza, sexo,
                   fecha_nacimiento, color, senas_particulares
            FROM mascota
            ORDER BY nombre;
            """;

        using MySqlCommand cmd = new(sql, cn);
        using MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
            lista.Add(Mapear(reader));

        return lista;
    }

    public List<Mascota> ListarPorCliente(int idCliente)
    {
        var lista = new List<Mascota>();

        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT id_mascota, id_cliente, nombre, especie, raza, sexo,
                   fecha_nacimiento, color, senas_particulares
            FROM mascota
            WHERE id_cliente = @idCliente
            ORDER BY nombre;
            """;

        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@idCliente", idCliente);

        using MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
            lista.Add(Mapear(reader));

        return lista;
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

        return reader.Read() ? Mapear(reader) : null;
    }

    public int Agregar(Mascota mascota)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            INSERT INTO mascota
                (id_cliente, nombre, especie, raza, sexo,
                 fecha_nacimiento, color, senas_particulares)
            VALUES
                (@idCliente, @nombre, @especie, @raza, @sexo,
                 @fechaNac, @color, @senas);
            """;

        using MySqlCommand cmd = new(sql, cn);
        AgregarParametros(cmd, mascota);
        cmd.ExecuteNonQuery();

        return Convert.ToInt32(cmd.LastInsertedId);
    }

    public bool Modificar(Mascota mascota)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            UPDATE mascota
            SET id_cliente = @idCliente,
                nombre = @nombre,
                especie = @especie,
                raza = @raza,
                sexo = @sexo,
                fecha_nacimiento = @fechaNac,
                color = @color,
                senas_particulares = @senas
            WHERE id_mascota = @idMascota;
            """;

        using MySqlCommand cmd = new(sql, cn);
        AgregarParametros(cmd, mascota);
        cmd.Parameters.AddWithValue("@idMascota", mascota.IdMascota);

        return cmd.ExecuteNonQuery() > 0;
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

    private static void AgregarParametros(MySqlCommand cmd, Mascota mascota)
    {
        cmd.Parameters.AddWithValue("@idCliente", mascota.IdCliente);
        cmd.Parameters.AddWithValue("@nombre", mascota.Nombre);
        cmd.Parameters.AddWithValue("@especie", mascota.Especie);
        cmd.Parameters.AddWithValue("@raza", (object?)mascota.Raza ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@sexo", (object?)mascota.Sexo ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@fechaNac", (object?)mascota.FechaNacimiento ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@color", (object?)mascota.Color ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@senas", (object?)mascota.SenasParticulares ?? DBNull.Value);
    }

    private static Mascota Mapear(MySqlDataReader reader)
    {
        return new Mascota
        {
            IdMascota = reader.GetInt32("id_mascota"),
            IdCliente = reader.GetInt32("id_cliente"),
            Nombre = reader.GetString("nombre"),
            Especie = reader.GetString("especie"),
            Raza = reader.IsDBNull(reader.GetOrdinal("raza"))
                ? null
                : reader.GetString("raza"),
            Sexo = reader.IsDBNull(reader.GetOrdinal("sexo"))
                ? null
                : reader.GetString("sexo"),
            FechaNacimiento = reader.IsDBNull(reader.GetOrdinal("fecha_nacimiento"))
                ? null
                : reader.GetDateTime("fecha_nacimiento"),
            Color = reader.IsDBNull(reader.GetOrdinal("color"))
                ? null
                : reader.GetString("color"),
            SenasParticulares = reader.IsDBNull(reader.GetOrdinal("senas_particulares"))
                ? null
                : reader.GetString("senas_particulares")
        };
    }
}
