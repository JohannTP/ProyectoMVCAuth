using ProjectMVCAuth.Models;

namespace ProjectMVCAuth.Repository.IRepository
{
    public interface IRolRepository
    {
        Task<List<Rol>> GetRolUser(int id);

    }
}
