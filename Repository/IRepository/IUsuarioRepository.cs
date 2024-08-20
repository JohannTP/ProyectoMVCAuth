using ProjectMVCAuth.Models;

namespace ProjectMVCAuth.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        Task<Usuario> FindUser(string correo, string clave);
    }
}
