using Veterinaria.Datos;
using Veterinaria.Entidades;

namespace Veterinaria.Negocios;

public class MascotaNegocio
{
    private readonly MascotaDatos _mascotaDatos;

    public MascotaNegocio(MascotaDatos mascotaDatos)
    {
        _mascotaDatos = mascotaDatos;
    }

    public List<Mascota> ObtenerPorCliente(int idCliente) => _mascotaDatos.ObtenerPorCliente(idCliente);

    public bool Agregar(Mascota mascota)
    {
        if (mascota.IdCliente <= 0)
            throw new ArgumentException("Debe seleccionar un cliente válido.");

        if (string.IsNullOrWhiteSpace(mascota.Nombre))
            throw new ArgumentException("El nombre de la mascota es obligatorio.");

        if (mascota.FechaNacimiento.HasValue &&
            mascota.FechaNacimiento.Value.Date > DateTime.Today)
        {
            throw new ReglaNegocioException(
                "La fecha de nacimiento no puede ser futura.");
        }

        return _mascotaDatos.Agregar(mascota);
    }

    public bool Eliminar(int idMascota)
    {
        if (idMascota <= 0)
            throw new ArgumentException("Mascota no válida.");

        Mascota? mascota = _mascotaDatos.BuscarPorId(idMascota);

        if (mascota is null)
            return false;

        if (_mascotaDatos.TieneConsultas(idMascota))
        {
            throw new ReglaNegocioException(
                "No se puede eliminar la mascota porque tiene consultas registradas.");
        }

        return _mascotaDatos.Eliminar(idMascota);
    }
}