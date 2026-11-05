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

        return _mascotaDatos.Agregar(mascota);
    }
}