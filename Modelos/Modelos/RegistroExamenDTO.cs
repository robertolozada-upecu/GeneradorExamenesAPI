namespace Modelos
{
    public class RegistroExamenDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UsuarioDTO? Usuario { get; set; }
        public string NombreExamen { get; set; }
        public int NotaFinal { get; set; }
        public DateTime FechaExamen { get; set; }
    }
}
