using Veterinaria.Datos;
using Veterinaria.Entidades;

namespace Veterinaria.Negocios;

public class ClienteNegocio
{
    private readonly ClienteDatos _clienteDatos;

    public ClienteNegocio(ClienteDatos clienteDatos)
    {
        _clienteDatos = clienteDatos;
    }

    public List<Cliente> ObtenerTodos() => _clienteDatos.ObtenerTodos();

    public bool Agregar(Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.Nombre) || string.IsNullOrWhiteSpace(cliente.ApellidoPaterno))
            throw new ArgumentException("El nombre y apellido paterno son obligatorios.");

        return _clienteDatos.Agregar(cliente);
    }

    public bool Eliminar(int idCliente)
    {
        if (idCliente <= 0)
            throw new ArgumentException("Cliente no válido.");

        Cliente? cliente = _clienteDatos.BuscarPorId(idCliente);

        if (cliente is null)
            return false;

        if (_clienteDatos.TieneMascotas(idCliente))
        {
            throw new ReglaNegocioException(
                "No se puede eliminar el cliente porque tiene mascotas registradas.");
        }

        return _clienteDatos.Eliminar(idCliente);
    }
}