using ProjectMVCAuth.Models;
using ProjectMVCAuth.Repository.IRepository;
using System.Data.SqlClient;

namespace ProjectMVCAuth.Repository
{
    public class RolRepository : IRolRepository
    {
        private readonly string conn;

        public RolRepository(IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("CadenaSQL");
        }
        public async Task<List<Rol>> GetRolUser(int id)
        {
            List<Rol> listRoles = new List<Rol>();
            try
            {
                using(SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    using(SqlCommand cmd = new SqlCommand("SP_GetRolUser", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", id);

                        using(SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while(reader.Read())
                            {
                                Rol rol = new Rol
                                {
                                    Id = Convert.ToInt32(reader["IdRol"]),
                                    Descripcion = reader["Decripcion"].ToString()
                                };
                                listRoles.Add(rol);
                            }
                            
                        }

                    }
                }
                return listRoles;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
