namespace ProjectMVCAuth.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }

        public Rol IdRol { get; set; }
    }
}
