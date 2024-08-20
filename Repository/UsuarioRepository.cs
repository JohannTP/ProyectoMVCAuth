using ProjectMVCAuth.Models;
using ProjectMVCAuth.Repository.IRepository;
using System.Data.SqlClient;

namespace ProjectMVCAuth.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string conn;

        public UsuarioRepository(IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("CadenaSQL");
        }

        public async Task<Usuario> FindUser(string correo, string clave)
        {
            Usuario usuario = null;
            try
            {
                using(SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    using(SqlCommand cmd = new SqlCommand("SP_FindUser", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Correo", correo);
                        cmd.Parameters.AddWithValue("@Clave", clave);

                        using(SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                usuario = new Usuario
                                {
                                    Id = Convert.ToInt32(reader["IdUsuario"]),
                                    Nombres = reader["Nombres"].ToString(),
                                    Correo = reader["Correo"].ToString(),
                                    Clave = reader["Clave"].ToString()
                                };
                            }
                        }
                    }
                    return usuario;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
