namespace Manyminds.Domain.Entities
{
    public class UsuarioControleAcesso
    {
        public int Codigo { get; set; }
        public string UsuarioEmail { get; set; }
        public DateTime UltimoAcesso { get; set; }
        public int NumeroTentativa { get; set; }
        public bool Bloqueado { get; set; }
    }
}
