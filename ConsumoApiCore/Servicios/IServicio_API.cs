using ConsumoApiCore.Models;

namespace ConsumoApiCore.Servicios
{
    public interface IServicio_API
    {
        Task<List<Libro>> Lista();
        Task<Libro> Obtener(int id);
        Task<bool> Crear(Libro libro);
        Task<bool> Editar(Libro libro);
        Task<bool> Eliminar(int id);
    }
}
