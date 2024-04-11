namespace Manyminds.Application.ViewModels.Response
{
    public class RetornarTodosResponse : ResultBase
    {
        public IEnumerable<UsuarioVM> Data { get; set; }
    }
}
